using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Model.Enum.SystemEnum;

namespace Model.Entity.System
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table("Sys_Permission")]
    public class Permission
    {
        #region 实体属性

        /// <summary>
        /// 父编号
        /// </summary>
        [ForeignKey("Parent")]//外键
        [Column("ParentID")]
        public string ParentId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [Key]
        [Column("PermissionID")]
        public string PermissionId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 父对象
        /// </summary>
        //[ForeignKey("ParentId")]  //外键
        [JsonIgnore]                //忽略Json序列化自我引用循环（特性修饰其中一个导航属性，该特性指示Json在序列化时不遍历该导航属性。）
        public Permission Parent { get; set; }

        /// <summary>
        /// 子对象集合
        /// </summary>
        public List<Permission> Children { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建者编号
        /// </summary>
        [Column("CreatorID")]
        public int? CreatorId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 修改者编号
        /// </summary>
        [Column("ModifierID")]
        public int? ModifierId { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public DataState DataState { get; set; } = DataState.Enable;

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; } = null;

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientCategory ClientCategory { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 角色权限集合
        /// </summary>
        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        #endregion

        #region 数据传输

        /// <summary>
        /// 父对象id层级集合
        /// </summary>
        [NotMapped]//不映射字段
        public virtual List<string> Ids { get; set; }

        #endregion

    }
}