using Model.ModelBase;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity.System
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    [Table("Sys_User")] //表映射，对应数据库指定的表名
    [Serializable] //可序列化类
    public class User: SystemBase
    {
        #region 实体属性
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIpAddress { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 登录地区
        /// </summary>
        public string LoginArea { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public SexType? Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int? DepartmentId { get; set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 所属部门
        /// </summary>
        //[JsonIgnore]
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        #endregion

        #region 数据传输
        [NotMapped]
        public int[] RoleIds { get; set; }
        #endregion
    }
}