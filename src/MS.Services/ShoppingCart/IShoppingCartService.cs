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
        // 查看购物车
        Task<IEnumerable<ShoppingCart>> GetShoppingCart(long UserId);
        // 添加购物车
        Task<ExecuteResult<ShoppingCart>> AddToCartAsync(AddToCartDto addToCartDto, long userId);
        // 删除购物车中一个商品
        Task<ExecuteResult<bool>> SubFromCartAsync(AddToCartDto removeFromCartDto, long userId);
        // 清空购物车
        Task<ExecuteResult<bool>> CleanCartAsync(long userId);
    }
}
