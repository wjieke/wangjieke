using Model.ModelBase;

namespace Model.ModelSearch
{
    /// <summary>
    /// 菜单搜索模型类
    /// </summary>
    public class MenuSearchModel: SearchBase
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int? ParentId { get; set; }
    }
}
