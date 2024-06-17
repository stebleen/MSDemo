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



        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmOrder([FromBody] OrdersConfirmDTO request)
        {
            bool success = await _orderService.ConfirmOrderAsync(request.Id);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Order confirmed successfully." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Order not found." });
            }
        }

        public class OrdersConfirmDTO
        {
            public long Id { get; set; }
        }


        [HttpPut("delivery/{id}")]
        public async Task<IActionResult> DeliverOrder([FromRoute] long id)
        {
            bool success = await _orderService.DeliverOrderAsync(id);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Order is now being delivered." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Order not found." });
            }
        }

        [HttpPut("complete/{id}")]
        public async Task<IActionResult> FinishOrder([FromRoute] long id)
        {
            bool success = await _orderService.FinishOrderAsync(id);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Order is now being finished." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Order not found." });
            }
        }


        [HttpPost("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] OrdersCancelDTO orderCancelDto)
        {
            bool success = await _orderService.CancelOrderAsync(orderCancelDto);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Order cancelled successfully." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Order not found." });
            }
        }

        [HttpPost("rejection")]
        public async Task<IActionResult> RejectionOrder([FromBody] OrdersRejectionDTO orderRejectionDto)
        {
            bool success = await _orderService.RejectionOrderAsync(orderRejectionDto);

            if (success)
            {
                return Ok(new { code = true, data = new { }, msg = "Order rejected successfully." });
            }
            else
            {
                return NotFound(new { code = false, data = new { }, msg = "Order not found." });
            }
        }



    }
}
