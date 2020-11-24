using Model.ModelBase;

namespace Model.ModelSearch
{
    public class PermissionSearch: SearchBase
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}
