using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
using System;
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


        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] ModifyCategoryDto categoryDto)
        {
            var success = await _categoryService.UpdateCategoryAsync(categoryDto);

            if (success)
            {
                return Ok(new { code = true, data = "Category updated successfully", msg = "Success" });
            }
            else
            {
                return NotFound(new { code = false, data = "string", msg = "Category not found" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] long id)
        {
            bool success = await _categoryService.DeleteCategoryByIdAsync(id);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Category deleted successfully." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Category not found." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] AddCategoryDto categoryDto)
        {
            try
            {
                var createdCategory = await _categoryService.AddCategoryAsync(categoryDto);
                return Ok(new
                {
                    code = true,
                    data = new { id = createdCategory.Id },
                    msg = "Category created successfully"
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

        [HttpPost("status/{status}")]
        public async Task<IActionResult> UpdateCategoryStatus([FromRoute] string status, [FromQuery] string id)
        {
            // 尝试将路径参数和查询参数转换为整数
            if (!int.TryParse(status, out var intStatus) || !long.TryParse(id, out var longId))
            {
                return BadRequest(new { code = false, data = "string", msg = "Invalid parameters." });
            }

            var success = await _categoryService.UpdateCategoryStatusAsync(longId, intStatus);

            if (success)
            {
                return Ok(new { code = true, data = "Category status updated successfully", msg = "Success" });
            }
            else
            {
                return NotFound(new { code = false, data = "string", msg = "Category not found" });
            }
        }



    }
}
