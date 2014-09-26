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
using System.Security.Cryptography;


namespace Client//客户端
{
    public partial class Login : Form //登陆窗口与连接验证实现
    {

        TcpClient client = null;
        NetworkStream stream = null;
        Thread connThread;
        AESEncrytion aes;
        private const string serverPubKey = "<RSAKeyValue><Modulus>xjmPH5FQXwEXyS7fFfJ9rgFnvgD257FJ7Q69G4rhEbvfx2+d9ohM2E2EqVLEwcEUNBrYMCKBZhkNxhqmFhEVZIzs6gsGYMnb0plHPX3tAy01jLZP7P972V2mdCZehn3Erdk3m/41O5lbfxZmLoK3A2ah4uqMiVOuDM58IsRRC/E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public Login()//构造方法
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            pal_mask.Visible = false;
        }

        #region 登陆事件
        /// <summary>
        /// 登陆事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
                return;
            connThread = new Thread(Connect);
            connThread.IsBackground = true;
            connThread.Start();
            pal_mask.Visible = true;
        }
        #endregion


        #region 与服务器通信 Connect()
        /// <summary>
        /// 连接服务器线程
        /// </summary>
        private void Connect()
        {
            IPAddress serverAddress = IPAddress.Parse(tb_serverIp.Text.Trim());
            IPEndPoint serverPoint = new IPEndPoint(serverAddress, 6789);
            try
            {
                client = new TcpClient();
                client.Connect(serverPoint);//连接服务器

            }
            catch (Exception e)
            {
                MessageBox.Show(this, "连接服务器失败:" + e.Message + "  请稍后再试");
                pal_mask.Visible = false;
                connThread.Abort();
            }

            try
            {
                stream = client.GetStream();//获取网络流
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "TcpClient错误：" + e.Message);
                pal_mask.Visible = false;
                connThread.Abort();
            }
            SafeLogin();
        }

        #endregion


        #region 安全验证 SafeLogin(Socket conn)
        /// <summary>
        /// 登陆安全认证
        /// </summary>
        /// <param name="conn">连接套接字</param>
        private void SafeLogin()
        {

            //string msgSend = tb_userName.Text.Trim() + " " + tb_passwd.Text.Trim();
            //byte[] userSend = Format.StoB(0,msgSend);

            aes = new AESEncrytion();
            byte[] userName = Format.StoB(tb_userName.Text.Trim());
            byte[] userSend = new byte[userName.Length + 1 + aes.IV.Length + aes.Key.Length];
            userSend[0] = (byte)userName.Length;
            Buffer.BlockCopy(userName, 0, userSend, 1, userName.Length);
            Buffer.BlockCopy(aes.IV, 0, userSend, 1 + userName.Length, aes.IV.Length);
            Buffer.BlockCopy(aes.Key, 0, userSend, 1 + userName.Length + aes.IV.Length, aes.Key.Length);
            byte[] first = Format.SetFlag(0, RSAEncrytion.RSAEncrypt(userSend, serverPubKey));

            stream.Write(first, 0, first.Length);

            byte[] resp = new byte[200];
            int len = stream.Read(resp, 0, resp.Length);
            byte[] recv = new byte[len];
            Buffer.BlockCopy(resp, 0, recv, 0, len);
            byte[] temp = aes.DecryptoBytes(recv);
            byte[] temp2 = new byte[4];
            Buffer.BlockCopy(temp, 0, temp2, 0, temp.Length);
            int respone = BitConverter.ToInt32(temp2, 0);
            //int resopne = stream.ReadByte();
            if (respone == 1)
            {
                stream.Close();
                stream.Dispose();
                client.Close();
                MessageBox.Show(this, "用户名错误,请重试");
                pal_mask.Visible = false;
                return;
            }


            AESEncrytion aesKey = new AESEncrytion();

            byte[] userPassword = Format.StoB(tb_passwd.Text.Trim());
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] userPHash = sha.ComputeHash(userPassword);

            string s = Format.BtoS(userPHash);
            byte[] userPHashStr = Format.StoB(s);

            byte[] num = BitConverter.GetBytes(respone);
            byte[] userPWithNum = new byte[userPHashStr.Length + num.Length];
            Buffer.BlockCopy(userPHashStr, 0, userPWithNum, 0, userPHashStr.Length);
            Buffer.BlockCopy(num, 0, userPWithNum, userPHashStr.Length, num.Length);
            byte[] second = aes.EncrypFromBytes(sha.ComputeHash(userPWithNum));
            stream.Write(second, 0, second.Length);

            byte[] resp2 = new byte[16];
            int len2 = stream.Read(resp2, 0, resp2.Length);
            //byte[] recv2 = new byte[len2];
            //Buffer.BlockCopy(resp2, 0, recv2, 0, len);
            int respone2 = BitConverter.ToInt32(aes.DecryptoBytes(resp2), 0);
            switch (respone2)
            {
                case 0://用户名，密码正确
                    byte[] resp3 = new byte[2000];
                    int len3 = stream.Read(resp3, 0, resp3.Length);
                    byte[] recv3 = new byte[len3];
                    Buffer.BlockCopy(resp3, 0, recv3, 0, len3);
                    byte[] key = aes.DecryptoBytes(recv3);
                    string keyString;
                    try
                    {
                        AESEncrytion aesEncryption = new AESEncrytion();
                        aesEncryption.SetKeyAndIv(tb_userName.Text.Trim(), tb_passwd.Text.Trim());
                        keyString = Format.BtoS(aesEncryption.DecryptoBytes(key));

                    }
                    catch (Exception)
                    {
                        byte[] temp4 = BitConverter.GetBytes(60);
                        byte[] third = aes.EncrypFromBytes(temp4);
                        stream.Write(third, 0, third.Length);
                        stream.Close();
                        stream.Dispose();
                        client.Close();
                        MessageBox.Show(this, "公私钥核对失败，放弃登陆");
                        pal_mask.Visible = false;
                        break;
                        throw;
                    }

                    byte[] temp5 = BitConverter.GetBytes(0);
                    byte[] fourth = aes.EncrypFromBytes(temp5);
                    stream.Write(fourth, 0, fourth.Length);
                    List<object> canshu = new List<object>();
                    canshu.Add(tb_userName.Text.Trim());
                    canshu.Add(client);
                    canshu.Add(keyString);
                    Thread newChat = new Thread(ExecCreate);//后台线程使用委托创建窗体，防止线程阻塞
                    newChat.IsBackground = true;
                    newChat.Start(canshu);
                    this.Hide();
                    break;
                case 2:
                    stream.Close();
                    stream.Dispose();
                    client.Close();
                    MessageBox.Show(this, "密码错误,请重试");
                    pal_mask.Visible = false;
                    break;
                case 3://账号已经登录

                    stream.Close();
                    stream.Dispose();
                    client.Close();
                    MessageBox.Show(this, "该账号已经登录");
                    pal_mask.Visible = false;
                    break;
                default:
                    stream.Close();
                    stream.Dispose();
                    client.Close();
                    MessageBox.Show(this, "未知错误！");
                    pal_mask.Visible = false;
                    break;
            }
        }
        #endregion


        #region 创建主窗口 void ExecCreate(object obj)
        private void CreateMainForm(object list)
        {
            List<object> lt = list as List<object>;
            CMainForm f = new CMainForm(lt[0] as string, lt[1] as TcpClient, aes,lt[2] as string);
            f.Show();
        }

        private void ExecCreate(object obj)
        {
            Action<object> mi = new Action<object>(CreateMainForm);
            BeginInvoke(mi, obj);
        }
        #endregion


        #region 检查用户输入bool CheckInput()
        /// <summary>
        /// 检查用户输入
        /// </summary>
        /// <returns>检查结果</returns>
        private bool CheckInput()
        {
            if (tb_serverIp.Text.Trim() == "")
            {
                MessageBox.Show(this, "请输入服务器IP地址");
                return false;
            }
            if (tb_userName.Text.Trim() == "")
            {
                MessageBox.Show(this, "请输入用户名");
                return false;
            }
            if (tb_passwd.Text.Trim() == "")
            {
                MessageBox.Show(this, "请输入密码");
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 点击注册按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkb_regest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Regest regest = new Regest(IPAddress.Parse(tb_serverIp.Text.Trim()), serverPubKey);
            regest.ShowDialog();
        }



    }
}
