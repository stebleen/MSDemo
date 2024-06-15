using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
using MS.Entities;

namespace MS.WebApi.Controllers
{
    [ApiController]
    [Route("/user/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: /user/address/list
        [HttpGet("list")]
        public async Task<IActionResult> List() // 从某处获得了userId，可能是JWT token中
        {
           
            var items = await _addressService.GetAllAddressesAsync();
            return Ok(new { code = true, data = items });
        }
    }
}
