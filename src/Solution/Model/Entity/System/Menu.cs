using Model.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;
using static Model.Enum.SystemEnum;

namespace Model.Entity.System
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table("Sys_Menu")]
    public class Menu : TreeBase<Menu>
    {
        #region 实体属性

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }

        #endregion

        #region 导航属性

        #endregion

        #region 数据传输

        /// <summary>
        /// 系统终端类别
        /// </summary>
        [NotMapped]
        public ClientCategory ClientCategory { get; set; }

        #endregion
    }
}