using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Tool
{
    /// <summary>
    /// 公共方法工具类
    /// </summary>
    public static class CommonTool
    {
        #region 枚举操作

        /// <summary>
        /// 根据枚举值获取描述属性
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
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
        public static List<EnumInfo> GetEnumList<TEnum>() where TEnum : Enum
        {
            List<EnumInfo> list = new List<EnumInfo>();
            foreach (Enum item in Enum.GetValues(typeof(TEnum)))
            {
                list.Add(new EnumInfo
                {
                    Value = Convert.ToInt32(item),
                    Label = GetEnumDescription(item)
                });
            }
            return list;
        }

        #endregion
    }
}