using Model.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity.System
{
    /// <summary>
    /// 地区
    /// </summary>
    [Table("Sys_Area")]
    public class Area : TreeBase<Area>
    {
        #region 实体属性

        /// <summary>
        /// 地区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 地区简称
        /// </summary>
        public string Abbreviation { get; set; } = null;

        #endregion

        #region 导航属性

        #endregion

        #region 数据传输

        #endregion

    }
}