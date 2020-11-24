using Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    [Table("Sys_Permission")]
    public class Permission
    {
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
        public virtual Permission Parent { get; set; }

        /// <summary>
        /// 子对象集合
        /// </summary>
        public virtual List<Permission> Children { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public virtual int Degree { get; set; }

        /// <summary>
        /// 父对象id层级集合
        /// </summary>
        [NotMapped]//不映射字段
        public virtual List<string> Ids { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建者编号
        /// </summary>
        [Column("CreatorID")]
        public virtual int? CreatorId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 修改者编号
        /// </summary>
        [Column("ModifierID")]
        public virtual int? ModifierId { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public virtual DataState DataState { get; set; } = DataState.Enable;

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public virtual string Remark { get; set; } = null;

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientCategory ClientCategory { get; set; }

        //public virtual List<Role> Roles { get; set; } = new List<Role>();
        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}