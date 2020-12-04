using System;
using System.ComponentModel;

namespace Model.Enum
{
    /// <summary>
    /// 数据字典->数据类型
    /// </summary>
    [Flags]
    public enum DataType
    {
        /// <summary>
        /// 数据1
        /// </summary>
        [Description("数据1")]
        Data1 = 1,

        /// <summary>
        /// 数据2
        /// </summary>
        [Description("数据2")]
        Data2 = 2,
    }
}
