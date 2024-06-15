using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
using MS.Entities;
using System;

namespace MS.WebApi.Controllers
{
    [ApiController]
    [Route("/user/[controller]")]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookService _addressBookService;

        public AddressBookController(IAddressBookService addressBookService)
        {
            _addressBookService = addressBookService;
        }

        // GET: /user/addressBook/list
        [HttpGet("list")]
        public async Task<IActionResult> List(long userId) 
        {
            userId = 7;
            var items = await _addressBookService.GetAddressBook(userId);
            return Ok(new { code = true, data = items });
        }


        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultAddress()
        {
           
            long userId = 7; 

            var address = await _addressBookService.GetDefaultAddressAsync(userId);

            if (address != null)
            {
                return Ok(new
                {
                    code = true,
                    data = address,
                    msg = "获取默认地址成功"
                });
            }

            return NotFound(new
            {
                code = false,
                msg = "未找到默认地址"
            });
        }

        [HttpPut("default")] 
        public async Task<IActionResult> SetDefaultAddress([FromBody] SetDefaultAddressDto dto)
        {
            // 输出 "dto.Id" 的值
            Console.WriteLine($"AddressBook ID: {dto.Id}");

            // 尝试将 "dto" 对象序列化为一个字符串来输出（需要 using System.Text.Json;）
            Console.WriteLine($"DTO object: {System.Text.Json.JsonSerializer.Serialize(dto)}");


            var result = await _addressBookService.SetDefaultAddressAsync(dto.Id);

            if (result)
            {
                return Ok(new
                {
                    code = true,
                    data = new { },
                    msg = "默认地址设置成功"
                });
            }
            else
            {
                return Ok(new
                {
                    code = false,
                    data = new { },
                    msg = "默认地址设置失败，地址不存在"
                });
            }
        }
    }



}

