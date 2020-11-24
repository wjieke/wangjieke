using System;
using System.ComponentModel;

namespace Model.Enum
{
    /// <summary>
    /// 性别类型枚举
    /// </summary>
    [Flags]
    public enum SexType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 0,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 1,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 2,
    }
}