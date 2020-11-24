using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Library.Tool
{
    /// <summary>
    /// Json序列化工具类
    /// </summary>
    public static class JsonSerializerTool
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

        /// <summary>
        /// 把指定的对象序列化成Json字符串
        /// </summary>
        /// <typeparam name="TValue">T类型</typeparam>
        /// <param name="value">T类型值</param>
        /// <returns></returns>
        public static string ObjectToJson<TValue>(TValue value)
        {
            return JsonSerializer.Serialize(value, options);
        }

        /// <summary>
        /// 把Json字符串序列化成指定对象
        /// </summary>
        /// <typeparam name="TValue">T类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static TValue JsonToObject<TValue>(string json)
        {
            return JsonSerializer.Deserialize<TValue>(json, options);
        }
    }
}
