//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Diagnostics;

//namespace Client
//{
//    public enum UdpKind
//    {
//        textMsg = 23456,
//        fileMsg = 23457,
//        voiceMsg = 23458,
//        videoMsg = 23459
//    }
//    class TcpCon
//    {
//        public delegate void RecieveDataEventHandler(byte[] data);
//        public event RecieveDataEventHandler RecieveData;
//        Int32 sendPort = 23456;
//        Int32 recivePort = 23457;
//        TcpClient clientRevc, clientSend;
//        Thread thread;


//        public TcpCon()
//        {
//            clientRevc = new TcpClient();
//            clientSend = new TcpClient();
//            thread = new Thread(new ThreadStart(Receive));
//            thread.IsBackground = true;
//            thread.Start();
//        }



//        internal void Close()
//        {
//            try
//            {
//                thread.Abort();
//                clientRevc.Close();
//                clientSend.Close();
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine("Close " + e.ToString());
//            }
//        }

//        private void Receive()
//        {

//            if (RecieveData != null)
//            {
//                while (true)
//                {
//                    try
//                    {
//                        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, sendPort);
//                        byte[] content = clientRevc.Receive(ref remoteIPEndPoint);
//                        if (content.Length > 0)
//                        {
//                            RecieveData(content);
//                            Debug.WriteLine("recive " + content.Length + "bytes data");
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        Debug.WriteLine("Recive " + e.ToString());
//                    }
//                }
//            }
//        }


//        /////////////////////////////////////////////////////////////////////////////////

//        internal void SendData(byte[] data, IPAddress ip)
//        {
//            IPEndPoint ipEndPoint = new IPEndPoint(ip, recivePort);
//            try
//            {
//                int count = clientSend.Send(data, data.Length, ipEndPoint);
//                if (count > 0)
//                {
//                    Debug.WriteLine(data.Length + "bytes data has been sent.");
//                }
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine("SendData " + e.ToString());
//            }
//        }


//    }
//}
