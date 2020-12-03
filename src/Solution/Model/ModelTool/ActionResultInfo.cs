using Model.ModelBase;

namespace Model.ModelTool
{
    /// <summary>
    /// 动作结果返回信息
    /// </summary>
    /// <typeparam name="TModel">返回数据的该类对象</typeparam>
    public class ActionResultInfo<TModel> : ResultInfoBase
    {
        /// <summary>
        /// 对象数据
        /// </summary>
        public TModel Data { get; set; }
    }
}