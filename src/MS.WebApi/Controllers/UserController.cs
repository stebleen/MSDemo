using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Models.ViewModel;
using MS.Services;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
{
    [Route("/user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ExecuteResult<User>>> Login([FromBody] WeChatLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest("Invalid request: Code is required.");
            }

            
            var appId = "wx52b577deb9074e88";
            var secret = "c82bff42b3b720b4785f43d4df6d598e";

            var result = await _userService.HandleWeChatLoginAsync(appId, secret, request.Code);

            if (result.Code)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
