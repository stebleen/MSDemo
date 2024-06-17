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

    }
}
