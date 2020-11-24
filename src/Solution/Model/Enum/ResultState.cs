using System;
using System.ComponentModel;

namespace Model.Enum
{
    /// <summary>
    /// 返回状态枚举
    /// </summary>
    [Flags]
    public enum ResultState
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failure = 2,

        /// <summary>
        /// 出错
        /// </summary>
        [Description("出错")]
        Error = 4
    }
}