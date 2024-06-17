using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
using System;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace MS.WebApi.Controllers.admin
{
    [Route("admin/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAddresses([FromQuery] string campusName)
        {
            var addresses = await _addressService.GetAddressesByCampusNameAsync(campusName);
            return Ok(new { code = true, data = addresses, msg = "" });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] long id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address != null)
            {
                return Ok(new { code = true, data = address, msg = "" });
            }
            else
            {
                return NotFound(new { code = false, msg = "Address not found" });
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] Address address)
        {
            var success = await _addressService.UpdateAddressAsync(address);
            if (success)
            {
                return Ok(new { Code = true, Msg = "Address updated successfully." });
            }
            else
            {
                return NotFound(new { Code = false, Msg = "Address not found." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            try
            {
                var createdAddress = await _addressService.CreateAddressAsync(address);
                return Ok(new
                {
                    code = true,
                    data = new { id = createdAddress.Id },
                    msg = "Address created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = false,
                    msg = ex.Message
                });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAddress([FromQuery] long id)
        {
            bool success = await _addressService.DeleteAddressByIdAsync(id);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Address deleted successfully." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Address not found." });
            }
        }


    }
}
