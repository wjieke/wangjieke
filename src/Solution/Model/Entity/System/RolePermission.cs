using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [Table("Sys_RolePermissions")]
    public class RolePermission
    {
        #region 实体属性

        /// <summary>
        /// 编号ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string PermissionId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 角色信息
        /// </summary>
        [JsonIgnore]
        public virtual Role Role { get; set; } = new Role();

        /// <summary>
        /// 权限信息
        /// </summary>
        [JsonIgnore]
        public virtual Permission Permission { get; set; } = new Permission();

        #endregion

        #region 数据传输

        #endregion
    }
}
