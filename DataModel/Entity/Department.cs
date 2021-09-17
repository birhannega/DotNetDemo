using DataModel.common;

namespace DataModel.Entity
{
    public class Department :BaseEntity
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
