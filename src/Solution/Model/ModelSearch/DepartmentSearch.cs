using Model.ModelBase;

namespace Model.ModelSearch
{
    public class DepartmentSearch: SearchBase
    {
        public string DepartmentName { get; set; }
        public int? ParentId { get; set; }
    }
}
