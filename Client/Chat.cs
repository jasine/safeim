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
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography; //要用到系统API
using EmotionTest;
using CSharpWin;



namespace Client
{
    public partial class Chat : Form
    {
        internal UserInfo chatTo;//聊天对象
        internal string self;//自己
        int TextLength = 0;//聊天消息总长度
        string path = Application.StartupPath + "/Images/Color/";//头像文件夹路径
        internal FileInfo file = null;
        FileRequst messageform = null;
        internal CMainForm mainForm;
        bool videoChattng = false;
        FontDialog fontDialog;
        EmotionDropdown emotion;



        #region 封装字段
        public FileRequst Messageform
        {
            get { return messageform; }
        }
        #endregion

        #region 构造方法 变量赋值 Chat(string self, UserInfo chatTo,CMainForm mainForm)
        /// <summary>
        /// 构造方法 变量赋值
        /// </summary>
        /// <param name="self">自己用户名</param>
        /// <param name="chatTo">聊天对方名</param>
        /// <param name="mainForm">主窗口</param>
        public Chat(string self, UserInfo chatTo, CMainForm mainForm)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.chatTo = chatTo;
            this.self = self;
            this.Text = chatTo.UserName;
            this.mainForm = mainForm;

            lb_chatTo.Text = chatTo.UserName;
            lb_selfIntr.Text = chatTo.SelfIntr;
            try
            {
                picb_chatTo.ImageLocation = path + chatTo.HeadPic + ".jpg";
            }
            catch (System.Exception)
            {
            }
            fontDialog = new FontDialog();
            fontDialog.ShowColor = true;


            emotion = new EmotionDropdown();

            //获取点击的表情。
            emotion.EmotionContainer.ItemClick += delegate(
                object sender, EmotionItemMouseClickEventArgs e)
            {
                Image img = e.Item.Image;
                Clipboard.SetDataObject(img);
                tb_send.Paste(DataFormats.GetFormat(DataFormats.Bitmap)); //加图片
                tb_send.Focus();
            };

            btn_faces.Click += delegate(object sender, EventArgs e)
             {
                 emotion.Show(btn_faces);
             };

        }


        #endregion


        #region 消息接收与发送 void ReciveMsg(string msg)/void SendMsg()
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        internal void ReciveMsg(string msg)
        {
            if (!this.Focused)
            {
                FlashWindow(this.Handle, true);
            }
            rtb_content.AppendTextAsRtf(chatTo.UserName + "  " + DateTime.Now.ToShortTimeString() + '\n',
                    new Font("宋体", 9), RtfColor.Blue);
            rtb_content.AppendTextAsRtf("   ");
            rtb_content.AppendRtf(msg);
            rtb_content.ScrollToCaret();
            // Return focus to message text box
            tb_send.Focus();
            //AppendContent(chatTo.UserName, msg);                       
        }

        /// <summary>
        /// 新消息任务栏闪动
        /// </summary>
        /// <param name="hWnd">handle   to   window</param>
        /// <param name="bInvert">flash   status</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);


        /// <summary>
        /// 按钮发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            //Action<int, string,AESEncrytion> m = new Action<int, string,AESEncrytion>(SendMsg);
            //BeginInvoke(m, 8, tb_send.Text,null);
            ////SendMsg(8, tb_send.Text.Trim());

            //Action<string, string> mi = new Action<string, string>(AppendContent);
            //BeginInvoke(mi, self, tb_send.Text);
            ////AppendContent(self, tb_send.Text);

            //tb_send.Text = "";


