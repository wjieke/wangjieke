using Model.ModelBase;

namespace Model.ModelTool
{
    /// <summary>
    /// 返回信息统一格式
    /// </summary>
    /// <typeparam name="TModel">返回数据的该类对象</typeparam>
    public class ActionResultInfo<TModel> : ResultInfoBase
    {
        /// <summary>
        /// 数据
        /// </summary>
        public TModel Data { get; set; }
    }
}