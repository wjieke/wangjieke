using Model.ModelBase;

namespace Model.ModelSearch
{
    /// <summary>
    /// 部门搜索模型类
    /// </summary>
    public class DepartmentSearchModel: SearchBase
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int? ParentId { get; set; }
    }
}
