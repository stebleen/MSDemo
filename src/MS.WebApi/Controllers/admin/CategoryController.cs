using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers.admin
{
    [Route("admin/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetCategoryPage([FromQuery] CategoryPageRequestDto requestDto)
        {
            var response = await _categoryService.GetCategoryPageAsync(requestDto);
            return Ok(new { data=response,code=true });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCategoryList([FromQuery] CategoryListRequestDto requestDto)
        {
            var categories = await _categoryService.GetCategoryListAsync(requestDto);
            return Ok(new{ data=categories,code=true});
        }


    }
}
