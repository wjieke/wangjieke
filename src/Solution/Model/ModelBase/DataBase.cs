using System;
using System.ComponentModel.DataAnnotations;        //关系型数据库命名空间
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.ModelBase
{
    [Serializable]
    public abstract class DataBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]                           //主键
        [Column("ID")]                  //Column列映射，对应数据库表的列名
        public virtual int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        //public virtual string Name { get; set; }
    }
}