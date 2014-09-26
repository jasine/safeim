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
using System.Diagnostics;

namespace Client
{
    public partial class CMainForm : Form
    {
        internal TcpClient client = null;//连接套接字
        internal NetworkStream stream = null;//套接字读写流
        Thread mainConnThr;//后台与服务器交互线程
        bool serverState = true;//服务器状态
        internal Dictionary<string, UserInfo> onLineMember = new Dictionary<string, UserInfo>();//在线用户列表
        internal Dictionary<string, Chat> chatting = new Dictionary<string, Chat>();//正在聊天用户列表
        string userName;//自己用户名 
        string path = Application.StartupPath + "/Images/Color/";//头像文件夹路径
        internal List<string> videoChatting = null;
        internal List<string> voiceChatting = null;

        Video video;
        DirectSoundHelper sound;
        UdpCon udpViodeo = null;
        UdpCon udpVoice = null;
        Thread videoThread = null;
        Thread voiceThread = null;
        private Thread voiceCapThread;


        //NetChat netChat;//采集与播放声音

        internal AESEncrytion aes;
        internal string rsaKeyString;


        #region 构造方法 初始化变量 CMainForm(string userName, TcpClient client)
        /// <summary>
        /// 构造方法 初始化变量
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="client"></param>
        public CMainForm(string userName, TcpClient client, AESEncrytion aes, string rsa)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.client = client;
            this.userName = userName;
            this.aes = aes;
            stream = client.GetStream();

            this.Text = userName + " -SafeIM";
            rsaKeyString = rsa;

            mainConnThr = new Thread(TcpProcess);//处理与服务器TCP连接线程
            mainConnThr.IsBackground = true;
            mainConnThr.Start();

            //Thread udpConnThr = new Thread(UdpProcess);
            //udpConnThr.IsBackground = true;
            //udpConnThr.Start();

            lb_userName.Text = userName;


        }
        #endregion


