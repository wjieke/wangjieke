using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Library.Tool
{
    /// <summary>
    /// 扩展工具类
    /// </summary>
    public static class ExtensionTool
    {
        #region byte[] class 互转

        /// <summary>
        /// 对象类转byte[] 【且对象为类类型，对象类必须为可序列化的(Serializable)】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>byte[]</returns>
        public static byte[] ToBytes<T>(this T t) where T : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, t);
                return ms.GetBuffer();
            }
        }

        /// <summary>
        /// byte[] 转类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Bytes"></param>
        /// <returns>该类</returns>
        public static T ToClass<T>(this byte[] Bytes) where T : class
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms) as T;
            }
        }
        #endregion
    }
}
