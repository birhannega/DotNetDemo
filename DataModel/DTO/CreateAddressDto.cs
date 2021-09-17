using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DTO
{
    public class CreateAddressDto
    {
        public int AddressId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woredda { get; set; }
    }
}
