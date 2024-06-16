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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitOrder([FromBody] Orders submitOrderDto)
        {
      
            var userId = 7;
            if (submitOrderDto == null)
            {
                return BadRequest("提交的订单数据不能为空");
            }

            var orderResponse = await _orderService.SubmitOrderAsync(submitOrderDto, userId);

            return Ok(new
            {
                code = true,
                data = orderResponse,
                msg = "成功创建订单"
            });
        }
    }
}
