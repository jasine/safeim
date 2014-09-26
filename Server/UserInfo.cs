using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Base;

namespace Server
{
    class UserInfo
    {
        string userName;//用户名
        TcpClient client;//连接套接字
        NetworkStream stream;//套接字流
        EndPoint endPoint;//用户IP与端口
        IPAddress address;//用户IP
        Thread selfThread;//处理此用户信息的线程
        int headPic;//头像编号
        string selfIntr;//个性签名
        AESEncrytion aes;
        string pubKey;
        string priAndPubKey;

         #region 字段封装
        public UserInfo(string userName, TcpClient client, Thread selfThr)
        {
            UserName = userName;
            Client = client;
            SelfThread = selfThr;
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public TcpClient Client
        {
            get
            {
                return client;
            }
            set
            {
                EndPoint = value.Client.RemoteEndPoint;
                Stream = value.GetStream();
                client = value;
            }
        }

        public NetworkStream Stream
        {
            get { return stream; }
            private set { stream = value; }
        }

        public EndPoint EndPoint
        {
            get { return client.Client.RemoteEndPoint; }
            private set
            {
                endPoint = value;
                string ip = value.ToString();
                ip = ip.Remove(ip.IndexOf(':'));
                Address = IPAddress.Parse(ip);
            }
        }

        public IPAddress Address
        {
            get
            {
                return address;
            }
            private set
            {
                address = value;
            }
        }

        public Thread SelfThread
        {
            get { return selfThread; }
            private set { selfThread = value; }
        }

        public int HeadPic
        {
            get { return headPic; }
            set { headPic = value; }
        }

        public string SelfIntr
        {
            get { return selfIntr; }
            set { selfIntr = value; }
        }

        public AESEncrytion Aes
        {
            get { return aes; }
            set { aes = value; }
        }
        public string PubKey
        {
            get { return pubKey; }
            set { pubKey = value; }
        }
     
        public string PriAndPubKey
        {
            get { return priAndPubKey; }
            set { priAndPubKey = value; }
        }
        #endregion


    }
}
