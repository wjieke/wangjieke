using Model.ModelBase;
using Model.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity.System
{
    [Table("Sys_Role")]
    public class Role : TreeBase<Role>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 系统终端类别
        /// </summary>
        [NotMapped]
        public ClientCategory? ClientCategory { get; set; }

        [NotMapped]
        public string[] PermissionIds { get; set; }

        //public virtual List<Permission> Permissions { get; set; } = new List<Permission>();
        //[JsonIgnore]


        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}