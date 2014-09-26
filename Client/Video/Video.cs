using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Client
{
    class Video
    {
        public bool flag = true;
        private IntPtr lwndC;       //保存无符号句柄
        private IntPtr mControlPtr; //保存管理指示器
        private int mWidth;
        private int mHeight;
        public delegate void RecievedFrameEventHandler(byte[] videoData);
        public event RecievedFrameEventHandler RecievedFrame;

        public VideoAPI.CAPTUREPARMS Capparms;
        private VideoAPI.FrameEventHandler mFrameEventHandler;
        public VideoAPI.CAPDRIVERCAPS CapDriverCAPS;//捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等；
        public VideoAPI.CAPSTATUS CapStatus;//该结构用于保存视频设备捕获窗口的当前状态，如图像的宽、高等
        string strFileName;
        public VideoAPI.BITMAPINFO bitmapInfo;

        EncoderParameters eps;//压缩参数
        ImageCodecInfo ici;//同上

        public Video(IntPtr handle, int width, int height)
        {
            CapDriverCAPS = new VideoAPI.CAPDRIVERCAPS();//捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等；
            CapStatus = new VideoAPI.CAPSTATUS();//该结构用于保存视频设备捕获窗口的当前状态，如图像的宽、高等
            mControlPtr = handle; //显示视频控件的句柄
            mWidth = width;      //视频宽度
            mHeight = height;    //视频高度
            ChangeQuality();
        }
        

        public bool StartWebCam()
        {
           return  StartWebCam(mWidth, mHeight);
        }
       /// <summary>
        ///  打开视频设备
       /// </summary>
        /// <param name="width">捕获窗口的宽度</param>
        /// <param name="height">捕获窗口的高度</param>
       /// <returns></returns>
        public bool StartWebCam(int width,int height)
        {
            this.lwndC = VideoAPI.capCreateCaptureWindow("", VideoAPI.WS_CHILD | VideoAPI.WS_VISIBLE, 0, 0, mWidth, mHeight, mControlPtr, 0);//AVICap类的捕捉窗口
            VideoAPI.FrameEventHandler FrameEventHandler = new VideoAPI.FrameEventHandler(FrameCallback);
            VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_CALLBACK_ERROR, 0, 0);//注册错误回调函数
            VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_CALLBACK_STATUS, 0, 0);//注册状态回调函数 
            VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);//注册视频流回调函数
            VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_CALLBACK_FRAME, 0, FrameEventHandler);//注册帧回调函数

            if (VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_DRIVER_CONNECT, 0, 0))
            {
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_DRIVER_GET_CAPS, VideoAPI.SizeOf(CapDriverCAPS), ref CapDriverCAPS);//获得当前视频 CAPDRIVERCAPS定义了捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等；
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_GET_STATUS, VideoAPI.SizeOf(CapStatus), ref CapStatus);//获得当前视频流的尺寸 存入CapStatus结构

                bitmapInfo = new VideoAPI.BITMAPINFO();//设置视频格式 (height and width in pixels, bits per frame). 
                bitmapInfo.bmiHeader = new VideoAPI.BITMAPINFOHEADER();
                bitmapInfo.bmiHeader.biSize = VideoAPI.SizeOf(bitmapInfo.bmiHeader);
                bitmapInfo.bmiHeader.biWidth = mWidth;
                bitmapInfo.bmiHeader.biHeight = mHeight;
                bitmapInfo.bmiHeader.biPlanes = 1;
                bitmapInfo.bmiHeader.biBitCount = 24;
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_PREVIEWRATE, 40, 0);//设置在PREVIEW模式下设定视频窗口的刷新率 设置每40毫秒显示一帧，即显示帧速为每秒25帧
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_SCALE, 1, 0);//打开预览视频的缩放比例
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_VIDEOFORMAT, VideoAPI.SizeOf(bitmapInfo), ref bitmapInfo);

                this.mFrameEventHandler = new VideoAPI.FrameEventHandler(FrameCallback);
                this.capSetCallbackOnFrame(this.lwndC, this.mFrameEventHandler);

                VideoAPI.CAPTUREPARMS captureparms = new VideoAPI.CAPTUREPARMS();
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_GET_SEQUENCE_SETUP, VideoAPI.SizeOf(captureparms), ref captureparms);
                if (CapDriverCAPS.fHasOverlay)
                {
                    VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_OVERLAY, 1, 0);//启用叠加 注：据说启用此项可以加快渲染速度    
                }
                VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_PREVIEW, 1, 0);//设置显示图像启动预览模式 PREVIEW
                VideoAPI.SetWindowPos(this.lwndC, 0, 0, 0, width, height, VideoAPI.SWP_NOZORDER | VideoAPI.SWP_NOMOVE);//使捕获窗口与进来的视频流尺寸保持一致
                return true;
            }
            else
            {              
                flag = false;
                return false;
            }
        }


        public bool   ReSizePic(int width, int height)
        {
            try
            {
                return VideoAPI.SetWindowPos(this.lwndC, 0, 0, 0, width, height, VideoAPI.SWP_NOZORDER | VideoAPI.SWP_NOMOVE) > 0;
            }
            catch
            {
                return false;
            }
        }


        public void get()
        {
            VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_GET_SEQUENCE_SETUP, VideoAPI.SizeOf(Capparms), ref Capparms);
        }
        public void set()
        {
            VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_SEQUENCE_SETUP, VideoAPI.SizeOf(Capparms), ref Capparms);
        }
        private bool capSetCallbackOnFrame(IntPtr lwnd, VideoAPI.FrameEventHandler lpProc)
        {
            return VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
        }


        /// <summary>
        /// 关闭视频设备
        /// </summary>
        public void CloseWebcam()
        {
            VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }
        

        #region 屏幕 拍照/录像 暂时无用
        ///   <summary>   
        ///   拍照
        ///   </summary>   
        ///   <param   name="path">要保存bmp文件的路径</param>   
        public void GrabImage(IntPtr hWndC, string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }


        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool StarKinescope(string path)
        {
            try
            {
                strFileName = path;
                string dir = path.Remove(path.LastIndexOf("\\"));
                if (!File.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                int hBmp = Marshal.StringToHGlobalAnsi(path).ToInt32();
                bool b = VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_FILE_SET_CAPTURE_FILE, 0, hBmp);
                b = b && VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_SEQUENCE, 0, 0);
                return b;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 停止录像
        /// </summary>
        public bool StopKinescope()
        {
            return VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_STOP, 0, 0);
        } 


        #endregion


        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="lwnd"></param>
        /// <param name="lpvhdr"></param>
        private void FrameCallback(IntPtr lwnd, IntPtr lpvhdr)
        {           
            ///通过回调函数返回数据还原bitmap 有点问题---〉当摄像头驱动为系统默认时捕获数据大小总为614400 16位 格式为YUY2 不可设置，为以后压缩扩展
            if (this.RecievedFrame != null)
            {
                VideoAPI.VIDEOHDR videoHeader = new VideoAPI.VIDEOHDR();
                byte[] VideoData;
                VideoAPI.CAPSTATUS g = CapStatus;
                videoHeader = (VideoAPI.VIDEOHDR)VideoAPI.GetStructure(lpvhdr, videoHeader);
                VideoData = new byte[videoHeader.dwBytesUsed];
                VideoAPI.Copy(videoHeader.lpData, VideoData);

            //    //////此方法生成倒立图像，只有宽640,高400可行
            //    //GCHandle hObject = GCHandle.Alloc(VideoData, GCHandleType.Pinned);
            //    //IntPtr pObject = hObject.AddrOfPinnedObject();
            //    //if (hObject.IsAllocated)
            //    //    hObject.Free();
            //    //Bitmap newBitmap = new Bitmap(mWidth, mHeight, VideoData.Length / 200, PixelFormat.Format24bppRgb, pObject);strid=videowidth*3=videodata.length/videoheight
            //    //this.RecievedFrame(newBitmap);

                //只有宽640,高400可行

                try
                {
                    IntPtr hDc = VideoAPI.GetDC(IntPtr.Zero);
                    IntPtr hBmp = VideoAPI.CreateDIBitmap(hDc, ref this.bitmapInfo.bmiHeader, 4, VideoData, ref this.bitmapInfo, 0);
                    VideoAPI.ReleaseDC(IntPtr.Zero, hDc);
                    Bitmap newBitmap = Bitmap.FromHbitmap(hBmp);
                    byte[] jpgData = imageToByteArray(newBitmap);
                    this.RecievedFrame(jpgData);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }


            ///通过剪贴板交换获取当前图片，不断发送以形成视频
            //if (this.RecievedFrame != null)
            //{
            //    VideoAPI.SendMessage(this.lwndC, VideoAPI.WM_CAP_EDIT_COPY, 0, 0);
            //    IDataObject obj1 = Clipboard.GetDataObject();
            //    if (obj1.GetDataPresent(typeof(Bitmap)))
            //    {
            //        Image image1 = (Image)obj1.GetData(typeof(Bitmap));
            //        if (image1!=null)
            //        {
            //            byte[] jpgData = imageToByteArray(image1);
            //            this.RecievedFrame(jpgData);
            //        }                    
            //    }
            //}
        }

        #region 视频图片Jpg压缩解压 ChangeQuality(long quality = 25L)/imageToByteArray(Image imageIn)/byteArrayToImage(byte[] Bytes)

        /// <summary>
        /// 调整视频质量
        /// </summary>
        /// <param name="quality"></param>
        public void ChangeQuality(long quality = 90L)
        {
            eps = new EncoderParameters(1);
            eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ici = GetEncoderInfo("image/jpeg");
        }
        /// <summary>
        /// 图片转为Byte字节数组
        /// </summary>
        /// <param name="FilePath">路径</param>
        /// <returns>字节数组</returns>
        private byte[] imageToByteArray(Image imageIn)
        {            
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    imageIn.Save(ms, ici, eps);
                    return ms.ToArray();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    return null;
                }
            }
        }
        /// <summary>
        /// 字节数组生成图片
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <returns>图片</returns>
        public static Image byteArrayToImage(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                Image outputImg = Image.FromStream(ms);
                return outputImg;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        #endregion
       
    }
}
