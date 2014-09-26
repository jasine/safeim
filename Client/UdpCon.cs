using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using Base;

namespace Client
{
    public enum UdpKind
    {
        //textMsg=23456,
        //fileMsg=23457,
        voiceMsg=23456,
        videoMsg=23458
    }
    class UdpCon
    {
        public delegate void RecieveDataEventHandler(byte[] data);
        public event RecieveDataEventHandler RecieveData;
        Int32 sendPort;
        Int32 recivePort;
        UdpClient udpClientRevc, udpClientSend;
        Thread thread;
        AESEncrytion audio_aes;

 
        public UdpCon(UdpKind kind,AESEncrytion aes)
        {
            sendPort = (int)kind;
            recivePort = sendPort + 1;
            audio_aes = aes;

            udpClientSend = new UdpClient(sendPort);
            udpClientRevc = new UdpClient(recivePort);
            thread = new Thread(new ThreadStart(Receive));
            if (sendPort==23456)
            {
                thread.Name = "voiceThread";
            }
            else
            {
                thread.Name = "videoThread";
            }
            thread.IsBackground = true;
            thread.Start();
        }



        internal void Close()
        {
            try
            {
                thread.Abort();
                udpClientRevc.Close();
                udpClientSend.Close();
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
                        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, sendPort);
                        byte[] content = udpClientRevc.Receive(ref remoteIPEndPoint);
                        if (content.Length > 0)
                        {                                                      
                            content=audio_aes.DecryptoBytes(content);
                            RecieveData(content);
                            Debug.WriteLine(thread.Name+" recive " + content.Length + "bytes data"+" port:"+sendPort);
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

        internal void SendData(byte[] data,IPAddress ip)
        {            
            if (thread.IsAlive==false)
            {
                thread = new Thread(new ThreadStart(Receive));
                if (sendPort == 23456)
                {
                    thread.Name = "voiceThread";
                }
                else
                {
                    thread.Name = "videoThread";
                }
                thread.IsBackground = true;
                thread.Start();
            }
            
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(ip, recivePort);
                data = audio_aes.EncrypFromBytes(data);
                int count = udpClientSend.Send(data, data.Length, ipEndPoint);
                if (count > 0)
                {
                    Debug.WriteLine(data.Length+"bytes data has been sent by "+thread.Name+" port:"+recivePort);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("SendData " + e.ToString());
            }
        }


    }
}
