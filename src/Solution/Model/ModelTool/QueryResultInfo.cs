using System.Collections.Generic;

namespace Model.ModelTool
{
    /// <summary>
    /// 查询结果返回信息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class QueryResultInfo<TModel> : ActionResultInfo<TModel>
    {
        /// <summary>
        /// 数据集
        /// </summary>
        public IList<TModel> Datas { get; set; }

        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
    }
}
