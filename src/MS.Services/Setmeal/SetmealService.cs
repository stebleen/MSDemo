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
    public class SetmealService : BaseService, ISetmealService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;

        public SetmealService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Setmeal>> GetSetmealByCategoryIdAsync(long categoryId)
        {
            return await _unitOfWork.GetRepository<Setmeal>()
                         .GetAll()
                         .Where(setmeal => setmeal.CategoryId == categoryId && setmeal.Status == 1)
                         .ToListAsync();
        }

        public async Task<IEnumerable<DishDto>> GetDishesBySetmealIdAsync(long setmealId)
        {
            // 查询SetmealDish表找出与套餐相关的菜品ID
            var setmealDishes = await _unitOfWork.GetRepository<SetmealDish>().GetAllAsync(sd => sd.SetmealId == setmealId);

            // 准备要返回的菜品信息列表
            var dishesInfo = new List<DishDto>();

            foreach (var setmealDish in setmealDishes)
            {
                // 根据DishId查询Dish表获取菜品详细信息
                var dish = await _unitOfWork.GetRepository<Dish>().GetFirstOrDefaultAsync(
                     predicate: d => d.Id == setmealDish.DishId,
                     disableTracking: false
                 );
                if (dish != null)
                {
                    dishesInfo.Add(new DishDto
                    {
                        Copies = setmealDish.Copies,
                        Description = dish.Description, // 假设您希望显示的是菜品的描述，而非setmealDish的名称
                        Image = dish.Image,
                        Name = dish.Name
                    });
                }
            }

            return dishesInfo;
        }
    }
}
