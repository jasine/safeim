using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Base;
using System.Data.SqlClient;
using System.Configuration;
using Server.Ds_UsersTableAdapters;
using System.Security.Cryptography;
using System.Diagnostics;


namespace Server
{
    public partial class SMainForm : Form
    {
        Thread acceptThr;//监听线程
        TcpListener listener;//监听套接字
        bool done = true;//监听循环标准
        private Dictionary<string, UserInfo> onLineList;//用户在线列表
        T_UsersTableAdapter adapter;
        Ds_Users.T_UsersDataTable userTable;
        private const string serverKey = "<RSAKeyValue><Modulus>xjmPH5FQXwEXyS7fFfJ9rgFnvgD257FJ7Q69G4rhEbvfx2+d9ohM2E2EqVLEwcEUNBrYMCKBZhkNxhqmFhEVZIzs6gsGYMnb0plHPX3tAy01jLZP7P972V2mdCZehn3Erdk3m/41O5lbfxZmLoK3A2ah4uqMiVOuDM58IsRRC/E=</Modulus><Exponent>AQAB</Exponent><P>+WGcUdfyyAxfp+Qi9v8SMa4mkNXvsCOv9iWNTQ9SrjBkKY0CIArRlxRTY808Z2Z7sRfEpcMnm3caWekHXEVFjw==</P><Q>y3xfWg56wtX1M289CfvKj/5XfBtCFKZgcNU68Ljpbc2zsXbb5uFWHvjcbry/b5HiPjCmAWuQ2L4JwYj+UlLWfw==</Q><DP>BKZjSctjBYPljLXeSQi8iBydL3otu/UOZOKSXet5OTJBy9yLO5m4Cr8gRu4ewDbS+5xb2FNpO6be4OpNbtUD1Q==</DP><DQ>r05GI6Ln1iVYauiB0LERET7RBgXBx2KTIYJClhLoYAXgspow11b4yBQkbG7GCovHO0bULdMK5f/LDeZFHI1rZw==</DQ><InverseQ>B7K03cm9sLwR5itSa2QTvr58a2Jx+V33xc6D5CDcpyvv6n8pCznm79xfwUcYVKiJpNOjz5E678tIW+BoYCaV7A==</InverseQ><D>LbqzoPW2E+S9pwTwJDEH+2+JjlTMoRDOXCjtSYBVgfDUpD7Es04oZDIl66YTjBtN3ONggmJDddlLyyr0q9yPEXXTs/wv877QccizjKI5ftGKEd7S4uCciR8W5ZbHrSs3U/0L1MRKkJL8yt592xax6ZxKAKuR5hDX5N8A80rCml0=</D></RSAKeyValue>";

        public SMainForm()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;//关闭tb跨线程调用检查      
            adapter = new T_UsersTableAdapter();
            userTable = adapter.GetData();
        }

        /// <summary>
        /// 连接数据库
        /// </summary>

