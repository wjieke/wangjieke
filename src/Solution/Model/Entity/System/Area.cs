using Model.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity.System
{
    [Table("Sys_Area")]
    public class Area : TreeBase<Area>
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 地区简称
        /// </summary>
        public string Abbreviation { get; set; } = null;
    }
}