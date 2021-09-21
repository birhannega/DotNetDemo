using System;

namespace DataModel.common
{
    public class BaseEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public bool IsDeleted { get; set; }
        public int CurrentStatus { get; set; }
    }
}
