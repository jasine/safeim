using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    class NetChat
    {
        private IPEndPoint ipeLocal;
        private Socket LocalSocket;
        private IPEndPoint ipeRemote;
        private int intMaxDataSize = 10000;//接收缓冲区长度
        private VoiceCapture voicecapture1 = new VoiceCapture();
        private IntPtr intptr;

        public IntPtr Intptr
        {
            set
            {
                intptr = value;
            }
        }

        public IPEndPoint LocalIPEnd
        {
            get { return ipeLocal; }
        }

        public NetChat(int intPort)
        {
            ipeLocal = new IPEndPoint(IPAddress.Any, intPort);//配置本地IP 和 端口
            LocalSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public NetChat(string strIP, int intPort)
        {
            ipeLocal = new IPEndPoint(IPAddress.Parse(strIP), intPort);//配置本地IP 和 端口
            LocalSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        /// 绑定自己的IP和端口
        /// </summary>
        /// <param name="ipe">IP端口对</param>
        /// <returns>绑定成功返回true</returns>
        public string  BindSelf(IPEndPoint ipe)
        {
            //while (true)
            //{
                try
                {
                    LocalSocket.Bind((EndPoint)ipeLocal);
                    return ipeLocal.Address.ToString()+" : "+ipeLocal.Port;
                }
                catch
                {
                    MessageBox.Show(ipeLocal.Port+"端口被占用");
                    return null;
                }
            //}

        }

        /// <summary>
        /// 设置远程IP端口节点
        /// </summary>
        /// <param name="strRemote">远程IP</param>
        /// <param name="intPort">远程端口</param>
        public void SetRemoteIPEnd(string strRemote, int intPort)
        {
            ipeRemote = new IPEndPoint(IPAddress.Parse(strRemote), intPort);
        }

        private Thread ListenThread;
        private byte[] bytData;

        /// <summary>
        /// 监听方法，用于监听远程发送到本机的信息
        /// </summary>
        public void Listen()
        {
            ListenThread = new Thread(new ThreadStart(DoListen));
            ListenThread.IsBackground = true;//设置为后台线程，这样当主线程结束后，该线程自动结束
            ListenThread.Start();
        }

        private EndPoint epRemote;

        /// <summary>
        /// 监听线程
        /// </summary>
        private void DoListen()
        {
            bytData = new byte[intMaxDataSize];
            epRemote = (EndPoint)(new IPEndPoint(IPAddress.Any, 0));

            //可能会抛出一个异常
            while (true)
            {
                if (LocalSocket.Poll(5000, SelectMode.SelectRead))
                {//每5ms查询一下网络，如果有可读数据就接收
                    LocalSocket.BeginReceiveFrom(bytData, 0, bytData.Length, SocketFlags.None, ref epRemote, new AsyncCallback(ReceiveData), null);
                }
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="iar"></param>
        private void ReceiveData(IAsyncResult iar)
        {
            int intRecv = 0;
            try
            {
                intRecv = LocalSocket.EndReceiveFrom(iar, ref epRemote);
            }
            catch
            {
                throw new Exception();
            }
            if (intRecv > 0)
            {
                byte[] bytReceivedData = new byte[intRecv];
                Buffer.BlockCopy(bytData, 0, bytReceivedData, 0, intRecv);
                
                voicecapture1.GetVoiceData(intRecv, bytReceivedData);//调用声音模块中的GetVoiceData来从字节数组中获取声音并播放
            }
        }

        public void InitVoice()
        {
            voicecapture1 = new VoiceCapture();
            voicecapture1.NotifyNum = 10;
            voicecapture1.NotifySize = 4410;
            voicecapture1.LocalSocket = LocalSocket;
            voicecapture1.RemoteEndPoint = (EndPoint)ipeRemote;
            voicecapture1.Intptr = intptr;
            voicecapture1.InitVoice();
        }

        public void StartSendVoice()
        {
            voicecapture1.StartVoiceCapture();
        }

        public void Stop()
        {
            if (ListenThread != null && ListenThread.IsAlive == true)
            {
                ListenThread.Abort();
            }
            voicecapture1.Stop();
        }
    }
}
