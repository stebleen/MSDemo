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


        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAddressBookById(long id)
        {
            var addressBook = await _addressBookService.GetAddressBookByIdAsync(id);

            if (addressBook == null)
            {
                return NotFound(new { code = false, msg = "未找到相应的地址信息。" });
            }

            return Ok(new
            {
                code = true,
                data = addressBook,
                msg = "获取默认地址成功"
            }); ;
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAddressBook([FromQuery] long id) // 注意这里的改动
        {
            var result = await _addressBookService.DeleteAddressBookByIdAsync(id);
            if (result)
            {
                return Ok(new { code=true,message = "地址删除成功" });
            }

            return NotFound(new { code=false,message = "未找到要删除的地址" });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAddressBook([FromBody] AddressBook updateDto)
        {
            var result = await _addressBookService.UpdateAddressBookAsync(updateDto);
            if (result)
            {
                return Ok(new { code=true,message = "地址更新成功" });
            }

            return NotFound(new { code=false,message = "未找到要更新的地址" });
        }


        [HttpPost]
        public async Task<IActionResult> AddAddressBook([FromBody] AddressBook createDto)
        {
            var addressBook = await _addressBookService.AddAddressBookAsync(createDto);
            if (addressBook != null)
            {
                // 构建成功时返回的JSON结构
                var successResponse = new
                {
                    code = true,
                    data = addressBook, // 或使用匿名对象装载您想返回的地址信息
                    msg = "新增地址成功"
                };
                return Ok(successResponse);
            }
            else
            {
                // 构建失败时返回的JSON结构
                var errorResponse = new
                {
                    code = false,
                    data = new { }, // 根据需要可返回额外信息
                    msg = "新增地址失败"
                };
                return BadRequest(errorResponse);
            }
        }


    }



}

