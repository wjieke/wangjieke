using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [Table("Sys_RolePermissions")]
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string PermissionId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; } = new Role();
        [JsonIgnore]
        public virtual Permission Permission { get; set; } = new Permission();
    }
}
