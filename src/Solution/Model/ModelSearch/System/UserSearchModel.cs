using Model.ModelBase;
using System;

namespace Model.ModelSearch.System
{
    /// <summary>
    /// 用户搜索过滤类
    /// </summary>
    public class UserSearchModel : SearchBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime? Birthday { get; set; }
    }
}
