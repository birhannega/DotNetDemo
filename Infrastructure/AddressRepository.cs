using Contracts;
using DataModel;
using DataModel.common;
using DataModel.DTO;
using DataModel.Entity;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AddressRepository : IAddress
    {
        private readonly EmployeeDbContext _dbcontext;
        private readonly AddressValidator _validator;
        public AddressRepository(EmployeeDbContext dbContext)
        {
            _dbcontext = dbContext;
            _validator = new AddressValidator();
        }


        public ResponseModel<Address> Create(CreateAddressDto addressDto)
        {
            var result = _validator.Validate(addressDto);
            if (!result.IsValid)
            {
                return new ResponseModel<Address>()
                {
                    Error= new ErrorModel()
                    {
                        ErrorCode= StatusCodes.Status400BadRequest,
                        ErrorDescription= "Validation errors found", 
                        ErrorMessage= result.Errors[0].ErrorMessage
                    }, 
                    Success= false
                };
            }
            var address = new Address()
            {
                Region= addressDto.Region, 
                Zone=  addressDto.Zone,
                Woreda= addressDto.Woreda, 
            };
            _dbcontext.Addresses.Add(address);
            _dbcontext.SaveChanges();
            return new ResponseModel<Address>
            {
                Success = true,
                Data = new List<Address>
                {
                    address
                }
            };

        }

        public ResponseModel<Address> Delete(int addressId)
        {
            var target = _dbcontext.Addresses.Find(addressId);
            if(target!=null)
            {
                return new ResponseModel<Address>()
                {
                    Success= false,
                    Error = new ErrorModel()
                    {
                        ErrorCode= StatusCodes.Status404NotFound, 
                        ErrorMessage= "Address not found"
                    }
                };
            }
            _dbcontext.Remove(target);
            _dbcontext.SaveChanges();
            return new ResponseModel<Address>()
            {
                    Success= true,
                    Data= new List<Address>()
                    {
                       target 
                    }
            };
        }

        public ResponseModel<Address> Get(int addressId)
        {
            return new ResponseModel<Address>()
            {
                Success=true, 
                Error=null,
                Data= new List<Address>()
                {

                    _dbcontext.Addresses.Find(addressId)
                }

            };
        }

        public ResponseModel<Address> GetAll()
        {
            return new ResponseModel<Address>()
            {
                Success = true,
                Data = _dbcontext.Addresses.ToList()
            };
        }

        public ResponseModel<Address> Update(int id, CreateAddressDto addressDto)
        {
            var target = _dbcontext.Addresses.Find(id);
            if (target != null)
            {
                return new ResponseModel<Address>()
                {
                    Success = false,
                    Error = new ErrorModel()
                    {
                        ErrorCode = StatusCodes.Status404NotFound,
                        ErrorMessage = "Address not found"
                    }
                };
            }
            target.Region = addressDto.Region;
            target.Zone = addressDto.Zone;
            target.Woreda = addressDto.Woreda;
            _dbcontext.Addresses.Update(target);
            _dbcontext.SaveChanges();

            return new ResponseModel<Address>()
            {
                Success= true, Data= new List<Address>()
                {
                    target
                }
            };
           
        }
    }
}
