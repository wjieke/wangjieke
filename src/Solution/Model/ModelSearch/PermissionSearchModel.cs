using Model.ModelBase;

namespace Model.ModelSearch
{
    /// <summary>
    /// 权限搜索模型
    /// </summary>
    public class PermissionSearchModel: SearchBase
    {

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentId { get; set; }
    }
}
