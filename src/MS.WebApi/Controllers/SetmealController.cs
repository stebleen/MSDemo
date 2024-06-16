using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
namespace MS.WebApi.Controllers
{
    [ApiController]
    [Route("/user/[controller]")]
    public class SetmealController : ControllerBase
    {
        private readonly ISetmealService _setmealService;

        // 通过构造函数注入服务
        public SetmealController(ISetmealService setmealService)
        {
            _setmealService = setmealService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetSetmealByCategory([FromQuery] long? categoryId)
        {
            if (!categoryId.HasValue)
            {
                // 如果没有提供categoryId，返回错误信息
                return BadRequest("CategoryId is required");
            }

            try
            {
                var dishes = await _setmealService.GetSetmealByCategoryIdAsync(categoryId.Value);


                // 返回查询结果，这里直接返回 dishes
                return Ok(new { code = true, data = dishes });
            }
            catch (System.Exception ex)
            {
                // 在生产环境中，更详细的错误处理很重要，包括记录日志
                return StatusCode(500, new { code = false, message = "Internal server error: " + ex.Message });
            }
        }


        [HttpGet("dish/{id:long}")]
        public async Task<IActionResult> GetDishesBySetmealId(long id)
        {
            var dishDtos = await _setmealService.GetDishesBySetmealIdAsync(id);
            return Ok(new { code=true, data=dishDtos });
        }
    }
}
