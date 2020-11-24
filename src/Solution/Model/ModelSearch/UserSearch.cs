using Model.ModelBase;
using System;

namespace Model.ModelSearch
{
    /// <summary>
    /// 用户搜索过滤类
    /// </summary>
    public class UserSearch : SearchBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
    }
}
