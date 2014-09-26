using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Base;
using System.Security.Cryptography;

namespace Client
{
    public partial class Regest : Form
    {
        string path = Application.StartupPath + "/Images/Color/";//头像文件夹路径
        TcpClient client;
        Thread connThread;
        NetworkStream stream;
        IPAddress serverIp;
        private string serverPubKey;

        public Regest(IPAddress serverIp,string serverPubKey)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            CheckForIllegalCrossThreadCalls = false;
            AddImages();///加载头像图片
            this.serverIp = serverIp;
            this.serverPubKey = serverPubKey;
        }

        #region 与服务器通信并发送信息获取结果 void Connect(object obj)
        /// <summary>
        /// 连接服务器线程
        /// </summary>
        private void Connect(object obj)
        {
            bool tyte = (bool)obj;
            IPEndPoint serverPoint = new IPEndPoint(serverIp, 6789);
            try
            {
                client = new TcpClient();
                client.Connect(serverPoint);//连接服务器

            }
            catch (Exception e)
            {
                MessageBox.Show(this, "连接服务器失败:" + e.Message + "  请稍后再试");
                connThread.Abort();
            }

            try
            {
                stream = client.GetStream();//获取网络流
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "TcpClient错误：" + e.Message);
                connThread.Abort();
            }
            if (tyte)//检查用户名是否可用
            {
                byte[] send = Format.StoB(9, tb_username.Text);
                stream.Write(send, 0, send.Length);
                if (stream.ReadByte() == 13)
                {
                    lb_userChack.ForeColor = Color.Green;
                    lb_userChack.Text = "用户名可用";
                }
                else
                {
                    lb_userChack.ForeColor = Color.Red;
                    lb_userChack.Text = "用户名已注册";
                }
            }
            else//注册
            {
                byte[] userPassword = Format.StoB(tb_password1.Text.Trim());

                string[] keys = new string[2];//0:私钥;1:公钥
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                keys[0] = rsa.ToXmlString(true);
                keys[1] = rsa.ToXmlString(false);

                AESEncrytion aesKey=new AESEncrytion();
                aesKey.SetKeyAndIv(tb_username.Text.Trim(),tb_password1.Text.Trim());
                byte[] toEncrypt = Format.StoB(keys[0]);
                byte[] priEncrypted=aesKey.EncrypFromBytes(toEncrypt);
                
                SHA1 sha = new SHA1CryptoServiceProvider();
                byte[] userPHash = sha.ComputeHash(userPassword);
                string encryPasswd = Format.BtoS(userPHash);

                //此处有Bug，用户名中间不能用空格，否则会出错
                byte[] tmp=Format.StoB(tb_username.Text.Trim() + "$" + encryPasswd + "$" +
                                        lst_heads.SelectedIndices[0].ToString() + "$" +keys[1]+"$"+ tb_intro.Text.Trim()+"$"
                                        +priEncrypted.Length );
                byte[] toSend = new byte[tmp.Length + priEncrypted.Length];
                Buffer.BlockCopy(priEncrypted, 0,toSend, 0,priEncrypted.Length);
                Buffer.BlockCopy(tmp, 0, toSend, priEncrypted.Length, tmp.Length);
                byte[] send = RSAEncrytion.RSAEncrypt(toSend, serverPubKey);
                
                stream.Write(Format.SetFlag(10,send), 0, send.Length+1);
                int result=stream.ReadByte();
                if (result==15)
                {
                    MessageBox.Show(this, "注册成功！请用您的用户名：" + tb_username.Text.Trim() + "登录");
                    this.Close();
                }
                else if(result==16)
                {
                    MessageBox.Show(this, "注册失败，服务器故障..");
                }
                else
                {
                    MessageBox.Show(this, "注册失败，您提交的用户名可能已经被注册，请重试..");
                }
            }            
            stream.Close();
            stream.Dispose();
            connThread.Abort();
        }

        #endregion 


        #region 加载头像图片 void AddImages()
        /// <summary>
        /// 加载头像图片
        /// </summary>
        private void AddImages()
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            string Filenames = "*.jpg";
            FileInfo[] files = dir.GetFiles(Filenames);
            if (files.Length != 0)
            {
                imgl_heads.ColorDepth = ColorDepth.Depth24Bit;
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo f = null;
                    try
                    {
                        f = new FileInfo(path + i.ToString() + ".jpg");
                        imgl_heads.Images.Add(Image.FromFile(f.FullName));
                        lst_heads.Items.Add("");
                        lst_heads.Items[i].ImageIndex = i;
                        if (picb_head.ImageLocation == null)
                        {
                            picb_head.ImageLocation = f.FullName;
                            lst_heads.Items[i].Selected = true;
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("头像加载失败，请勿修改头像文件夹文件");
                    }
                }
            }
        } 
        #endregion


        #region 验证用户输入 bool Check()
        /// <summary>
        /// 验证用户输入 bool Check()
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            if (tb_username.Text.Trim() == "")
            {
                lb_userChack.Text = "请输入用户名";
                lb_userChack.ForeColor = Color.Red;
                return false;
            }
            if (tb_password1.Text.Trim() == "" || tb_password2.Text.Trim() == "")
            {
                lb_passwdChack.Text = "请输入两次密码";
                lb_passwdChack.ForeColor = Color.Red;
                return false;
            }
            if (tb_password1.Text.Trim() != tb_password2.Text.Trim())
            {
                lb_passwdChack.Text = "两次密码不一致";
                lb_passwdChack.ForeColor = Color.Red;
                return false;
            }
            else
            {
                lb_passwdChack.Text = "密码输入一致";
                lb_passwdChack.ForeColor = Color.Green;
                return true;
            }
        } 
        #endregion


        #region 提示信息的显示
        private void tb_username_Enter(object sender, EventArgs e)
        {
            lb_userChack.Text = "";
        }

        private void tb_password1_Enter(object sender, EventArgs e)
        {
            lb_passwdChack.Text = "";
        }

        private void tb_password2_Enter(object sender, EventArgs e)
        {
            lb_passwdChack.Text = "";
        }

        private void tb_password2_Leave(object sender, EventArgs e)
        {
            Check();
        }

        private void tb_password1_Leave(object sender, EventArgs e)
        {
            Check();
        }

        /// <summary>
        /// 验证用户名是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_username_Leave(object sender, EventArgs e)
        {
            connThread = new Thread(Connect);
            connThread.IsBackground = true;
            connThread.Start(true);         
        } 
        #endregion


        #region 点击预览头像事件
        /// <summary>
        /// 图片头像点击预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lst_heads_SelectedIndexChanged(object sender, EventArgs e)
        {
            int s = 0;
            if (lst_heads.FocusedItem != null)
            {
                s = lst_heads.FocusedItem.ImageIndex;
            }
            picb_head.ImageLocation = path + s.ToString() + ".jpg";
        } 
        #endregion

        /// <summary>
        /// 点击注册按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_regest_Click(object sender, EventArgs e)
        {
            connThread = new Thread(Connect);
            connThread.IsBackground = true;
            connThread.Start(false);                   
        }
        
        /// <summary>
        /// 退出检查和释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
            if (client!=null)
            {
                client.Close();//考虑是不是发生异常，如果异常发生关闭消息给服务器
            }           
        }

    }
}
