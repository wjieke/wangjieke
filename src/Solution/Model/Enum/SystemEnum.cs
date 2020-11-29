using System;
using System.ComponentModel;

namespace Model.Enum
{
    public class SystemEnum
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
}
