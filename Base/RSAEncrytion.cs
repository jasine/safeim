using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

namespace Base
{
    public class RSAEncrytion
    {
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, String RSAKeyInfo)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.FromXmlString(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later. 
                    int offset=0;
                    if (DataToEncrypt.Length > 64)//blockSize=(KeySize/8)-11
                    {
                        double count=Math.Ceiling((double)DataToEncrypt.Length/(double)64);
                        byte[] temp =new byte [DataToEncrypt.Length * 5];
                        for(int i=0;i<(int)count;i++)
                        {
                            byte[] src=new byte[64];
                            if((i+1)*64<=DataToEncrypt.Length)
                                Buffer.BlockCopy(DataToEncrypt,i*64,src,0,64);
                            else
                                Buffer.BlockCopy(DataToEncrypt, i * 64, src, 0, DataToEncrypt.Length-i*64);
                            byte[] temp2 = RSA.Encrypt(src,true);
                            Buffer.BlockCopy(temp2, 0, temp, offset,temp2.Length);
                            offset = offset + temp2.Length;
                        }
                        encryptedData = new byte[offset];
                        Buffer.BlockCopy(temp, 0, encryptedData, 0, offset);
                    }
                    else
                    {
                        encryptedData = RSA.Encrypt(DataToEncrypt, true);
                    }
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, string RSAKeyInfo)
        {
            byte[] back;
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.FromXmlString(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    int offset = 0;
                    if (DataToDecrypt.Length > 128)//blockSize=(KeySize/8)-11
                    {
                        double count = Math.Ceiling((double)DataToDecrypt.Length / (double)128);
                        byte[] temp = new byte[DataToDecrypt.Length];
                        for (int i = 0; i < (int)count; i++)
                        {
                            byte[] src = new byte[128];
                            if ((i + 1) * 128 <= DataToDecrypt.Length)
                                Buffer.BlockCopy(DataToDecrypt, i * 128, src, 0, 128);
                            else
                                Buffer.BlockCopy(DataToDecrypt, i * 128, src, 0, DataToDecrypt.Length - i * 128);
                            byte[] temp2 = RSA.Decrypt(src, true);
                            Buffer.BlockCopy(temp2, 0, temp, offset, temp2.Length);
                            offset = offset + temp2.Length;
                        }
                        decryptedData = new byte[offset];
                        Buffer.BlockCopy(temp, 0, decryptedData, 0, offset);
                    }
                    else
                    {
                        decryptedData = RSA.Decrypt(DataToDecrypt, true);
                    }

                    int k = decryptedData.Length - 1;
                    for (; k >= 0 && decryptedData[k] == 0; k--) ;
                    back = new byte[k + 1];
                    Buffer.BlockCopy(decryptedData, 0, back, 0, back.Length);
                }
                return back;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="str_source">原字符串</param>
        /// <returns>加密后的字符串</returns>
        public static byte[] getHash(string str_source)
        {
            HashAlgorithm ha = HashAlgorithm.Create("MD5");
            byte[] bytes = Encoding.GetEncoding(0).GetBytes(str_source);
            byte[] str_hash = ha.ComputeHash(bytes);
            return str_hash;
        }

        /// <summary>
        /// 对SHA1加密后的字符串进行RSA签名
        /// </summary>
        /// <param name="privatekey">私有密钥</param>
        /// <param name="str_HashbyteSingture">SHA1加密后的字符串</param>
        /// <returns>签名后的数据</returns>
        public static byte[] SignatureFormatter(string privatekey, byte[] rgbHash)
        {
            //byte[] rgbHash = Convert.FromBase64String(str_HashbyteSingture);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(privatekey);
            RSAPKCS1SignatureFormatter rsa_formatter = new RSAPKCS1SignatureFormatter(key);
            rsa_formatter.SetHashAlgorithm("MD5");
            byte[] bytes = rsa_formatter.CreateSignature(rgbHash);
            return bytes;
        }

        /// <summary>
        /// 对RSA签名进行验证
        /// </summary>
        /// <param name="publickey">公有密钥</param>
        /// <param name="strHashbyteDeformatter">SHA1加密后的字符串</param>
        /// <param name="strDeformatterData">RSA签名后的字符串</param>
        /// <returns></returns>
        public static bool SignatureDeformatter(string publickey, byte[] rgbHash, byte[] rgbSignature)
        {
            //byte[] rgbHash = Convert.FromBase64String(strHashbyteDeformatter);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(publickey);
            RSAPKCS1SignatureDeformatter rsa_Deformatter = new RSAPKCS1SignatureDeformatter(key);
            rsa_Deformatter.SetHashAlgorithm("MD5");
            //byte[] rgbSignature = Convert.FromBase64String(strDeformatterData);
            if (rsa_Deformatter.VerifySignature(rgbHash, rgbSignature))
            {
                return true;
            }
            return false;
        }


    }

}
