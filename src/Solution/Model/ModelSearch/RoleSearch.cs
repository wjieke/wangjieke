using Model.ModelBase;

namespace Model.ModelSearch
{
    public class RoleSearch : SearchBase
    {
        public int? ParentId { get; set; }
        public string RoleName { get; set; }
    }
}
