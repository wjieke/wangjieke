using System;
using System.ComponentModel;

namespace Model.Enum
{
    /// <summary>
    /// 数据库操作类型
    /// </summary>
    [Flags]
    public enum DbActionType
    {
        /// <summary>
        /// 增加
        /// </summary>
        [Description("增加")]
        Add = 1,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del = 2,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Mod = 3
    }
}
