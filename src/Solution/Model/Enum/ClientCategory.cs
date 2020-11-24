using System;
using System.ComponentModel;

namespace Model.Enum
{
    /// <summary>
    /// 客户端类别枚举
    /// </summary>
    [Flags]
    public enum ClientCategory
    {
        /// <summary>
        /// 后台管理端
        /// </summary>
        [Description("管理端")]
        Manage = 1,

        /// <summary>
        /// 移动手机端
        /// </summary>
        [Description("移动端")]
        Mobile = 2,

        /// <summary>
        /// 平板端
        /// </summary>
        [Description("平板端")]
        Pad = 4,

        /// <summary>
        /// 网页端
        /// </summary>
        [Description("网页端")]
        Web = 8,

        /// <summary>
        /// 用户界面端
        /// </summary>
        [Description("界面端")]
        UI = 16
    }
}