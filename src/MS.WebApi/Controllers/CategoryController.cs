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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        // 通过构造函数注入服务
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] int? type)
        {
            try
            {
                var categories = await _categoryService.GetCategoriesByType(type);
                // return Ok(new { data = categories });
                return Ok(new { code = true, data = categories ,Message="null"});
            }
            catch (System.Exception ex)
            {
                // return StatusCode(500, "Internal server error: " + ex.Message);
                return StatusCode(500, new { code = false, message = "Internal server error: " + ex.Message });
            }
        }
    }
}
