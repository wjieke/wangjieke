using Model.ModelBase;

namespace Model.ModelSearch
{
    public class AreaSearch: SearchBase
    {
        public string AreaName { get; set; }
        public string Abbreviation { get; set; }
        public int? ParentId { get; set; }
    }
}
