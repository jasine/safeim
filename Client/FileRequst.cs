using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using Base;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Cryptography;


namespace Client
{
    public partial class FileRequst : Form
    {
        FileInfo file = null;
        string path = Application.StartupPath + "/Images/Color/";//头像文件夹路径
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        NetworkStream stream = null;
        UserInfo chatTo = null;
        System.Windows.Forms.Timer myTimer = null;
        System.Windows.Forms.Timer updateTimer = null;
        TcpClient uClient = null;
        Thread background = null;
        string fileName;
        NetworkStream streamNet;
        bool finished = false;
        long fileLen = 0;
        long haveFinish = 0;
        long lastFinish = 0;
        AESEncrytion aes;
        private AESEncrytion file_aes;
        Chat chat;
        private string rsaKeyString;
        /***********************************以下为发送端方法***********************************************/

        /// <summary>
        /// 发送端构造方法
        /// </summary>
        /// <param name="self">发送者</param>
        /// <param name="chatTo">接收者</param>
        /// <param name="client">与服务器连接TcpClient</param>
        /// <param name="file">文件信息</param>
        public FileRequst(Chat chat, AESEncrytion newAes,string rsaKeyString)//发送文件
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.chatTo = chat.chatTo;
            this.aes = chat.mainForm.aes;
            this.file_aes = newAes;
            this.file = chat.file;
            this.chat = chat;
            this.rsaKeyString = rsaKeyString;
            stream = chat.mainForm.client.GetStream();

            picb_head.ImageLocation = path + chatTo.HeadPic + ".jpg";
            lb_info.Text = "等待" + chatTo.UserName + "接收文件 ";
            lb_file.Text = file.Name;
            lkb_accept.Visible = false;
            lkb_refuse.Visible = false;
            lkb_saveAs.Visible = false;
            lb_percent.Visible = false;
            lb_current.Visible = false;
            pross_precent.Visible = false;
        }


