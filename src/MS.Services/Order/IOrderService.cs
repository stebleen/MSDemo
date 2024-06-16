using MS.Entities;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IOrderService : IBaseService
    {
        Task<OrderResponseDto> SubmitOrderAsync(Orders submitOrderDto, long userId);

    }
}
