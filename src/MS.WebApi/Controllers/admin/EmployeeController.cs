using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
using MS.Entities;
using MS.Entities.admin;

namespace MS.WebApi.Controllers.admin
{
    [ApiController]
    [Route("/admin/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var loginResponse = await _employeeService.LoginAsync(loginRequest);

            if (loginResponse == null)
            {
                return Unauthorized(new { code = false, message = "Username or password is incorrect" });
            }

            return Ok(new { code = true, data = loginResponse });
        }


        /*
        [HttpGet("page")]
        public async Task<IActionResult> GetEmployeePage([FromQuery] EmployeePageRequestDto requestDto)
        {
            var response = await _employeeService.GetEmployeePageAsync(requestDto);
            return Ok(new { code = true, data = response });
        }
        */


    }
}
