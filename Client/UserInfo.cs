using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Base;

namespace Client
{
    public class UserInfo
    {
        string userName;//用户名
        NetworkStream stream;//套接字流
        IPAddress address;//用户IP
        string headPic;//头像编号
        string selfIntr;//个性签名
        QQListBoxItem item;
        AESEncrytion audio_aes;
        string pubKey;

        

        #region 字段封装
        public UserInfo(string userName,IPAddress address,string headPic,string selfIntr,string pubKey,QQListBoxItem item)
        {
            UserName = userName;
            Address = address;
            HeadPic = headPic;
            SelfIntr = selfIntr;
            Item = item;
            PubKey = pubKey;
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        public NetworkStream Stream
        {
            get { return stream; }
            private set { stream = value; }
        }


        public IPAddress Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }


        public string HeadPic
        {
            get { return headPic; }
            set { headPic = value; }
        }

        public string SelfIntr
        {
            get { return selfIntr; }
            set { selfIntr = value; }
        }

        public QQListBoxItem Item
        {
            get { return item; }
            set { item = value; }
        }

        public AESEncrytion Audio_aes
        {
            get { return audio_aes; }
            set { audio_aes = value; }
        }

        public string PubKey
        {
            get { return pubKey; }
            set { pubKey = value; }
        }

        #endregion
    }
}
