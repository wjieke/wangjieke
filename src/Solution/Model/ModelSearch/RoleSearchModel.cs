using Model.ModelBase;

namespace Model.ModelSearch
{
    /// <summary>
    /// 角色搜索模型类
    /// </summary>
    public class RoleSearchModel : SearchBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int? ParentId { get; set; }
    }
}
