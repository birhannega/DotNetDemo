using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataModel;
using DataModel.common;
using DataModel.Entity;
using Infrastructure.Validators;

namespace Infrastructure
{
    public class AddressRepository : IAddress
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly AddressValidator _addressValidator;

        public AddressRepository(EmployeeDbContext dbContext)
        {

        }
        public ResponseModel<Address> Create(Address address)
        {
            var response = new ResponseModel<Address>();
            var result = _addressValidator.Validate(address);
            if (!result.IsValid)

            {
                response.TotalCount = 0;
                response.Success = false;
                response.Error = new ErrorModel()
                {
                    ErrorCode = 0,
                    ErrorDescription = "Please fix validation errors",
                    ErrorMessage = result.Errors[0].ErrorMessage
                };
                return response;
            }
            //var newAddre = new Address()
            //{
            //    AddressId = address.AddressId,
            //    Zone = address.Zone,
            //    Woreda = address.Woreda,
            //    Region = address.Region,
            //                };

            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();

            response.Success = true;
            response.Error = null;
            response.TotalCount = 1;
            response.Data = new List<Address>()
            {
            _dbContext.Addresses.Find(address.AddressId)
            };
            return response;
        }

        public ResponseModel<Address> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<Address> Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<Address> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseModel<Address> Update(int id, Address address)
        {
            throw new NotImplementedException();
        }
    }
}
