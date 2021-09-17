using DataModel.common;
using DataModel.DTO;
using DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IAddress
    {
        public ResponseModel<Address> Create(CreateAddressDto addressDto);
        public ResponseModel<Address> Get(int  addressId);
        public ResponseModel<Address> Delete(int  addressId);
        public ResponseModel<Address> GetAll();
        public ResponseModel<Address> Update(int id,CreateAddressDto addressDto);
    }
}
