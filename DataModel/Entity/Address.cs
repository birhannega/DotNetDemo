using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entity
{
    public class Address
    {
        public int AddressId { get; set; }
            public string Zone { get; set; }
            public string Woreda { get; set; }
            public string Region { get; set; }
    }
}
