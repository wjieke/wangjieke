using Model.ModelBase;

namespace Model.ModelSearch
{
    /// <summary>
    /// 公司搜索模型类
    /// </summary>
    public class CompanySearchModel : SearchBase
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
