using Model.ModelBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Model.Enum.SystemEnum;

namespace Model.Entity.System
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("Sys_Role")]
    public class Role : TreeBase<Role>
    {
        #region 实体属性

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 用户角色集合
        /// </summary>
        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 角色权限集合
        /// </summary>
        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        #endregion

        #region 数据传输

        /// <summary>
        /// 系统终端类别
        /// </summary>
        [NotMapped]
        public ClientCategory? ClientCategory { get; set; }

        /// <summary>
        /// 权限ID数组
        /// </summary>
        [NotMapped]
        public string[] PermissionIds { get; set; }

        #endregion
    }
}