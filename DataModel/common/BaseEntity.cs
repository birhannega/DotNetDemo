using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
