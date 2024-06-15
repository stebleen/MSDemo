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
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: /user/shoppingCart/list
        [HttpGet("list")]
        public async Task<IActionResult> List(long userId) // 从某处获得了userId，可能是JWT token中
        {
            userId = 7;
            var items = await _shoppingCartService.GetShoppingCart(userId);
            return Ok(new { code=true, data = items });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            long userId = 7; 

            var result = await _shoppingCartService.AddToCartAsync(addToCartDto, userId);

            if (result.Code)
            {
                return Ok(new { code = true, data = result.Data, msg = result.Message });
            }
            else
            {
                return Ok(new { code = false, data = "Error", msg = result.Message });
            }
        }
        [HttpPost("sub")]
        public async Task<IActionResult> SubToCart([FromBody] AddToCartDto addToCartDto)
        {
            long userId = 7;

            var result = await _shoppingCartService.SubFromCartAsync(addToCartDto, userId);

            if (result.Code)
            {
                return Ok(new { code = true, data = result.Data, msg = result.Message });
            }
            else
            {
                return Ok(new { code = false, data = "Error", msg = result.Message });
            }
        }
    }
}
