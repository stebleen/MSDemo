using AutoMapper;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MS.Common.Extensions;

namespace MS.Services
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;
        public ShoppingCartService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCart(long userId)
        {
            var allItems = await _unitOfWork.GetRepository<ShoppingCart>().GetAllAsync();
            return allItems.Where(item => item.UserId == userId).ToList();
        }

        public async Task<ExecuteResult<ShoppingCart>> AddToCartAsync(AddToCartDto addToCartDto, long userId)
        {
            ExecuteResult<ShoppingCart> result = new ExecuteResult<ShoppingCart>();

            // 使用 GetFirstOrDefaultAsync 方法查找已存在的购物车项
            var existingItem = await _unitOfWork.GetRepository<ShoppingCart>().GetFirstOrDefaultAsync(
                predicate: s => s.UserId == userId &&
                                ((addToCartDto.DishId.HasValue && s.DishId == addToCartDto.DishId) ||
                                 (addToCartDto.SetmealId.HasValue && s.SetmealId == addToCartDto.SetmealId)),
                disableTracking: false); // 追踪以便于更新

            if (existingItem != null)
            {
                // 如果购物车项已存在，则增加数量
                existingItem.Number += 1;
                // 直接调用 SaveChangesAsync 提交更改，因为实体已被追踪
            }
            else
            {
                // 初始化一个新的购物车项
                var newItem = new ShoppingCart
                {
                    UserId = userId,
                    DishId = addToCartDto.DishId,
                    SetmealId = addToCartDto.SetmealId,
                    DishFlavor = addToCartDto.DishFlavor,
                    Number = 1 // 设定默认数量为1
                };
                // 如果不存在，先获取价格
                decimal price = 0;
                if (addToCartDto.DishId.HasValue)
                {
                    // 如果添加的是菜品
                    var dish = await _unitOfWork.GetRepository<Dish>().FindAsync(addToCartDto.DishId.Value);
                    if (dish == null)
                    {
                        return result.SetFailMessage("指定的菜品不存在");
                    }
                    price = dish?.Price ?? 0;

                    // 设置价格、名称和图片
                    newItem.Amount = price;
                    newItem.Name = dish.Name;
                    newItem.Image = dish.Image;
                }
                else if (addToCartDto.SetmealId.HasValue)
                {
                    var setmeal = await _unitOfWork.GetRepository<Setmeal>().FindAsync(addToCartDto.SetmealId.Value);
                    price = setmeal?.Price ?? 0;
                    // 设置价格、名称和图片
                    newItem.Amount = price;
                    newItem.Name = setmeal.Name;
                    newItem.Image = setmeal.Image;
                }

                

                // 使用 InsertAsync 方法添加新项
                await _unitOfWork.GetRepository<ShoppingCart>().InsertAsync(newItem);
            }

            // 保存所有更改
            await _unitOfWork.SaveChangesAsync();

            result.Code = true;
            result.Message = "购物车更新成功";
            result.Data = existingItem ?? new ShoppingCart(); // 返回更新后的或新增的项

            return result;
        }


        public async Task<ExecuteResult<bool>> SubFromCartAsync(AddToCartDto removeFromCartDto, long userId)
        {
            ExecuteResult<bool> result = new ExecuteResult<bool>();

            // 利用 GetFirstOrDefaultAsync 寻找购物车中的对应商品
            var cartItem = await _unitOfWork.GetRepository<ShoppingCart>().GetFirstOrDefaultAsync(
                s => s.UserId == userId &&
                    ((removeFromCartDto.DishId.HasValue && s.DishId == removeFromCartDto.DishId) ||
                    (removeFromCartDto.SetmealId.HasValue && s.SetmealId == removeFromCartDto.SetmealId)),
                null, // orderBy => 此处不需要排序
                null, // include => 如果有关联数据可以在此处加载
                false); // disableTracking => 设置为 false 以便对实体进行修改

            // 判断找到的购物车项是否存在
            if (cartItem == null)
            {
                result.SetFailMessage("购物车中未找到指定的商品");
                return result;
            }

            if (cartItem.Number > 1)
            {
                // 商品数量大于1，只需减少数量
                cartItem.Number -= 1;
            }
            else
            {
                // 如果商品数量为1，直接删除该商品记录
                _unitOfWork.GetRepository<ShoppingCart>().Delete(cartItem);
            }

            await _unitOfWork.SaveChangesAsync(); // 保存更改

            result.Data = true;
            result.Code = true;
            result.Message = "购物车更新成功";

            return result;
        }

    }

}