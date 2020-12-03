using Model.ModelBase;
using System.Collections.Generic;

namespace Models.ModelDto
{
    /// <summary>
    /// 节点模型Dto
    /// </summary>
    /// <typeparam name="TTreeModel"></typeparam>
    public class NodeModelDto<TTreeModel> : TreeBase<TTreeModel>
    {
        /// <summary>
        /// ID
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public new IList<TTreeModel> Children { get; set; }
    }
}
