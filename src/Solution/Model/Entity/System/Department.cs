using Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    [Table("Com_Department")]
    [Serializable]
    public class Department : TreeBase<Department>
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 主管id
        /// </summary>
        [Column("SupervisorID")]
        public int? SupervisorId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public virtual int? CompanyId { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [JsonIgnore]
        public virtual List<User> Users { get; set; }
    }
}
