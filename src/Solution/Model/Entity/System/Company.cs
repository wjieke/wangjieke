using Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entity.System
{
    [Table("Com_Company")]
    [Serializable]
    public class Company : SystemBase
    {
        public string CompanyName { get; set; }

        [JsonIgnore]
        public virtual List<Department> Departments { get; set; }
    }
}