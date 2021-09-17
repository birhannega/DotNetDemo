using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.common;
using DataModel.Entity;

namespace Contracts
{
    public interface IAddress
    {
        public ResponseModel<Address> Create(Address address);
        public ResponseModel<Address> Update(int id, Address address);
        public ResponseModel<Address> GetAll();
        public ResponseModel<Address> Get(int id);
        public ResponseModel<Address> Delete(int id);

    }

}

        