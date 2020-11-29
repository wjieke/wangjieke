using Model.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;
using static Model.Enum.SystemEnum;

namespace Model.Entity.System
{
    [Table("Sys_Menu")]
    public class Menu : TreeBase<Menu>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }

        /// <summary>
        /// 系统终端类别
        /// </summary>
        [NotMapped]
        public ClientCategory ClientCategory { get; set; }
    }
}