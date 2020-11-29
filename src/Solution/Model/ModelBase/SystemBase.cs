using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Model.Enum.SystemEnum;

namespace Model.ModelBase
{
    [Serializable]
    public abstract class SystemBase : DataBase
    {
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
    }
}