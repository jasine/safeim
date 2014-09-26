using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
    public class Format
    {
        #region 将字符串转为字符数byte[] StoB()
        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="s">要转换字符串</param>
        /// <returns>对应的字节数组</returns>
        public static byte[] StoB(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="flag">索引0中的标志位</param>
        /// <param name="str">索引0以后要转换的字符串</param>
        /// <returns></returns>
        public static byte[] StoB(int flag,string str)
        {           
            byte[] temp= System.Text.Encoding.UTF8.GetBytes(str);
            byte[] back = new byte[temp.Length + 1];
            back[0] =(byte) flag;
            Buffer.BlockCopy(temp, 0, back, 1, temp.Length);
            return back;

        }
        # endregion


        #region 将字节数字转为字符串 string BtoS()
        /// <summary>
        /// 将字节数组转为字符串
        /// </summary>
        /// <param name="bytes">输入字节数组</param>
        /// <returns>对应字符串</returns>
        public static string BtoS(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes).TrimEnd('\0');
        }
        /// <summary>
        /// 从数组开头将字节数组转为字符串
        /// </summary>
        /// <param name="bytes">输入字节数组</param>
        /// <param name="end">转换的长度</param>
        /// <returns>对应字符串</returns>
        public static string BtoS(byte[] bytes,int count)
        {
            return System.Text.Encoding.UTF8.GetString(bytes, 0, count);
        }
        /// <summary>
        /// 将字节数组转为字符串
        /// </summary>
        /// <param name="bytes">输入字节数组</param>
        /// <param name="start">开始转换位置索引</param>
        /// <param name="end">转换长度 -1:转换到字节数组末尾</param>
        /// <returns>对应字符串</returns>
        public static string BtoS(byte[] bytes,int start,int count)
        {
            return System.Text.Encoding.UTF8.GetString(bytes, start, count);
        }
        #endregion

        /// <summary>
        /// 为发送的数据设置标志位
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] SetFlag(int flag,byte[] source)
        {
            byte[] back = new byte[source.Length + 1];
            back[0] = (byte)flag;
            Buffer.BlockCopy(source,0,back,1,source.Length);
            return back;
        }
        public static  byte[] RemoveFlag(byte[] source)
        {
            byte [] back=new byte [source.Length-1];
            Buffer.BlockCopy(source, 1, back, 0, back.Length);
            return back;
        }
        public static byte[] RemoveFlag(byte[] source,int length)
        {
            byte[] back = new byte[length - 1];
            Buffer.BlockCopy(source, 1, back, 0, back.Length);
            return back;
        }

    }
}
