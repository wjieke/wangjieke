using System;
using System.ComponentModel;

namespace Model.Enum
{
    /// <summary>
    /// 数据状态枚举
    /// </summary>
    [Flags]
    public enum DataState
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,

        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disable = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Deleted = 4
    }
}