using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Library.Tool
{
    /// <summary>
    /// 加解密工具类
    /// </summary>
    public static class EncryptDecryptTool
    {
        #region 加解密

        #region DEC加解密

        private const string _PassWordKey = "__jetech"; //URL传输参数加密Key只能8位，所以这里只用8位。

        #region 公共方法

        /// <summary>
        ///     加密帐号口令
        /// </summary>
        /// <param name="wordString"></param>
        /// <returns></returns>
        public static string EncryptPassWord(string wordString)
        {
            return Encrypt(wordString, _PassWordKey);
        }

        /// <summary>
        ///     解密帐号口令
        /// </summary>
        /// <param name="EncryptWordString"></param>
        /// <returns></returns>
        public static string DecryptPassWord(string EncryptWordString)
        {
            return Decrypt(EncryptWordString, _PassWordKey);
        }

        #endregion

        #region 加密过程

        /// <summary>
        ///     DEC 加密过程
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <param name="encKey"></param>
        /// <returns></returns>
        private static string Encrypt(string strToEncrypt, string encKey)
        {
            var des = new DESCryptoServiceProvider(); //把字符串放到byte数组中

            var inputByteArray = Encoding.Default.GetBytes(strToEncrypt);

            des.Key = Encoding.ASCII.GetBytes(encKey); //建立加密对象的密钥和偏移量
            des.IV = Encoding.ASCII.GetBytes(encKey); //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            var ms = new MemoryStream(); //使得输入密码必须输入英文文本
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            var ret = new StringBuilder();
            foreach (var b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region 解密过程

        /// <summary>
        ///     DEC 解密过程
        /// </summary>
        /// <param name="strToDecrypt"></param>
        /// <param name="encKey"></param>
        /// <returns></returns>
        private static string Decrypt(string strToDecrypt, string encKey)
        {
            var ms = new MemoryStream();
            try
            {
                var des = new DESCryptoServiceProvider();

                var inputByteArray = new byte[strToDecrypt.Length / 2];
                for (var x = 0; x < strToDecrypt.Length / 2; x++)
                {
                    var i = Convert.ToInt32(strToDecrypt.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }

                des.Key = Encoding.ASCII.GetBytes(encKey); //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.IV = Encoding.ASCII.GetBytes(encKey);

                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
            }
            catch
            {
            }
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #endregion DEC加解密

        #region 大写32位MD5加密

        public static string Md5(string tblName, string condition)
        {
            return Md5(tblName + condition);
        }

        public static string Md5(string word)
        {
            var md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(word))).Replace("-", null);
        }

        #endregion

        #region 小写16位MD5加密

        public static string md5(string word)
        {
            var md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(word))).Replace("-", null).ToLower().Substring(8, 16);
        }

        #endregion

        #region AES加解密

        const string AES_IV = "1234567890000000";//16位  

        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        ///<param name="input">明文字符串  
        ///<param name="key">密钥（32位）  
        /// <returns>字符串</returns>  
        public static string EncryptByAES(string input, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV.Substring(0, 16));

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        return ByteArrayToHexString(bytes);
                    }
                }
            }
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        ///<param name="input">密文字节数组  
        ///<param name="key">密钥（32位）  
        /// <returns>返回解密后的字符串</returns>  
        public static string DecryptByAES(string input, string key)
        {
            byte[] inputBytes = HexStringToByteArray(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV.Substring(0, 16));

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将指定的16进制字符串转换为byte数组
        /// </summary>
        ///<param name="s">16进制字符串(如：“7F 2C 4A”或“7F2C4A”都可以)
        /// <returns>16进制字符串对应的byte数组</returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 将一个byte数组转换成一个格式化的16进制字符串
        /// </summary>
        ///<param name="data">byte数组
        /// <returns>格式化的16进制字符串</returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                //16进制数字
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
                //16进制数字之间以空格隔开
                //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return sb.ToString().ToUpper();
        }

        #endregion

        #endregion
    }
}