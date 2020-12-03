using Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table("Com_Department")]
    [Serializable]
    public class Department : TreeBase<Department>
    {
        #region 实体属性

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 主管id
        /// </summary>
        [Column("SupervisorID")]
        public int? SupervisorId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public int? CompanyId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 所属公司
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        /// <summary>
        /// 用户信息集合
        /// </summary>
        [JsonIgnore]
        public virtual List<User> Users { get; set; }

        #endregion

        #region 数据传输

        #endregion

    }
}
