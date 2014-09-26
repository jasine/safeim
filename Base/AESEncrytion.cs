using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Base
{
    public class AESEncrytion
    {
        public AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        private byte[] iv;
        private byte[] key;

        #region 封装字段
        public byte[] IV
        {
            get { return iv; }
            private set { iv = value; }
        }


        public byte[] Key
        {
            get { return key; }
            private set { key = value; }
        }
        #endregion

        public AESEncrytion()
        {
            IV = aes.IV;
            Key = aes.Key;
        }



        /// <summary>
        /// 从字符串设置密钥和IV向量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public void SetKeyAndIv(string iv,string key)
        {
            byte[] tmp1 = new byte[16];
            byte[] tmp2 = new byte[32];
            for (int i = 0; i < 16; i++)
                tmp1[i] = 0;
            for (int j = 0; j < 32; j++)
                tmp2[j] = 0;
            byte[] Iv =(Format.StoB(iv)); 
            byte[] Key =(Format.StoB(key));
            
            Buffer.BlockCopy(Iv,0, tmp1, 0,Iv.Length);
            Buffer.BlockCopy(Key,0,tmp2, 0, Key.Length);
            this.IV = tmp1;
            this.Key = tmp2;

        }


        public AESEncrytion(byte[] Iv,byte[] key)
        {
            this.IV = Iv;
            this.Key =key;
        }


        public static byte[] EncryptFromString(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static byte[] EncryptFromBytes(byte[] src, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var ms = new MemoryStream())
                using (var cstream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cstream.Write(src, 0, src.Length);

                    cstream.Close();
                    ms.Close();

                    return ms.ToArray();
                }
            }
        }



        public static string DecryptToString(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        static byte[] DecryptoBytes(byte[] dest, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var ms = new MemoryStream(dest))
                using (var destMs = new MemoryStream())
                using (var cstream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] data = new byte[100];
                    int readLen;

                    while ((readLen = cstream.Read(data, 0, 100)) > 0)
                        destMs.Write(data, 0, readLen);

                    return destMs.ToArray();
                }
            }
        }





        public  byte[] EncryptFromString(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        public byte[] EncrypFromBytes(byte[] src)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (var ms = new MemoryStream())
                using (var cstream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cstream.Write(src, 0, src.Length);
                    cstream.FlushFinalBlock();
                    cstream.Close();
                    ms.Close();

                    return ms.ToArray();
                }
            }
        }



        public string DecryptToString(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public byte[] DecryptoBytes(byte[] dest)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (var ms = new MemoryStream(dest))
                using (var destMs = new MemoryStream())
                using (var cstream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] data = new byte[1024];
                    int readLen;
                    try
                    {
                        while ((readLen = cstream.Read(data, 0, data.Length)) > 0)
                            destMs.Write(data, 0, readLen);
                        destMs.Flush();
                    }
                    catch (System.Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                    ms.Flush();
                    destMs.Flush();
                    return destMs.ToArray();
                }
            }
        }



    }

}