        #region 与服务器交互与消息循环 void TcpProcess()
        /// <summary>
        /// 与服务器交互与消息循环
        /// </summary>
        private void TcpProcess()
        {
            try
            {
                while (true)
                {
                    byte[] msg = new byte[1024 * 2048];
                    int len = stream.Read(msg, 0, msg.Length);
                    byte[] temp = new byte[len];
                    Buffer.BlockCopy(msg, 0, temp, 0, len);
                    msg = aes.DecryptoBytes(temp);
                    len = msg.Length;
                    switch (msg[0])
                    {
                        case 3:
                            GetOnlineList(msg, len);
                            break;
                        case 4://添加上线用户                           
                            AddOnline(msg, len);
                            break;
                        case 5://移除下线用户
                            RemoveOnline(msg, len);
                            break;
                        case 8:
                            ReciveMsg(msg, len);
                            break;
                        case 10:
                            this.BringToFront();
                            serverState = false;
                            MessageBox.Show(this, "服务器关闭,您已经下线..");
                            this.Close();
                            break;
                        case 18:
                            ReciveMsg(msg, len);
                            break;
                        case 19:
                            ReciveMsg(msg, len);
                            break;
                        case 20:
                            ReciveMsg(msg, len);
                            break;
                        case 25:
                            ReciveMsg(msg, len);
                            break;
                        case 30:
                            ReciveMsg(msg, len);
                            break;
                        case 31:
                            ReciveMsg(msg, len);
                            break;
                        case 32:
                            ReciveMsg(msg, len);
                            break;
                        case 33:
                            ReciveMsg(msg, len);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        #endregion


        #region 获取在线列表 void GetOnlineList()
        /// <summary>
        /// 获取在线列表
        /// </summary>
        private void GetOnlineList(byte[] msg, int len)
        {
            string list = Format.BtoS(msg, 1, len - 1);
            string[] items = list.Split('$');
            //qqLstb_friend.Items.Add(new QQListBoxItem(true, 0,"在线用户", true));要实现分组必须重写QQListBox的XML存取
            for (int i = 1; i < items.Length; i++)
            {
                string[] item = items[i].Split('_');
                Image s = new Bitmap(path + item[2] + ".jpg");
                QQListBoxItem newItem = new QQListBoxItem(0, 0000, null, item[0], item[3], new Bitmap(s, 42, 42), true, true, true);
                UserInfo newUser = new UserInfo(item[0], IPAddress.Parse(item[1]), item[2], item[3], item[4], newItem);
                onLineMember.Add(item[0], newUser);
                if (item[0] == userName)
                {
                    byte[] arr = System.Text.Encoding.Default.GetBytes(item[3]);
                    if (arr.Length > 26)//处理个性签名过长
                    {
                        string str = System.Text.Encoding.Default.GetString(arr, 0, 22);
                        lb_selfintr.Text = str + "...";
                    }
                    else
                    {
                        lb_selfintr.Text = item[3];//加载用户签名
                    }
                    try
                    {
                        picb_selfHead.ImageLocation = path + item[2] + ".jpg";
                    }
                    catch (System.Exception)
                    {

                    }

                }
                else
                {
                    try
                    {
                        qqLstb_friend.Items.Add(newItem);

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }

            }
            //onlintListFinish = true;           
        }
        #endregion


        #region 用户上下线处理 void AddOnline(byte[] msg, int len)/RemoveOnline(byte[] msg, int len)
        /// <summary>
        /// 添加新上线用户
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="len"></param>
        private void AddOnline(byte[] msg, int len)
        {
            string list = Format.BtoS(msg, 1, len - 1);
            string[] item = list.Split('_');
            if (!onLineMember.ContainsKey(item[0]))
            {
                PlaySound(1);
                Image s = new Bitmap(path + item[2] + ".jpg");
                QQListBoxItem newItem = new QQListBoxItem(0, 0000, null, item[0], item[3], new Bitmap(s, 42, 42), true, true, true);
                qqLstb_friend.Items.Add(newItem);
                UserInfo newUser = new UserInfo(item[0], IPAddress.Parse(item[1]), item[2], item[3], item[4], newItem);
                onLineMember.Add(item[0], newUser);
            }
        }

        /// <summary>
        /// 移除下线用户
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="len"></param>
        private void RemoveOnline(byte[] msg, int len)
        {
            string userName = Format.BtoS(msg, 1, len - 1);
            if (onLineMember.ContainsKey(userName))
            {
                if (chatting.ContainsKey(userName))//关闭正在聊天
                {
                    chatting[userName].Close();
                    chatting.Remove(userName);
                }
                qqLstb_friend.Items.Remove(onLineMember[userName].Item);
                onLineMember.Remove(userName);
            }
        }
        #endregion


        #region 收到聊天消息处理 void ReciveMsg(byte[] msg, int len)
        /// <summary>
        /// 收到聊天消息处理
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="len"></param>
        private void ReciveMsg(byte[] msg, int len)
        {
            string toChating = Format.BtoS(msg, 2, msg[1]);
            //string content = Format.BtoS(msg, 2 + msg[1], len - 2 - msg[1]);
            byte[] encrypted = new byte[len - 2 - msg[1]];
            Buffer.BlockCopy(msg, 2 + msg[1], encrypted, 0, len - 2 - msg[1]);
            byte[] msgTemp= RSAEncrytion.RSADecrypt(encrypted, rsaKeyString);//用自己的私钥解密内容
            string content = Format.BtoS(msgTemp,0,msgTemp.Length-16-128);
            byte[] hashString=new byte[16];
            Buffer.BlockCopy(msgTemp,msgTemp.Length-16-128,hashString,0,16);
            byte[] signature =new byte[128];
            Buffer.BlockCopy(msgTemp,msgTemp.Length-128,signature,0,128);

            if (RSAEncrytion.SignatureDeformatter(onLineMember[toChating].PubKey,hashString,signature))//签名验证成功
            {
                if (msg[0] == 8 || msg[0] == 25 || msg[0] == 30)//文字消息或视频请求
                {
                    if (!chatting.ContainsKey(toChating))
                    {
                        List<object> canshu = new List<object>();
                        canshu.Add(userName);
                        canshu.Add(onLineMember[toChating]);

                        Thread newChat = new Thread(ExecCreateChat);
                        newChat.IsBackground = true;
                        newChat.Start(canshu);
                    }
                    while (!chatting.ContainsKey(toChating))//等待聊天窗口创建成功
                    { }
                    if (msg[0] == 8)
                    {
                        PlaySound(2);
                        Action<string> mi = new Action<string>(chatting[toChating].ReciveMsg);
                        BeginInvoke(mi, content); 
                        
                    }
                    else if (msg[0] == 25)
                    {
                        PlaySound(4);
                        Action<bool> mi = new Action<bool>(chatting[toChating].SharkWind);
                        Action<int> mt = new Action<int>(chatting[toChating].appendSharkMsg);
                        if (msg.Length > 3)
                        {
                            BeginInvoke(mt, 3);
                            BeginInvoke(mi, false);
                        }
                        else
                        {
                            BeginInvoke(mt, 2);
                            BeginInvoke(mi, true);
                        }
                    }
                    else if (msg[0] == 30)
                    {
                        PlaySound(5);
                        Action mi = new Action(chatting[toChating].VideoRequst);
                        BeginInvoke(mi);
                    }

                }
                else if (msg[0] == 18)//发送文件请求
                {
                    PlaySound(6);
                    List<object> canshu = new List<object>();
                    string fileName = Format.BtoS(msgTemp,0,msgTemp.Length-144 - 48 );
                    byte[] Iv = new byte[16];
                    byte[] Key = new byte[32];
                    Buffer.BlockCopy(msgTemp,msgTemp.Length - 48-144, Iv, 0, 16);
                    Buffer.BlockCopy(msgTemp, msgTemp.Length - 32-144, Key, 0, 32);
                    AESEncrytion file_aes = new AESEncrytion(Iv, Key);
                    canshu.Add(onLineMember[toChating]);
                    canshu.Add(client);
                    canshu.Add(fileName);
                    canshu.Add(aes);
                    canshu.Add(file_aes);
                    canshu.Add(rsaKeyString);

                    Thread newFile = new Thread(ExecCreateFile);
                    newFile.IsBackground = true;
                    newFile.Start(canshu);
                    Thread.Sleep(300);//等待聊天窗口创建成功
                }
                else if (msg[0] == 19)//同意接收或拒绝
                {
                    int port = -2;
                    try
                    {
                        port = int.Parse(content);
                    }
                    catch (Exception)
                    {
                    }
                    if (port == -1)
                    {
                        PlaySound(9);
                        if (chatting.ContainsKey(toChating))
                        {
                            Action mi = new Action(chatting[toChating].Messageform.Refuse);
                            BeginInvoke(mi);
                        }
                        else
                        {
                            MessageBox.Show("对方拒绝了您发送的文件");
                        }
                    }
                    else
                    {
                        Action<int> mi = new Action<int>(chatting[toChating].Messageform.Send);
                        BeginInvoke(mi, port);

                    }
                }
                else if (msg[0] == 20)//取消发送/接收
                {
                    int value = -1;
                    try
                    {
                        value = int.Parse(content);
                    }
                    catch (Exception)
                    {
                    }
                    if (value == 1)
                    {
                        PlaySound(7);
                        Action mi = new Action(chatting[toChating].Messageform.Finish);
                        BeginInvoke(mi);
                    }
                    else
                    {
                        PlaySound(9);
                        if (chatting.ContainsKey(toChating))
                        {
                            Action mi = new Action(chatting[toChating].Messageform.Cancel);
                            BeginInvoke(mi);
                        }

                    }
                }
                else if (msg[0] == 31)//接受视频
                {
                    //voiceThread = new Thread(StartVoice);
                    //voiceThread.IsBackground = true;
                    //voiceThread.Start(toChating);
                    //StartVoice(toChating);


                    string fileName = Format.BtoS(msgTemp, 0, msgTemp.Length - 144 - 48);
                    byte[] Iv = new byte[16];
                    byte[] Key = new byte[32];
                    Buffer.BlockCopy(msgTemp, msgTemp.Length - 48 - 144, Iv, 0, 16);
                    Buffer.BlockCopy(msgTemp, msgTemp.Length - 32 - 144, Key, 0, 32);

                    AESEncrytion audio_aes = new AESEncrytion(Iv, Key);
                    onLineMember[toChating].Audio_aes = audio_aes;
                    Action mi = new Action(chatting[toChating].AcceptVideo);
                    BeginInvoke(mi);

                    voiceThread = new Thread(StartVoice);
                    voiceThread.IsBackground = true;
                    voiceThread.Start(toChating);

                    videoThread = new Thread(StartCarmer);
                    videoThread.IsBackground = true;
                    videoThread.Start(toChating);


                    //Action<string> mi2 = new Action<string>(VideoChat);
                    //BeginInvoke(mi2, toChating);

                }
                else if (msg[0] == 32)//拒绝视频
                {
                    Action mi = new Action(chatting[toChating].RefuseVideo);
                    BeginInvoke(mi);
                }
                else if (msg[0] == 33)//挂断视频
                {
                    Action<bool> mi = new Action<bool>(chatting[toChating].StopVideo);
                    if (videoChatting.Contains(toChating))
                    {
                        BeginInvoke(mi, true);
                        CloseCarmer(toChating);
                        CloseVoice(toChating);
                    }
                    else
                    {
                        BeginInvoke(mi, false);
                    }
                }
            }
            else//签名验证失败
            {
                MessageBox.Show(chatting + " 的数字签名验证失败，请谨慎处理消息内容！", "警告",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        #endregion


        #region 开启聊天窗口 void ExecCreate(object obj) obj为Chat参数的List列表
        private void CreateChatForm(object list)
        {
            List<object> lt = list as List<object>;
            Chat f = new Chat(lt[0] as string, lt[1] as UserInfo, this as CMainForm);
            chatting.Add((lt[1] as UserInfo).UserName, f);
            f.Show();
            f.Activate();
            f.BringToFront();
        }

        private void ExecCreateChat(object obj)
        {
            Action<object> mi = new Action<object>(CreateChatForm);
            BeginInvoke(mi, obj);
        }
        #endregion


        #region 开启文件传送窗口 void ExecCreateFile(object obj) obj为FileRecv参数的List列表
        private void CreateFileForm(object list)
        {
            List<object> lt = list as List<object>;
            FileRequst messageform = new FileRequst(lt[0] as UserInfo, lt[1] as TcpClient, lt[2] as string, lt[3] as AESEncrytion, lt[4] as AESEncrytion,lt[5] as string);
            Point point = new Point(Screen.PrimaryScreen.WorkingArea.Width - messageform.Size.Width, Screen.PrimaryScreen.WorkingArea.Height - messageform.Size.Height);//窗体位置          
            messageform.StartPosition = FormStartPosition.Manual;//窗体其实位置类型，manual由location指定
            messageform.Location = point;
            messageform.Show();
        }

        private void ExecCreateFile(object obj)
        {
            Action<object> mi = new Action<object>(CreateFileForm);
            BeginInvoke(mi, obj);
        }
        #endregion


        #region 处理视频
        internal void VideoChat(string chatTo)
        {
            if (videoChatting == null)
            {
                //StartVoice(chatTo);
                video = new Video(chatting[chatTo].pic_vidSelf.Handle, 320, 240);
                udpViodeo = new UdpCon(UdpKind.videoMsg, onLineMember[chatTo].Audio_aes);
                udpViodeo.RecieveData += ReciveData;
                //打开视频
                if (video.StartWebCam(chatting[chatTo].pic_vidSelf.Width, chatting[chatTo].pic_vidSelf.Height))
                {
                    video.get();
                    video.Capparms.fYield = true;
                    video.Capparms.fAbortLeftMouse = false;
                    video.Capparms.fAbortRightMouse = false;
                    video.Capparms.fCaptureAudio = false;
                    video.Capparms.dwRequestMicroSecPerFrame = 66667; // 设定帧率25fps： 1*1000000/25 = 0x9C40
                    video.set();
                    videoChatting = new List<string>();
                    videoChatting.Add(chatTo);
                    video.RecievedFrame += video_RecievedFrame;
                }
                else
                {
                    try
                    {
                        udpViodeo.Close();
                        videoThread.Abort();
                        videoChatting.Remove(chatTo);
                    }
                    catch (Exception)
                    {
                    }
                    MessageBox.Show("打开视频设备失败，请检查设备与设置");
                    return;
                }
            }
            else
            {
                videoChatting.Add(chatTo);
                ////未处理多人视频自己
            }

        }

        private void StartCarmer(object obj)
        {
            string toChating = obj as string;
            Action<string> mi = new Action<string>(VideoChat);
            BeginInvoke(mi, toChating);

        }

        void video_RecievedFrame(byte[] videoData)
        {
            try
            {
                foreach (string chat in videoChatting)
                {
                    udpViodeo.SendData(videoData, onLineMember[chat].Address);
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("video_RecievedFrame " + e.ToString());
            }
        }


        private void ReciveData(byte[] data)
        {
            try
            {
                Image img = Video.byteArrayToImage(data);
                foreach (string chat in videoChatting)
                {
                    Action<Image> mi = new Action<Image>(chatting[chat].ChangeImgChaTo);
                    //chatting[chat].pic_vidChat.Image = img;
                    this.BeginInvoke(mi, img);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ReciveData " + e.ToString());
            }

        }


        internal void CloseCarmer(string toChatting)
        {
            try
            {
                videoChatting.Remove(toChatting);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            if (videoChatting.Count == 0)
            {
                try
                {
                    video.CloseWebcam();
                    udpViodeo.Close();
                    videoThread.Abort();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("CloseCarmer " + e.ToString());
                }
            }
        }
        #endregion



        //internal void StartVoice(object obj)
        //{
        //    string toChating = obj as string;
        //    //if (videoChatting == null)
        //    {
        //        netChat = new NetChat(23458);
        //        netChat.SetRemoteIPEnd(onLineMember[toChating].Address.ToString(), 23458);
        //        netChat.BindSelf(netChat.LocalIPEnd);
        //        try
        //        {
        //            netChat.Intptr = this.Handle;
        //            netChat.InitVoice();
        //        }
        //        catch (Exception e)
        //        {
        //            Debug.WriteLine("初始化失败 " + e.ToString());
        //            MessageBox.Show("初始化声音设备失败，请检查您的声音设备");
        //        }
        //        try
        //        {
        //            netChat.Listen();
        //        }
        //        catch (Exception e)
        //        {
        //            Debug.WriteLine("监听模块异常 " + e.ToString());
        //        }
        //        try
        //        {
        //            netChat.StartSendVoice();
        //        }
        //        catch (Exception e)
        //        {
        //            //if (MessageBox.Show("应用程序出现问题，需要重新启动应该程序吗？", "系统提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
        //            //{
        //            //    Application.Exit();//先关闭应用程序
        //            //    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //            //}
        //            Debug.WriteLine("未知错误 " + e.ToString());
        //        }
        //    }

        //}

        //internal void CloseVoice()
        //{
        //    try
        //    {
        //        netChat.Stop();
        //        voiceThread.Abort();
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.ToString());
        //    }
        //}

        internal void StartVoice(object obj)
        {
            string toChatting = obj as string;
            try
            {
                if (voiceChatting == null)
                {

                    voiceChatting = new List<string>();
                    sound = new DirectSoundHelper();
                    sound.OnBufferFulfill += new EventHandler(SendVoiceBuffer);
                    udpVoice = new UdpCon(UdpKind.voiceMsg, onLineMember[toChatting].Audio_aes);
                    udpVoice.RecieveData += RecieveSoundData;
                    voiceCapThread = new Thread(new ThreadStart(sound.StartCapturing));
                    voiceCapThread.IsBackground = true;
                    voiceCapThread.Start();
                    sound.StopLoop = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("您的声音设备打开失败，请检查");
                Debug.WriteLine(e.ToString());
                return;
            }
            voiceChatting.Add(toChatting);
        }

        private void RecieveSoundData(byte[] data)
        {
            try
            {
                sound.PlayReceivedVoice(data);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        void SendVoiceBuffer(object voiceBuffer, EventArgs e)
        {
            byte[] buffer = (byte[])voiceBuffer;
            try
            {
                foreach (string chat in voiceChatting)
                {
                    udpVoice.SendData(buffer, onLineMember[chat].Address);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("SendVoiceBuffer " + ex.ToString());
            }
        }

        internal void CloseVoice(string toChating)
        {
            try
            {
                voiceChatting.Remove(toChating);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            if (voiceChatting.Count == 0)
            {
                sound.StopLoop = true;
                udpVoice.Close();
                voiceCapThread.Abort();
                voiceThread.Abort();
            }
        }

        #region 双击在线列表事件 弹出聊天窗口处理
        /// <summary>
        /// 双击在线列表事件 弹出聊天窗口处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qqLstb_friend_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (qqLstb_friend.SelectedItem == null)
            {
                return;
            }
            else
            {
                QQListBoxItem item = qqLstb_friend.SelectedItem as QQListBoxItem;
                if (chatting.Keys.Contains(item.Remarks))
                {
                    chatting[item.Remarks].BringToFront();
                    return;
                }
                Chat chat = new Chat(userName, onLineMember[item.Remarks], this);
                chatting.Add(item.Remarks, chat);
                chat.Show();
            }
        }
        #endregion


        #region 播放声音 void PlaySound(int type)
        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="type">1:上线 2:用户消息 3:系统消息 4:窗口抖动 5:语音/视频</param>
        internal static void PlaySound(int type)
        {
            System.Media.SoundPlayer sound;
            switch (type)
            {
                case 1:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.Global);
                    break;
                case 2:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.msg);
                    break;
                case 3:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.system);
                    break;
                case 4:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.shake);
                    break;
                case 5:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.Audio);
                    break;
                case 6:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.Start);
                    break;
                case 7:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.Finish);
                    break;
                case 8:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.Waring);
                    break;
                case 9:
                    sound = new System.Media.SoundPlayer(global::Client.Properties.Resources.WaringSlight);
                    break;
                default:
                    return;
            }
            sound.Play();
        }
        #endregion


        #region 退出处理
        /// <summary>
        /// 退出处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (Chat chat in chatting.Values)
                {
                    chat.Close();
                }
                stream.Flush();
                if (serverState)
                {
                    byte[] sendMsg = new byte[1];
                    sendMsg[0] = 2;
                    byte[] toSend = aes.EncrypFromBytes(sendMsg);
                    stream.Write(toSend, 0, toSend.Length);
                }
                mainConnThr.Abort();
            }
            catch (Exception)
            {

                Application.Exit();//若有异常 终止线程
                //System.Environment.Exit(System.Environment.ExitCode);
            }
            finally
            {
                Application.Exit();//若有异常 终止线程
            }


        }
        #endregion


        private void lkb_about_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }


    }
}
