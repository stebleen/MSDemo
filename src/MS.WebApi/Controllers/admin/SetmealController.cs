using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
using MS.Entities;
using System;
using MS.Entities.admin;

namespace MS.WebApi.Controllers.admin
{

    [Route("admin/setmeal")]
    [ApiController]
    public class SetmealController : ControllerBase
    {
        private readonly ISetmealService _setmealService;

        public SetmealController(ISetmealService setmealService)
        {
            _setmealService = setmealService;
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetSetmeals([FromQuery] SetmealPageRequestDto requestDto)
        {
            var responseDto = await _setmealService.GetSetmealPageAsync(requestDto);
            return Ok(new { data=responseDto,code=true });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSetmeal([FromRoute] long id)
        {
            var setmealDetails = await _setmealService.GetSetmealByIdAsync(id);
            if (setmealDetails != null)
            {
                return Ok(new { code = true, data = setmealDetails, msg = "" });
            }
            else
            {
                return NotFound(new { code = false, msg = "Setmeal not found" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSetmeal([FromBody] AddSetmealDto setmealDto)
        {
            var createdsetmeal = await _setmealService.CreateSetmealAsync(setmealDto);
            if (createdsetmeal)
            {
                return Ok(new { code = true, data = "Setmeal added successfully", msg = "Success" });
            }
            else
            {
                return BadRequest(new { code = false, data = "string", msg = "Failed to add setmeal" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSetmeals([FromQuery] string ids)
        {
            var success = await _setmealService.DeleteSetmealsAsync(ids);

            if (success)
            {
                return Ok(new { code = true, data = "Setmeals deleted successfully", msg = "Success" });
            }
            else
            {
                return BadRequest(new { code = false, data = "string", msg = "Cannot delete setmealDishes referenced by setmeals" });
            }
        }


        [HttpPost("status/{status}")]
        public async Task<IActionResult> UpdateSetmealStatus([FromRoute] string status, [FromQuery] string id)
        {
            // 尝试将路径参数和查询参数转换为整数
            if (!int.TryParse(status, out var intStatus) || !long.TryParse(id, out var longId))
            {
                return BadRequest(new { code = false, data = "string", msg = "Invalid parameters." });
            }

            var success = await _setmealService.UpdateSetmealStatusAsync(longId, intStatus);

            if (success)
            {
                return Ok(new { code = true, data = "Setmeal status updated successfully", msg = "Success" });
            }
            else
            {
                return NotFound(new { code = false, data = "string", msg = "Setmeal not found" });
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStemeal([FromBody] Setmeal setmealDto)
        {
            var result = await _setmealService.UpdateSetmealAsync(setmealDto);
            if (result)
            {
                return Ok(new { code = true, data = "Setmeal updated successfully", msg = "Success" });
            }
            else
            {
                return BadRequest(new { code = false, data = "string", msg = "Failed to update setmeal" });
            }
        }


    }
}
