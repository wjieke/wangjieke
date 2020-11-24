using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.ModelBase
{
    [Serializable]
    public abstract class TreeBase<TTreeModel> : SystemBase
    {
        /// <summary>
        /// 父编号
        /// </summary>
        [ForeignKey("Parent")]//外键
        [Column("ParentID")]
        public virtual int? ParentId { get; set; }

        /// <summary>
        /// 父对象
        /// </summary>
        //[ForeignKey("ParentId")]  //外键
        [JsonIgnore]                //忽略Json序列化自我引用循环（特性修饰其中一个导航属性，该特性指示Json在序列化时不遍历该导航属性。）
        public virtual TTreeModel Parent { get; set; }

        /// <summary>
        /// 子对象集合
        /// </summary>
        public virtual List<TTreeModel> Children { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public virtual int Degree { get; set; }

        /// <summary>
        /// 父对象id层级集合
        /// </summary>
        [NotMapped]//不映射字段
        public virtual List<int> Ids { get; set; }
    }
}