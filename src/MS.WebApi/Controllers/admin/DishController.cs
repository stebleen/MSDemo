using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
using MS.Entities;
using System;


namespace MS.WebApi.Controllers.admin
{
    [Route("admin/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetDishesPage([FromQuery] DishPageRequestDto requestDto)
        {
            var response = await _dishService.GetDishPageAsync(requestDto);
            return Ok(new { data=response,code=true });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDish([FromRoute] long id)
        {
            var dishByIdResponse = await _dishService.GetDishByIdAsync(id);
            if (dishByIdResponse != null)
            {
                return Ok(new 
                {
                    Code = true,
                    Data = dishByIdResponse,
                    Msg = ""
                });
            }
            else
            {
                return NotFound(new { Code = false, Msg = "Dish not found" });
            }
        }

        [HttpPost("status/{status}")]
        public async Task<IActionResult> UpdateDishStatus([FromRoute] string status, [FromQuery] string id)
        {
            // 尝试将路径参数和查询参数转换为整数
            if (!int.TryParse(status, out var intStatus) || !long.TryParse(id, out var longId))
            {
                return BadRequest(new { code = false, data = "string", msg = "Invalid parameters." });
            }

            var success = await _dishService.UpdateDishStatusAsync(longId, intStatus);

            if (success)
            {
                return Ok(new { code = true, data = "Dish status updated successfully", msg = "Success" });
            }
            else
            {
                return NotFound(new { code = false, data = "string", msg = "Dish not found" });
            }
        }



    }
    
}
