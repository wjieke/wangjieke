using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Library.Tool
{
    /// <summary>
    /// 扩展工具类
    /// </summary>
    public static class ExtensionTool
    {
        /// <summary>
        /// Json配置项
        /// </summary>
        private readonly static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),//跳过自我循环引用
            ReadCommentHandling = JsonCommentHandling.Skip,//默认大写，配置驼峰命名
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        #region byte[] class 互转 （MemoryStream 内存流方式；.net5.0 已移除BinaryFormatter使用，已废弃使用）

        /// <summary>
        /// 对象类转byte[] 【且对象为类类型，对象类必须为可序列化的(Serializable)】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>byte[]</returns>
        [Obsolete(".net 5.0 已移除BinaryFormatter使用，此方法不再使用")]
        public static byte[] ToBytesMemoryStream<T>(this T t) where T : class
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
        [Obsolete(".net 5.0 已移除BinaryFormatter使用，此方法不再使用")]
        public static T ToClassMemoryStream<T>(this byte[] Bytes) where T : class
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms) as T;
            }
        }

        #endregion

        #region System.Text.Json序列化

        /// <summary>
        /// 对象转Json字符串
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJsonString<TValue>(this TValue value)
        {
            return JsonSerializer.Serialize(value, options);
        }

        /// <summary>
        /// Json字符串转指定对象
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static TValue ToObject<TValue>(this string jsonString)
        {
            return JsonSerializer.Deserialize<TValue>(jsonString, options);
        }

        #endregion

        #region byte[] object 互转 (Json方式)

        /// <summary>
        /// 对象转byte[]
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        public static byte[] ToBytes<TObject>(this TObject @object)
        {
            return Encoding.Default.GetBytes(@object.ToJsonString());
        }

        /// <summary>
        /// byte[]转指定对象
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static TObject ToObject<TObject>(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes).ToObject<TObject>();
        }

        #endregion

        #region 枚举操作

        /// <summary>
        /// 根据枚举值获取描述属性
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            //获取描述属性
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            //当描述属性没有时，直接返回名称
            if (objs == null || objs.Length == 0)
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns></returns>
        public static List<EnumInfo> GetEnumList<TEnum>(this TEnum @enum) where TEnum : Enum
        {
            List<EnumInfo> list = new List<EnumInfo>();
            foreach (Enum item in Enum.GetValues(@enum.GetType()))
            {
                list.Add(new EnumInfo
                {
                    Value = Convert.ToInt32(item),
                    Label = item.GetEnumDescription()
                });
            }
            return list;
        }

        #endregion

    }
}
