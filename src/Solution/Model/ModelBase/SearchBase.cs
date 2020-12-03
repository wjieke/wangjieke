namespace Model.ModelBase
{
    /// <summary>
    /// 搜索基类
    /// </summary>
    public abstract class SearchBase
    {
        /// <summary>
        /// 页面索引(当前第几页)
        /// </summary>
        public virtual int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页面大小(一页多少条)
        /// </summary>
        public virtual int PageSize { get; set; } = 10;

    }
}