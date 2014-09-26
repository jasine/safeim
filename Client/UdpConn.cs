using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Client
{
    public enum MsgKind
    {
        textMsg = 23456,
        fileMsg = 23457,
        voiceMsg = 23458,
        videoMsg = 23459
    }
    class UdpConn
    {
        public delegate void RecieveDataEventHandler(byte[] data,IPAddress address);
        public event RecieveDataEventHandler RecieveData;
        readonly Int32 port;
        UdpClient client;
        Thread thread;


        public UdpConn(MsgKind kind)
        {
            port = (int)kind;
            client = new UdpClient(port);
            thread = new Thread(new ThreadStart(Receive));
            thread.IsBackground = true;
            thread.Start();
        }



        internal void Close()
        {
            try
            {
                thread.Abort();
                client.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Close " + e.ToString());
            }
        }

        private void Receive()
        {

            if (RecieveData != null)
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, port);
                        byte[] content = client.Receive(ref remoteIPEndPoint);
                        if (content.Length > 0)
                        {
                            RecieveData(content,remoteIPEndPoint.Address);
                            Debug.WriteLine("recive " + content.Length + "bytes data");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Recive " + e.ToString());
                    }
                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////

        internal void SendData(byte[] data, IPAddress ip)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
            try
            {
                int count = client.Send(data, data.Length, ipEndPoint);
                if (count > 0)
                {
                    Debug.WriteLine(data.Length + "bytes data has been sent.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("SendData " + e.ToString());
            }
        }


    }
}