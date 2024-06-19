using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
using System;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace MS.WebApi.Controllers.admin
{
   
    [Route("admin/workspace")]
    [ApiController]
    public class BusinessDataController : ControllerBase
    {
        private readonly IBusinessDataService _businessDataService;

        public BusinessDataController(IBusinessDataService businessDataService)
        {
            _businessDataService = businessDataService;
        }

        [HttpGet("businessData")]
        public async Task<IActionResult> GetTodayBusinessData()
        {
            var businessData = await _businessDataService.GetTodayBusinessDataAsync();
            return Ok(new { code = true, data = businessData, msg = "Success" });
        }
    }
}