            try
            {
                Action<int, string, AESEncrytion> m = new Action<int, string, AESEncrytion>(SendMsg);
                BeginInvoke(m, 8, tb_send.Rtf, null);

                // Add fake message owner using insert
                rtb_content.AppendTextAsRtf(self + "  " + DateTime.Now.ToShortTimeString() + '\n',
                    new Font("宋体", 9), RtfColor.Green);

                // Just to show it's possible, if the text contains a smiley face [:)]
                // insert the smiley image instead. This is not a practical way to do this.
                //int _index;
                //if ((_index = rtbox_SendMessage.Find(":)")) > -1) {
                //    rtbox_SendMessage.Select(_index, ":)".Length);
                //    rtbox_SendMessage.InsertImage(new Bitmap(typeof(IMWindow), "Emoticons.Beer.png"));
                //}

                // Add the message to the history
                rtb_content.AppendTextAsRtf("   ");
                rtb_content.AppendRtf(tb_send.Rtf);

                // Add a newline below the added line, just to add spacing
                //rtb_content.AppendTextAsRtf("\n");

                // History gets the focus
                rtb_content.Focus();

                // Scroll to bottom so newly added text is seen.
                rtb_content.Select(rtb_content.TextLength, 0);
                rtb_content.ScrollToCaret();

                // Return focus to message text box
                tb_send.Focus();

                // Add the Rtf Codes to the RtfCode Window
                //frm_RtfCodes.AppendText(rtbox_SendMessage.Rtf);

                // Clear the SendMessage box.
                tb_send.Text = String.Empty;
            }
            catch (Exception _e)
            {
                MessageBox.Show("An error occured when \"sending\" :\n\n" +
                    _e.Message, "Send Error");
            }



        }

        /// <summary>
        /// 回车发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == '\r')
            {
                try
                {
                    Action<int, string, AESEncrytion> m = new Action<int, string, AESEncrytion>(SendMsg);
                    BeginInvoke(m, 8, tb_send.Rtf, null);

                    // Add fake message owner using insert
                    rtb_content.AppendTextAsRtf(self + "  " + DateTime.Now.ToShortTimeString() + '\n',
                        new Font("宋体", 9), RtfColor.Green);

                    // Just to show it's possible, if the text contains a smiley face [:)]
                    // insert the smiley image instead. This is not a practical way to do this.
                    //int _index;
                    //if ((_index = rtbox_SendMessage.Find(":)")) > -1) {
                    //    rtbox_SendMessage.Select(_index, ":)".Length);
                    //    rtbox_SendMessage.InsertImage(new Bitmap(typeof(IMWindow), "Emoticons.Beer.png"));
                    //}

                    // Add the message to the history
                    rtb_content.AppendTextAsRtf("   ");
                    rtb_content.AppendRtf(tb_send.Rtf);

                    // Add a newline below the added line, just to add spacing
                    //rtb_content.AppendTextAsRtf("\n");

                    // History gets the focus
                    rtb_content.Focus();

                    // Scroll to bottom so newly added text is seen.
                    rtb_content.Select(rtb_content.TextLength, 0);
                    rtb_content.ScrollToCaret();

                    // Return focus to message text box
                    tb_send.Focus();

                    // Add the Rtf Codes to the RtfCode Window
                    //frm_RtfCodes.AppendText(rtbox_SendMessage.Rtf);

                    // Clear the SendMessage box.
                    // tb_send.Text = String.Empty;
                    //tb_send.Text = "";
                }
                catch (Exception _e)
                {
                    MessageBox.Show("An error occured when \"sending\" :\n\n" +
                        _e.Message, "Send Error");
                }
            }
        }

        private void tb_send_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == '\r')
            {
                tb_send.Text = "";
            }
        }


        /// <summary>
        /// 发送消息处理
        /// </summary>
        private void SendMsg(int flag, string content, AESEncrytion newAes = null)
        {

            if (mainForm.chatting.Keys.Contains(chatTo.UserName))
            {
                content.Trim();
                byte[] hashString=RSAEncrytion.getHash(content);
                byte[] signature = RSAEncrytion.SignatureFormatter(mainForm.rsaKeyString, hashString);//用自己的私钥签名
                byte[] msg = Format.StoB(content);
                byte[] msgTemp ;
                if(newAes==null)
                {
                    msgTemp= new byte[msg.Length + 16 + 128];
                }
                else
                {
                    msgTemp = new byte[msg.Length + 16 + 128 + 48];
                    Buffer.BlockCopy(newAes.IV, 0, msgTemp, msgTemp.Length - 48-144, 16);
                    Buffer.BlockCopy(newAes.Key, 0, msgTemp, msgTemp.Length - 32-144, 32);
                }
                Buffer.BlockCopy(msg,0,msgTemp,0,msg.Length);
                Buffer.BlockCopy(hashString,0,msgTemp,msgTemp.Length-144,hashString.Length);
                Buffer.BlockCopy(signature,0,msgTemp,msgTemp.Length-128,signature.Length);
                byte[] msgEncrypted = RSAEncrytion.RSAEncrypt(msgTemp, chatTo.PubKey);//将签名后的信息用对方的公钥加密

                byte[] toChat = Format.StoB(chatTo.UserName);


                byte[] sendmsg=new byte[2 + toChat.Length + msgEncrypted.Length];
                /*if (newAes == null)
                {
                    sendmsg = new byte[2 + toChat.Length + msgEncrypted.Length];
                }
                else
                {
                    sendmsg = new byte[2 + toChat.Length + msgEncrypted.Length + 48];
                    Buffer.BlockCopy(newAes.IV, 0, sendmsg, sendmsg.Length - 48, 16);
                    Buffer.BlockCopy(newAes.Key, 0, sendmsg, sendmsg.Length - 32, 32);
                }*/


                Buffer.BlockCopy(toChat, 0, sendmsg, 2, toChat.Length);
                Buffer.BlockCopy(msgEncrypted, 0, sendmsg, 2 + toChat.Length, msgEncrypted.Length);

                sendmsg[0] = (byte)flag;
                sendmsg[1] = (byte)toChat.Length;
                byte[] toSend = mainForm.aes.EncrypFromBytes(sendmsg);


                try
                {
                    mainForm.stream.Write(toSend, 0, toSend.Length);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
            else
            {
                MessageBox.Show(this, "用户已经下线..");//如要实现离线消息，去掉此判断
            }
        }
        #endregion


        #region 向文本框添加发送与接收消息 void AppendContent(string user, string content)
        /// <summary>
        /// 向文本框添加发送与接收消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="content"></param>
        private void AppendContent(string user, string content)
        {
            string txt1 = user + " " + DateTime.Now.ToShortTimeString() + "\r\n";
            rtb_content.AppendText(txt1);
            rtb_content.SelectionStart = TextLength;
            rtb_content.SelectionLength = txt1.Length;
            if (user == self)
                rtb_content.SelectionColor = Color.Orange;
            else
                rtb_content.SelectionColor = Color.Green;
            rtb_content.SelectionFont = new Font("宋体", 9);

            TextLength += txt1.Length - 2;


            string txt2 = "   " + content + "\r\n";

            rtb_content.AppendText(txt2);
            rtb_content.SelectionStart = TextLength;
            rtb_content.SelectionLength = txt2.Length;
            if (user == self)
                rtb_content.SelectionColor = Color.Black;
            else
                rtb_content.SelectionColor = Color.Blue;
            rtb_content.SelectionFont = new Font("楷体", 11);

            TextLength += txt2.Length;

            rtb_content.ScrollToCaret();//文本框滚动到最后
        }
        #endregion


        #region 退出处理
        /// <summary>
        /// 退出处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.chatting.Remove(chatTo.UserName);
        }
        #endregion


        #region 发送文件请求，创建文件发送面板
        /// <summary>
        /// 点击发送文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sendFile_Click(object sender, EventArgs e)
        {
            //int count = 0;//用于技术打开文件发送/接收窗口个数，有问题
            //while (FindWindow("nul", "发送文件") != 0)
            //    count++; 
            //if (messageform == null)
            //{
            OpenFileDialog chooser = new OpenFileDialog();
            chooser.Multiselect = false;
            chooser.Title = "选择发送文件";
            if (chooser.ShowDialog() == DialogResult.OK)
            {
                file = new FileInfo(chooser.FileName);
                AESEncrytion file_aes = new AESEncrytion();
                SendMsg(18, file.Name, file_aes);
                messageform = new FileRequst(this, file_aes,mainForm.rsaKeyString);
                Point point = new Point(Screen.PrimaryScreen.WorkingArea.Width - messageform.Size.Width, Screen.PrimaryScreen.WorkingArea.Height - messageform.Size.Height);//窗体位置          
                messageform.StartPosition = FormStartPosition.Manual;//窗体其实位置类型，manual由location指定
                messageform.Location = point;
                messageform.Show();
            }

            //}
            //else
            //{
            //MessageBox.Show(this, "当前有向该好友的传输任务，请传送完成再试");
            //}

        }

        //用于计数打开文件发送/接收窗口个数，有问题
        //[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //static extern int FindWindow(string lpszClass, string lpszWindow); 
        #endregion





        /// <summary>
        /// 发出视频请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_video_Click(object sender, EventArgs e)
        {
            if (!chatTo.Address.Equals(mainForm.onLineMember[self].Address))
            {
                SendMsg(30, "", null);
                CMainForm.PlaySound(5);
                btn_video.Enabled = false;
                btn_voice.Enabled = false;
                btn_vidAccept.Visible = false;
                btn_vidRefuse.Visible = false;
                btn_vidStop.Visible = true;
                tssl_info.Text = "发出视频请求，等待" + chatTo.UserName + "回应..";
                OpenExtraPanel();
            }
            else
            {
                MessageBox.Show("请不要与自己视频");
            }
        }

        /// <summary>
        /// 收到视频请求
        /// </summary>
        internal void VideoRequst()
        {
            OpenExtraPanel();//return;//改变面板大小和按钮可见性
            btn_video.Enabled = false;
            btn_voice.Enabled = false;
            btn_vidAccept.Visible = true;
            btn_vidRefuse.Visible = true;
            btn_vidStop.Visible = false;
            tssl_info.Text = "收到视频" + chatTo.UserName + "的视频请求..";
        }

        /// <summary>
        /// 自己接受视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_vidAccept_Click(object sender, EventArgs e)
        {
            AESEncrytion aesVideo = new AESEncrytion();
            SendMsg(31, "", aesVideo);
            mainForm.onLineMember[chatTo.UserName].Audio_aes = aesVideo;
            mainForm.VideoChat(chatTo.UserName);
            mainForm.StartVoice(chatTo.UserName);
            btn_vidStop.Visible = true;
            btn_vidAccept.Visible = false;
            btn_vidRefuse.Visible = false;
            videoChattng = true;
        }

        /// <summary>
        /// 对方接收视频
        /// </summary>
        internal void AcceptVideo()
        {
            videoChattng = true;
            tssl_info.Text = "正在与" + chatTo.UserName + "视频通话..";
        }

        /// <summary>
        /// 自己拒绝视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_vidRefuse_Click(object sender, EventArgs e)
        {
            SendMsg(32, "", null);
            btn_video.Enabled = true;
            btn_voice.Enabled = true;
            tssl_info.Text = "拒绝" + chatTo.UserName + "的视频请求..";
            CloseExtraPanel();
        }

        /// <summary>
        /// 对方拒绝视频
        /// </summary>
        internal void RefuseVideo()
        {
            btn_video.Enabled = true;
            btn_voice.Enabled = true;
            CloseExtraPanel();
            tssl_info.Text = chatTo.UserName + "拒绝了您的视频请求..";
        }

        /// <summary>
        /// 自己结束视频/取消视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_vidStop_Click(object sender, EventArgs e)
        {
            Action<int, string, AESEncrytion> m = new Action<int, string, AESEncrytion>(SendMsg);
            BeginInvoke(m, 33, "", null);
            btn_video.Enabled = true;
            btn_voice.Enabled = true;
            if (videoChattng)
            {
                tssl_info.Text = "已经断开与" + chatTo.UserName + "的视频通话..";
                Action<string> mi = new Action<string>(mainForm.CloseCarmer);
                BeginInvoke(mi, chatTo.UserName);
                Action<string> mi2 = new Action<string>(mainForm.CloseVoice);
                BeginInvoke(mi2, chatTo.UserName);
            }
            else
            {
                tssl_info.Text = "已经取消与" + chatTo.UserName + "的视频通话..";
            }
            CloseExtraPanel();
        }

        /// <summary>
        /// 对方结束视频
        /// </summary>
        /// <param name="flag"></param>
        internal void StopVideo(bool flag)
        {
            CloseExtraPanel();
            btn_video.Enabled = true;
            btn_voice.Enabled = true;
            if (flag)
            {
                tssl_info.Text = chatTo.UserName + "断开与您的视频通话..";
            }
            else
            {
                tssl_info.Text = chatTo.UserName + "取消与您的视频通话..";
            }

        }



        /// <summary>
        /// 发起语音请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_voice_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 视频面板显示视频数据
        /// </summary>
        /// <param name="img"></param>
        internal void ChangeImgChaTo(Image img)
        {
            this.pic_vidChat.Image = img;
        }

        /// <summary>
        /// 打开视频/语音面板
        /// </summary>
        private void OpenExtraPanel()
        {
            splitCont_main.Panel2Collapsed = false;

            if (this.Width < 820)
            {
                this.Width = 820;
            }
            splitCont_main.SplitterDistance = this.Width / 2;
            splitCont_main.Panel1MinSize = 410;
            splitCont_main.Panel2MinSize = 410;
            this.MinimumSize = new Size(this.Width, this.Height);
        }

        /// <summary>
        /// 关闭扩展面板
        /// </summary>
        private void CloseExtraPanel()
        {
            splitCont_main.Panel2Collapsed = true;
            this.Width = this.Width / 2;
            MinimumSize = new Size(270, 330);
        }

        private void btn_font_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fontDialog.ShowDialog())
            {
                tb_send.Font = fontDialog.Font;
                tb_send.ForeColor = fontDialog.Color;
            }
        }


        public void btn_copyscreen_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));
            frmScreen f = new frmScreen(bmp);
            if (f.ShowDialog()==DialogResult.OK)
            {
                tb_send.Paste(DataFormats.GetFormat(DataFormats.Bitmap)); //加图片
                tb_send.Focus();
            }     
        }


        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^V ");
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^X ");
        }



        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_content.Clear();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtb_content.ZoomFactor = 0.75F;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rtb_content.ZoomFactor = 1.0F;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtb_content.ZoomFactor = 1.5F;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            rtb_content.ZoomFactor = 2.0F;
        }

        internal void SharkWind(bool type)
        {
            if (this.WindowState==FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
            tb_send.Focus();
            CMainForm.PlaySound(4);
            int times = 200;
            int rand = 20;         //抖动的幅度
            if (type==false)
            {
                times = 1000;
                rand = 150;
            }
            int recordx = this.Left; //保存原来窗体的左上角的x坐标
            int recordy = this.Top; //保存原来窗体的左上角的y坐标           
            Random random = new Random();
            for (int i = 0; i < times; i++)     //抖动的次数
            {
                int x = random.Next(rand);
                int y = random.Next(rand);
                if (x % 2 == 0)
                {
                    this.Left = this.Left + x;
                }
                else
                {
                    this.Left = this.Left - x;
                }
                if (y % 2 == 0)
                {
                    this.Top = this.Top + y;
                }
                else
                {
                    this.Top = this.Top - y;
                }
                this.Left = recordx; //还原原始窗体的左上角的x坐标
                this.Top = recordy; //还原原始窗体的左上角的y坐标
            }
        }

        private void btn_shark_MouseDown(object sender, MouseEventArgs e)
        {
            appendSharkMsg(1);
            Action<int, string, AESEncrytion> m = new Action<int, string, AESEncrytion>(SendMsg);
            SharkWind(true);
            if (e.Button == MouseButtons.Left)
            {
                BeginInvoke(m, 25, "", null);
            }
            else
            {
                BeginInvoke(m, 25, "false", null);
            }
        }

        /*internal void SignatureChack(bool statue)
        {
            if(statue)
            {
                tssl_info.ForeColor = Color.LawnGreen;
                tssl_info.Text = "您的聊天数据经过加密与数字签名验证";
            }
            else
            {
                tssl_info.ForeColor = Color.Red;
                tssl_info.Text="本次的聊天信息未通过数字签名验证，请谨慎处理"
            }
        }*/

        internal void appendSharkMsg(int type)
        {
            if (type==1)
            {
                rtb_content.AppendTextAsRtf(DateTime.Now.ToShortTimeString() + '\n',
                        new Font("宋体", 9), RtfColor.Green);
                rtb_content.AppendTextAsRtf("    您发送了一个窗口抖动\n",
                        new Font("宋体", 10), RtfColor.Gray);
            }
            else
            {
                rtb_content.AppendTextAsRtf(DateTime.Now.ToShortTimeString() + '\n',
                        this.Font, RtfColor.Blue);
                if (type==2)
                {
                    rtb_content.AppendTextAsRtf("   " + chatTo.UserName + "向您发送了一个窗口抖动\n",
                           new Font("宋体", 10), RtfColor.Gray);
                }
                else
                {
                    rtb_content.AppendTextAsRtf("   " + chatTo.UserName + "向您发送了一个暴力窗口抖动\n",
                           new Font("宋体", 10), RtfColor.Gray);
                }
                
            }
        }

        

        








    }
}
