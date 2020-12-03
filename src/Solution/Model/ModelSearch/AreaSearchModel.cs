using Model.ModelBase;

namespace Model.ModelSearch
{
    /// <summary>
    /// 地区搜索模型
    /// </summary>
    public class AreaSearchModel: SearchBase
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int? ParentId { get; set; }
    }
}
