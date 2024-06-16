using MS.Entities;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IOrderService : IBaseService
    {
        // 下单
        Task<OrderResponseDto> SubmitOrderAsync(Orders submitOrderDto, long userId);
        // 支付
        Task<PaymentInfoDto> PayOrderAsync(string orderNumber, int payMethod);

    }
}