        /// <summary>
        /// 开启发送数据线程
        /// </summary>
        /// <param name="port"></param>
        internal void Send(int port)
        {
            try
            {
                pross_precent.Visible = true;
                lb_percent.Visible = true;
                lb_info.Text = "开始向" + chatTo.UserName + "发送文件";
                uClient = new TcpClient();
                IPEndPoint endPoint = new IPEndPoint(chatTo.Address, port);
                uClient.Connect(endPoint);
                streamNet = uClient.GetStream();
                lkb_saveAs.Text = "取消";
                lkb_saveAs.Visible = true;

                updateTimer = new System.Windows.Forms.Timer();
                updateTimer.Tick += new EventHandler(TimerEventCurrent);
                updateTimer.Interval = 1000;
                updateTimer.Start();
                lb_current.Visible = true;
                background = new Thread(SendData);
                background.IsBackground = true;
                background.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        private void SendData()
        {
            try
            {
                AesCryptoServiceProvider encrypt = new AesCryptoServiceProvider();
                encrypt.IV = file_aes.IV;
                encrypt.Key = file_aes.Key;
                using (CryptoStream encryptStream = new CryptoStream(streamNet, encrypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (FileStream files = new FileStream(file.FullName, FileMode.Open))
                    {
                        fileLen = files.Length;
                        byte[] length = file_aes.EncrypFromBytes(BitConverter.GetBytes(files.Length));
                        streamNet.Write(length, 0, length.Length);

                        byte[] sourceData = new byte[1024 * 40];
                        int readLen = 0;
                        while ((readLen = files.Read(sourceData, 0, sourceData.Length)) > 0)
                        {
                            encryptStream.Write(sourceData, 0, readLen);
                            haveFinish += sourceData.Length;
                            int precent = (int)(haveFinish * 100 / files.Length);
                            pross_precent.Value = precent;
                            lb_percent.Text = precent.ToString() + "%";
                        }
                        files.Flush();
                    }
                    encryptStream.FlushFinalBlock();
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
                streamNet.Close();
                streamNet.Dispose();
            }
            pross_precent.Value = 100;
            lb_percent.Text = "100%";
            lb_info.ForeColor = Color.Blue;
            lb_info.Text = "完成发送";
            lkb_saveAs.Visible = false;
            streamNet.Flush();
            FlashWindow(this.Handle, true);
            this.Focus();
            Thread.Sleep(400);
            updateTimer.Stop();

            if (chat!=null)
            {
                chat.tssl_info.ForeColor = Color.Blue;
                chat.tssl_info.Text = "文件" + fileName + "发送完成..";
                PutOffClose();
            }                      
        }

        /// <summary>
        /// 收到接受端信号，结束发送，释放发送端资源
        /// </summary>
        internal void Finish()
        {
            try
            {
                streamNet.Close();
                streamNet.Dispose();
                uClient.Close();
                background.Abort();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }


        /**************************************以下为接收端方法*******************************************/

        /// <summary>
        /// 接收端构造方法
        /// </summary>
        /// <param name="self">接收者</param>
        /// <param name="chatTo">发送者</param>
        /// <param name="client">与服务器连接TcpClient</param>
        /// <param name="fileName">文件名</param>
        public FileRequst(UserInfo chatTo, TcpClient client, string fileName, AESEncrytion aes, AESEncrytion file_aes,string rsaKeyString)//接收文件
        {
            InitializeComponent();
            this.chatTo = chatTo;
            this.fileName = fileName;
            this.aes = aes;
            this.file_aes = file_aes;
            this.rsaKeyString = rsaKeyString;
            stream = client.GetStream();

            this.TopMost = true;
            picb_head.ImageLocation = path + chatTo.HeadPic + ".jpg";
            lb_info.Text = chatTo.UserName + "向你发送文件 ";
            lb_file.Text = fileName;
            lb_percent.Visible = false;
            pross_precent.Visible = false;
            lb_current.Visible = false;

        }

        /// <summary>
        /// 点击拒绝接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkb_refuse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!finished)
            {
                SendMsg(19, -1);
                this.Close();
                this.Dispose();
            }
            else
            {
                System.Diagnostics.Process.Start(filePath);
                PutOffClose();
            }
        }

        /// <summary>
        /// 拒绝接收
        /// </summary>
        internal void Refuse()
        {
            lb_info.ForeColor = Color.Red;
            lb_info.Text = "对方拒绝接收";
            FlashWindow(this.Handle, true);
            this.Focus();
            if (chat!=null)
            {
                chat.tssl_info.ForeColor = Color.Red;
                chat.tssl_info.Text = "对方拒绝接收文件 " + fileName;
            }
            PutOffClose();
        }

        /// <summary>
        /// 5秒后关闭窗口
        /// </summary>
        private void PutOffClose()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 5000;
            myTimer.Start();
        }

        /// <summary>
        /// 5秒自动关闭弹窗
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            this.Close();
            this.Dispose();
        }


        /// <summary>
        /// 接收文件 默认存放地址：桌面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkb_accept_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!finished)
            {
                filePath = filePath + "//" + fileName;
                Recive();
            }
            else
            {
                System.Diagnostics.Process.Start("Explorer", "/select," + filePath);
            }

        }


        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkb_saveAs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (background == null)
            {
                sfd = new SaveFileDialog();
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.FileName = fileName;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    Recive();
                }
            }
            else
            {
                SendMsg(20, 2);//取消接收
                Cancel();
            }

        }

        /// <summary>
        ///  同意接收，开启端口，开始接收线程
        /// </summary>
        private void Recive()
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 0);
            TcpListener listener = new TcpListener(ipEnd);
            listener.Start();
            IPEndPoint ipep = listener.LocalEndpoint as IPEndPoint;

            lb_info.Text = "开始接收" + chatTo.UserName + "的文件";
            lb_percent.Visible = true;
            pross_precent.Visible = true;
            lkb_accept.Visible = false;
            lkb_refuse.Visible = false;
            lkb_saveAs.Text = "取消";
            lb_current.Visible = true;
            if (ipep != null)
            {
                SendMsg(19, ipep.Port);
                updateTimer = new System.Windows.Forms.Timer();
                updateTimer.Tick += new EventHandler(TimerEventCurrent);
                updateTimer.Interval = 1000;
                updateTimer.Start();

                try
                {
                    uClient = listener.AcceptTcpClient();
                    listener.Stop();
                    background = new Thread(ReceiveData);
                    background.IsBackground = true;
                    background.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }



        /// <summary>
        /// 接收数据并解密
        /// </summary>
        void ReceiveData()
        {
            using (streamNet = uClient.GetStream())
            {
                byte[] fileLength = new byte[16];
                int lent = streamNet.Read(fileLength, 0, fileLength.Length);
                fileLen = BitConverter.ToInt64(file_aes.DecryptoBytes(fileLength), 0);

                AesCryptoServiceProvider decrypt = new AesCryptoServiceProvider();
                decrypt.IV = file_aes.IV;
                decrypt.Key = file_aes.Key;
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        using (var cstream = new CryptoStream(streamNet, decrypt.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            byte[] data = new byte[1024 * 40];
                            int readLen;
                            while ((readLen = cstream.Read(data, 0, data.Length)) > 0)
                            {
                                fs.Write(data, 0, readLen);
                                haveFinish += readLen;
                                int precent = (int)(haveFinish * 100 / fileLen);
                                pross_precent.Value = precent;
                                lb_percent.Text = precent.ToString() + "%";
                            }
                            cstream.FlushFinalBlock();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
            }
            if (haveFinish==fileLen)
            {
                uClient.Close();
                pross_precent.Value = 100;
                lb_percent.Text = "100%";

                finished = true;
                lkb_saveAs.Visible = false;
                lb_info.ForeColor = Color.Blue;
                lb_info.Text = "完成接收";
                lkb_refuse.Text = "打开";
                lkb_accept.Text = "打开文件夹";
                lkb_accept.Visible = true;
                lkb_refuse.Visible = true;
                FlashWindow(this.Handle, true);
                this.Focus();
                Thread.Sleep(300);
                SendMsg(20, 1);//返回给发送端完成发送标志
                updateTimer.Stop();
                CMainForm.PlaySound(7);
                try
                {
                    background.Abort();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
            else
            {
                Cancel();
            }
            
        }

        /******************************************以下为公共方法*******************************************/

        /// <summary>
        /// 发送信号
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="port"></param>
        private void SendMsg(int flag, int value)
        {
            byte[] toChat = Format.StoB(chatTo.UserName);

            byte[] hashString = RSAEncrytion.getHash(value.ToString());
            byte[] signature = RSAEncrytion.SignatureFormatter(rsaKeyString, hashString);//用自己的私钥签名
            byte[] msg = Format.StoB(value.ToString());

            byte[] msgTemp = new byte[msg.Length + 16 + 128];

            Buffer.BlockCopy(msg, 0, msgTemp, 0, msg.Length);
            Buffer.BlockCopy(hashString, 0, msgTemp, msgTemp.Length - 144, hashString.Length);
            Buffer.BlockCopy(signature, 0, msgTemp, msgTemp.Length - 128, signature.Length);
            byte[] msgEncrypted = RSAEncrytion.RSAEncrypt(msgTemp, chatTo.PubKey);//将签名后的信息用对方的公钥加密

            byte[] sendmsg = new byte[2 + toChat.Length + msgEncrypted.Length];
            sendmsg[0] = (byte)flag;
            sendmsg[1] = (byte)toChat.Length;
            Buffer.BlockCopy(toChat, 0, sendmsg, 2, toChat.Length);
            Buffer.BlockCopy(msgEncrypted, 0, sendmsg, 2 + toChat.Length, msgEncrypted.Length);
            byte[] toSend = aes.EncrypFromBytes(sendmsg);
            stream.Write(toSend, 0, toSend.Length);
        }



        #region 最小化出现在任务栏
        /// <summary>
        /// 最小化出现在任务栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileRequst_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;
            }
            else
            {
                this.ShowInTaskbar = false;
            }
        }
        #endregion

        /// <summary>
        /// 新消息任务栏闪动
        /// </summary>
        /// <param name="hWnd">handle   to   window</param>
        /// <param name="bInvert">flash   status</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        /// <summary>
        /// 取消任务
        /// </summary>
        internal void Cancel()
        {           
            finished = true;
            lb_info.ForeColor = Color.Red;
            lb_info.Text = "文件发送被取消";
            if (streamNet != null)
            {
                streamNet.Close();
                streamNet.Dispose();
            }
            if (background!=null)
            {
                background.Abort();
            }           
            FlashWindow(this.Handle, true);
            this.Focus();
            if (chat != null)
            {
                chat.tssl_info.ForeColor = Color.Red;
                chat.tssl_info.Text = "文件发送被取消";
                this.Close();
            }
        }


        /// <summary>
        /// 统计发送速度，发送量等
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private void TimerEventCurrent(Object myObject, EventArgs myEventArgs)
        {
            string total = null;
            string spreed = ((haveFinish - lastFinish) / 1024).ToString() + "K/s";
            if (fileLen < 1024)
            {
                if (haveFinish < fileLen)
                {
                    lb_current.Text = spreed + "(" + haveFinish + "/" + fileLen.ToString() + ")";
                }
                else
                {
                    lb_current.Text = fileLen.ToString();
                }

            }
            else if (fileLen < 1024 * 1024)
            {
                string temp = ((double)fileLen / 1024).ToString();
                total = temp.Substring(0, temp.IndexOf('.') + 2) + "KB";
                if (haveFinish < fileLen)
                {
                    lb_current.Text = spreed + " (" + haveFinish / 1024 + "/" + total + ")";
                }
                else
                {
                    lb_current.Text = total;
                }

            }
            else
            {
                string temp = ((double)fileLen / (1024 * 1024)).ToString();
                total = temp.Substring(0, temp.IndexOf('.') + 2) + "MB";
                if (haveFinish < fileLen)
                {
                    lb_current.Text = spreed + " (" + haveFinish / (1024 * 1024) + "/" + total + ")";
                }
                else
                {
                    lb_current.Text = total;
                }
            }
            lastFinish = haveFinish;
        }

        /// <summary>
        /// 关闭窗口，取消任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileRequst_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
