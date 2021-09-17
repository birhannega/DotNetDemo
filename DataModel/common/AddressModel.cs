using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.common
{
    class AddressModel<T>
    {
        public List<T> Data { get; set; }
        public bool Success { get; set; }
        public ErrorModel Error { get; set; }
        public int TotalCount { get; set; }
    }
        
    }

