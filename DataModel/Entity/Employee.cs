using System;

namespace DataModel.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public int DepartmentId { get; set; } 
        public int AddressId { get; set; }

        public virtual Department VDepartment { get; set; }
        public virtual Address VAddress { get; set; }
    }
}
