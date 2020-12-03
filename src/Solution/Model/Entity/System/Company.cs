using Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    /// <summary>
    /// 公司
    /// </summary>
    [Table("Com_Company")]
    [Serializable]
    public class Company : SystemBase
    {
        #region 实体属性

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 部门信息集合
        /// </summary>
        [JsonIgnore]
        public virtual List<Department> Departments { get; set; }

        #endregion

        #region 数据传输

        #endregion
    }
}