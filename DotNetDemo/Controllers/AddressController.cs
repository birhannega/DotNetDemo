using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using DataModel.common;
using DataModel.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressService;
        public AddressController(IAddress addressService)
        {

            _addressService = addressService;
        }
        [HttpGet]
        public ResponseModel<Address> Get()
        {
            return _addressService.GetAll();
        }
        [HttpPost]
        public ResponseModel<Address> Create(Address address)
        {

            return _addressService.Create(address);
        }
        [HttpPut]
        public ResponseModel<Address> Update(int id, Address address)
        {
            return _addressService.Update(id, address);
        }
        [HttpDelete]
        public ResponseModel<Address> Delete(int id)
        {
            return _addressService.Delete(id);
        }
    }
}