        #region 开启与关闭服务器
        /// <summary>
        /// 开启与关闭服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_startServer_Click(object sender, EventArgs e)
        {
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                string[] temp = (IpEntry.AddressList[i].ToString()).Split('.');
                if (temp.Length==4)
                {
                    tb_serverIp.Text = IpEntry.AddressList[i].ToString();
                    break;
                }
            }
            if (btn_startServer.Text == "启动服务器")
            {
                done = false;//监听条件
                acceptThr = new Thread(AcceptRequest);
                acceptThr.IsBackground = true;
                acceptThr.Start();
                btn_startServer.Text = "关闭服务器";
            }
            else
            {
                //关闭服务器，销毁资源
                if (!ClosingChack())
                    return;
                tb_serverIp.Text = "0.0.0.0";
                btn_startServer.Text = "启动服务器";
                Log("服务器关闭..");
                ltb_online.Items.Clear();
                CloseAllSocket();                
            }
        }
        #endregion


        #region 服务器监听 开启服务 void AcceptRequest()
        /// <summary>
        /// 接受客户端连接
        /// </summary>
        private void AcceptRequest()
        {
            try
            {
                onLineList = new Dictionary<string, UserInfo>();
                listener = new TcpListener(IPAddress.Any, 6789);                
                listener.Start();//开始监听
                //tb_serverIp.Text = ipep.Address.ToString();

                Log("服务器已启动..等待客户端连接..");
                while (!done)//循环监听
                {
                    TcpClient newClient = listener.AcceptTcpClient();
                    Thread clientThr = new Thread(DealClient);
                    List<object> temp = new List<object>();
                    temp.Add(newClient);
                    temp.Add(clientThr);
                    clientThr.Start(temp);
                }
                listener.Stop();
            }
            catch (SocketException)
            {
                //曾出现异常
            }
        }
        #endregion


        #region 客户端连接与处理消息循环 DealClient(object obj)
        /// <summary>
        /// 客户端连接与处理流程
        /// </summary>
        /// <param name="socket">客户端连接套接字</param>
        private void DealClient(object obj)
        {
            List<object> temp = obj as List<object>;
            TcpClient client = temp[0] as TcpClient;
            Thread currThr = temp[1] as Thread;
            NetworkStream stream = client.GetStream();

            byte[] recive = new byte[1024 * 5];
            int lenth = stream.Read(recive, 0, recive.Length);
            if (recive[0] == 9 || recive[0] == 10)//处理用户注册相关请求
            {
                 DealRegest(recive, lenth,client,currThr);
            }
            else if (recive[0] == 0)
            {
                string userName = CheckLogin(client, currThr,recive,lenth);//登陆身份验证 
                if (userName != null)//通过
                {
                    while (true)
                    {
                        try
                        {
                            byte[] msg = new byte[1024 * 2048];
                            int len = stream.Read(msg, 0, msg.Length);
                            byte[] temp2 = new byte[len];
                            Buffer.BlockCopy(msg, 0, temp2, 0, len);
                            msg = onLineList[userName].Aes.DecryptoBytes(temp2);
                            len = msg.Length;
                            switch (msg[0])
                            {
                                case 1:
                                    //client.Close();
                                    //onlineMember.Add(msg[0], sock.RemoteEndPoint);//更新服务器在线列表
                                    //onlineMember.Remove(msg[]
                                    //connectionMember1.Add(msg[0], sock);
                                    //connectionMember2.Add(sock,msg[0]);
                                    break;
                                case 2:
                                    UserOff(userName);
                                    break;
                                case 3:
                                    SendOnlineList(userName);
                                    break;
                                case 8:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 18:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 19:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 20:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 25:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 30:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 31:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 32:
                                    UserMsg(userName, msg, len);
                                    break;
                                case 33:
                                    UserMsg(userName, msg, len);
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (SocketException e)
                        {
                            Debug.WriteLine(e.ToString());
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString());
                        }

                    }
                }
            }
            else
            {
                client.Close();
                currThr.Abort();
            }
        }

      
        #endregion


        #region 用户注册验证与用户名 void DealRegest(byte[] recive, int lenth, TcpClient client,Thread currThr)
        private void DealRegest(byte[] recive, int lenth, TcpClient client,Thread currThr)
        {
            using (NetworkStream stream = client.GetStream())
            {
                if (recive[0]==9)
                {
                    string[] msg = Format.BtoS(recive, 1, lenth - 1).Split(' ');
                    int? result = adapter.GetEverRegisted(msg[0]);
                    if (result != 0)
                    {
                        stream.WriteByte(14);
                    }
                    else
                    {
                        stream.WriteByte(13);
                    }
                }
                else
                {
                    byte[] newBytes=RSAEncrytion.RSADecrypt(Format.RemoveFlag(recive, lenth), serverKey);
                    string[] msg = Format.BtoS(newBytes).Split('$');
                    byte[] encrypted=new byte[Convert.ToInt32(msg[msg.Length-1])];
                    byte[] tmp=new byte[newBytes.Length-encrypted.Length];
                    Buffer.BlockCopy(newBytes, encrypted.Length, tmp, 0, newBytes.Length - encrypted.Length);
                    string[] msg2 =Format.BtoS(tmp).Split('$');
                    if (msg2.Length > 4)
                    {
                        for (int i = 5; i < msg2.Length - 1; i++)
                        {
                            msg2[4] = msg2[4] + '$' + msg2[i];
                        }
                    }
                    int? result = adapter.GetEverRegisted(msg2[0]);
                    if (result != 0)
                    {
                        stream.WriteByte(14);
                    }
                    else
                    {
                        try
                        {
                            Buffer.BlockCopy(newBytes, 0, encrypted, 0, encrypted.Length);
                            string priv=Encoding.GetEncoding("ISO-8859-1").GetString(encrypted);
                            adapter.AddNewUser(msg2[0], msg2[1], int.Parse(msg2[2]), msg2[4], msg2[3],priv);
                            stream.WriteByte(15);
                            Log("新用户" + msg2[0] + "完成注册");
                        }
                        catch (Exception e)
                        {
                            stream.WriteByte(16);
                            MessageBox.Show("数据库可能不可写，请修改权限..");
                        }
                    }
                }                
            }
            currThr.Abort();            
        } 
        #endregion


        #region 转发用户聊天信息 void UerMsg(string userName, byte[] msg, int len)
        /// <summary>
        /// 转发用户聊天消息
        /// </summary>
        /// <param name="userName">发送源用户</param>
        /// <param name="msg">消息字节数组</param>
        /// <param name="len">消息长度</param>
        private void UserMsg(string userName, byte[] msg, int len)
        {
            string toChating = Format.BtoS(msg, 2, msg[1]);
            if (!onLineList.Keys.Contains(toChating))//检查被发送用户是否在线 不在线返回。。。。后续补充消息发送成功验证机制
            {
                ////
                return;
            }
            NetworkStream streamTo = onLineList[toChating].Stream;
            byte[] sendUser = Format.StoB(userName);
            byte[] newMsg = new byte[sendUser.Length + len - msg[1]];
            newMsg[0] = msg[0];
            newMsg[1] = (byte)sendUser.Length;
            Buffer.BlockCopy(sendUser, 0, newMsg, 2, sendUser.Length);
            Buffer.BlockCopy(msg, 2 + msg[1], newMsg, 2 + sendUser.Length, len - 2 - msg[1]);
            byte[] toSend = onLineList[toChating].Aes.EncrypFromBytes(newMsg);
            streamTo.Write(toSend, 0, toSend.Length);
        }
        #endregion


        #region 用户上下线与身份认证 string CheckLogin(TcpClient client, Thread currThr)/void UserOff(string userName)
        /// <summary>
        /// 用户上线身份认证
        /// </summary>
        /// <param name="sock"></param>
        /// <returns></returns>
        private string CheckLogin(TcpClient client, Thread currThr,byte[]bytes,int lenth)//登陆验证模块，如果步骤多可传入Socket
        {
            int result = 0;//result表示登陆结果
            NetworkStream stream = client.GetStream();
            byte[] decrypt1=RSAEncrytion.RSADecrypt(Format.RemoveFlag(bytes,lenth), serverKey);
            string userName = Format.BtoS(decrypt1,1,decrypt1[0]);
            UserInfo userInfo = new UserInfo(userName, client, currThr);
            Ds_Users.T_UsersDataTable table = adapter.GetDataByUsername(userInfo.UserName);
            byte[] IV=new byte[16];
            byte[] Key=new byte[32];
            Buffer.BlockCopy(decrypt1,1+decrypt1[0],IV,0,IV.Length);
            Buffer.BlockCopy(decrypt1,1+decrypt1[0]+IV.Length,Key,0,Key.Length);
            AESEncrytion aes = new AESEncrytion(IV, Key);
            if (table.Count == 0)
            {
                result = 1;
                //stream.WriteByte((byte)result);
                byte[] temp = BitConverter.GetBytes(result);
                byte[] first = aes.EncrypFromBytes(temp);
                stream.Write(first, 0, first.Length);
                return null;
            }
            else
            {
                Random random = new Random();
                int rand = random.Next(10, Int32.MaxValue);//生成挑战随机数
                byte[] first = aes.EncrypFromBytes(BitConverter.GetBytes(rand));
                stream.Write(first, 0, first.Length);

                byte[] userPassword = Format.StoB(table[0].password);
                byte[] num = BitConverter.GetBytes(rand);
                byte[] userPWithNum = new byte[userPassword.Length + num.Length];
                SHA1 sha = new SHA1CryptoServiceProvider();
                Buffer.BlockCopy(userPassword, 0, userPWithNum, 0, userPassword.Length);
                Buffer.BlockCopy(num, 0, userPWithNum, userPassword.Length, num.Length);
                byte[] sec = sha.ComputeHash(userPWithNum);
                byte[] temp = new byte[100];
                int len = stream.Read(temp, 0, temp.Length);
                byte[] temp2 = new byte[len];
                Buffer.BlockCopy(temp, 0, temp2, 0, len);
                byte[] back2 = aes.DecryptoBytes(temp2);
                if (!ChackByteSame(back2, sec))
                {
                    result = 2;
                    byte[] temp3 = BitConverter.GetBytes(result);
                    byte[] second = aes.EncrypFromBytes(temp3);
                    stream.Write(second, 0, second.Length);
                    return null;
                }

                if (onLineList.Keys.Contains(userInfo.UserName))
                {
                    result = 3;
                    byte[] temp4 = BitConverter.GetBytes(result);
                    byte[] second = aes.EncrypFromBytes(temp4);
                    stream.Write(second, 0, second.Length);
                    return null;
                }
                else
                {
                    result = 0;
                    byte[] temp5 = BitConverter.GetBytes(result);
                    byte[] second = aes.EncrypFromBytes(temp5);
                    stream.Write(second, 0, second.Length);
                    byte[] sendInfo = aes.EncrypFromBytes(Encoding.GetEncoding("ISO-8859-1").GetBytes(table[0].priAndPubKey));
                    stream.Write(sendInfo,0,sendInfo.Length);

                    byte[] temp6 = new byte[100];
                    int len6 = stream.Read(temp6, 0, temp6.Length);
                    byte[] temp7 = new byte[len6];
                    Buffer.BlockCopy(temp6, 0, temp7, 0, len6);
                    byte[] back3 = aes.DecryptoBytes(temp7);
                    if(Convert.ToInt16(back3[0])==0)
                    {
                        userInfo.HeadPic = table[0].headPic;
                        userInfo.SelfIntr = table[0].selfIntro;
                        userInfo.Aes = aes;
                        userInfo.PubKey = table[0].pubKey;
                        userInfo.PriAndPubKey = table[0].priAndPubKey;

                        onLineList.Add(userInfo.UserName, userInfo);

                        Thread.Sleep(300);
                        Log(userInfo.UserName + "通过身份验证,登陆服务器");
                        ltb_online.Items.Add(userInfo.UserName);//更新服务器在线显示列表
                        SendOnlineList(userInfo.UserName);
                        AddOnlineList(userInfo.UserName);//更新客户端与服务器在线列表 
                        return userInfo.UserName;
                    }
                    else
                    {
                        return null;
                    }

                   
                }
            }
        }

        /// <summary>
        /// 用户下线处理
        /// </summary>
        /// <param name="userName"></param>
        private void UserOff(string userName)
        {
            UserInfo thisUser = onLineList[userName];
            onLineList.Remove(userName);//从在线列表移除下线用户
            thisUser.Client.Close(); //关闭与客户端连接
            Log("用户 " + userName + "下线");
            byte[] msg = Format.StoB(5, userName);
            
            ltb_online.Items.Remove(userName);//从在线显示列表移除下线用户

            foreach (UserInfo user in onLineList.Values)////向各客户端发送下线消息
            {
                byte[] toSend = user.Aes.EncrypFromBytes(msg);
                user.Stream.Write(toSend, 0, toSend.Length);
            }
            thisUser.SelfThread.Abort();
        }
        #endregion


        #region 更新与发送在线列表void SendOnlineList(string LoginUser)/ voidAddOnlineList(string userName)
        /// <summary>
        /// 发送在线列表
        /// </summary>
        /// <param name="LoginUser"></param>
        private void SendOnlineList(string LoginUser)//没有考虑连接人数过多，一次发送不完
        {
            string online = "";
            foreach (string userName in onLineList.Keys)
            {
                //if (userName != LoginUser)
                //{//用户名与签名中不能出现$符号
                    online = online + "$" + userName + "_" + onLineList[userName].Address.ToString()+"_"+onLineList[userName].HeadPic.ToString()+"_"+
                        onLineList[userName].SelfIntr+"_"+onLineList[userName].PubKey;
                //}
            }
            byte[] msg = Format.StoB(3, online);
            byte[] toSend = onLineList[LoginUser].Aes.EncrypFromBytes(msg);
            onLineList[LoginUser].Stream.Write(toSend, 0, toSend.Length);
        }

        /// <summary>
        /// 向客户端通知用户上线信息
        /// </summary>
        /// <param name="userName"></param>
        private void AddOnlineList(string userName)
        {
            string online = userName + "_" + onLineList[userName].Address.ToString() + "_" + onLineList[userName].HeadPic.ToString() + "_"
                + onLineList[userName].SelfIntr + "_" + onLineList[userName].PubKey;
            byte[] msg = Format.StoB(4, online);
            foreach (string user in onLineList.Keys)
            {
                if (user != userName)
                {
                    byte[] toSend=onLineList[user].Aes.EncrypFromBytes(msg);
                    onLineList[user].Stream.Write(toSend, 0, toSend.Length);
                }
            }

        }
        #endregion

        /// <summary>
        /// 检查byte数组是否相等
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        private bool ChackByteSame(byte[] b1, byte[] b2)
        {
            if (b1.Length!=b2.Length)
            {
                return false;
            }
            for (int i = 0; i < b1.Length;i++)
            {
                if (b1[i]!=b2[i])
                {
                    return false;
                }                    
            }
            return true;
        }


        #region 日志记录 Log(string msg)
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="msg">日志消息</param>
        private void Log(string msg)
        {
            tb_log.AppendText(DateTime.Now.ToShortTimeString() + ": " + msg + "\r\n");
        }
        #endregion


        #region 关闭所有连接 关闭服务器 退出处理 bool ClosingChack() / voidcloseAllSocket()
        /// <summary>
        /// 关闭所有连接
        /// </summary>
        private bool ClosingChack()
        {
            if (onLineList.Count > 0)
            {
                if (MessageBox.Show(this, "有用户连接到服务器，确认关闭？", "确认关闭", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }
                else return true;
            }
            else
                return true;
        }

        /// <summary>
        /// 关闭所有套接字
        /// </summary>
        private void CloseAllSocket()
        {
            done = true;
            Thread.Sleep(400);
            if (onLineList.Count > 0)
            {
                try
                {
                    foreach (UserInfo user in onLineList.Values)
                    {
                        byte[] sendMsg = new byte[1];
                        sendMsg[0] = 10;
                        byte[] toSend = user.Aes.EncrypFromBytes(sendMsg);
                        user.Stream.Write(toSend,0,toSend.Length);
                        user.SelfThread.Abort();
                        user.Client.Close();
                    }
                }
                catch (SocketException)
                {
                }
            }
            listener.Stop();
            onLineList.Clear();
            acceptThr.Abort();
        }

        /// <summary>
        /// 退出时清理资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (acceptThr != null)
                {
                    if (!ClosingChack())
                        e.Cancel = true;
                    else
                    {
                        CloseAllSocket();
                    }

                }
                //Environment.Exit(0);
            }
            catch (Exception)
            {
                Application.Exit();//若有异常 终止线程
            }
            finally
            {
                Application.Exit();//若有异常 终止线程
            }
        }

        #endregion

    }
}
