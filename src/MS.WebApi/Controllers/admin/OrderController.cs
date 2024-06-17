using Microsoft.AspNetCore.Mvc;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;
using MS.Services;
using Renci.SshNet.Messages;
using MS.Entities;
using System;
using MS.Entities.admin;
using Ubiety.Dns.Core;

namespace MS.WebApi.Controllers.admin
{
    [Route("admin/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("conditionSearch")]
        public async Task<IActionResult> SearchOrders([FromQuery] OrderSearchRequestDto requestDto)
        {
            var response = await _orderService.SearchOrdersAsync(requestDto);
            return Ok(new { code = true, data = response });
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetOrderStatistics()
        {
            var statistics = await _orderService.GetOrderStatisticsAsync();
            return Ok(new {
                code = true,
                data = statistics,
            });
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetOrderDetails([FromRoute] long id)
        {
            var orderDetails = await _orderService.GetOrderDetailsAsync(id);

            return Ok(new
            {
                code = true,
                data = orderDetails,
            });

            
        }


    }
}
