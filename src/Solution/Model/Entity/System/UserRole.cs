using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Table("Sys_UserRole")]
    [Serializable]
    public class UserRole
    {
        #region 实体属性

        /// <summary>
        /// 编号ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 用户信息
        /// </summary>
        [JsonIgnore]
        public virtual User User { get; set; } = new User();

        /// <summary>
        /// 角色信息
        /// </summary>
        [JsonIgnore]
        public virtual Role Role { get; set; } = new Role();

        #endregion

        #region 数据传输

        #endregion
    }
}
