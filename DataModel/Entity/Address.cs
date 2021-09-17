using DataModel.common;

namespace DataModel.Entity
{
    public class Address: BaseEntity
    {
        public int AddressId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string Telephone { get; set; }
        public int Mobile { get; set; }
    }
}
