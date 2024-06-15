using MS.Entities;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IShoppingCartService : IBaseService
    {
        Task<IEnumerable<ShoppingCart>> GetShoppingCart(long UserId);
        Task<ExecuteResult<ShoppingCart>> AddToCartAsync(AddToCartDto addToCartDto, long userId);
        Task<ExecuteResult<bool>> SubFromCartAsync(AddToCartDto removeFromCartDto, long userId);
    }
}
