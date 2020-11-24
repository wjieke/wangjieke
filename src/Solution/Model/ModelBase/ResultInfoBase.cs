using Model.Enum;

namespace Model.ModelBase
{
    /// <summary>
    /// 返回信息统一格式(基类)
    /// </summary>
    public abstract class ResultInfoBase
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public ResultState ResultState { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
    }
}
