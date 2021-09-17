using Contracts;
using DataModel.common;
using DataModel.DTO;
using DataModel.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressService;
        public AddressController(IAddress addressService)
        {
            _addressService = addressService;
        }
        [HttpPost]
       public ResponseModel<Address> Create(CreateAddressDto addressDto)
        {
            return _addressService.Create(addressDto);
        }
        [HttpPut]
        public ResponseModel<Address> Update(int id,CreateAddressDto addressDto)
        {
            return _addressService.Update(id,addressDto);
        }
        [HttpGet("{id}")]
        public ResponseModel<Address> Get(int id)
        {
            return _addressService.Get(id);
        }
        [HttpGet]
        public ResponseModel<Address> GetAll()
        {
            return _addressService.GetAll();
        }
        [HttpDelete]
        public ResponseModel<Address> Delete(int id)
        {
            return _addressService.Delete(id);
        }
    }
}
