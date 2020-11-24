using Model.ModelBase;

namespace Model.ModelSearch
{
    public class MenuSearch: SearchBase
    {
        public string MenuName { get; set; }
        public int? ParentId { get; set; }
    }
}
