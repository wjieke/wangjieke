using Model.ModelBase;
using System.Collections.Generic;

namespace Models.ModelDto
{
    public class NodeModelDto<TTreeModel> : TreeBase<TTreeModel>
    {
        public override int Id { get; set; }

        public string NodeName { get; set; }

        public new IList<TTreeModel> Children { get; set; }
    }
}
