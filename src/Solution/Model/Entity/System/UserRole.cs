using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    [Table("Sys_UserRole")]
    [Serializable]
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; } = new User();
        [JsonIgnore]
        public virtual Role Role { get; set; } = new Role();
    }
}
