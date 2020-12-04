using Model.Enum;
using Model.ModelBase;

namespace Model.Entity.System
{
    /// <summary>
    /// 数据字典实体类
    /// </summary>
    public class DataDictionary : SystemBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; set; }
    }
}
