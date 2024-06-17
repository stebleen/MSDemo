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
using MS.Entities.admin;
using System.Globalization;

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



        public async Task<SetmealPageResponseDto> GetSetmealPageAsync(SetmealPageRequestDto requestDto)
        {
            var setmeals = _unitOfWork.GetRepository<Setmeal>().GetAll();
            var categories = _unitOfWork.GetRepository<Category>().GetAll();

            var query = from setmeal in setmeals
                        join category in categories
                        on setmeal.CategoryId equals category.Id
                        select new SetmealDto
                        {
                            Id = setmeal.Id,
                            CategoryId = setmeal.CategoryId,
                            CategoryName = category.Name,
                            Name = setmeal.Name,
                            Price = setmeal.Price,
                            Status = setmeal.Status,
                            Description = setmeal.Description,
                            Image = setmeal.Image,
                            UpdateTime = setmeal.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        };

            // 应用筛选条件
            if (!string.IsNullOrEmpty(requestDto.CategoryId))
            {
                var categoryId = long.Parse(requestDto.CategoryId);
                query = query.Where(s => s.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(requestDto.Name))
            {
                query = query.Where(s => s.Name.Contains(requestDto.Name));
            }
            if (!string.IsNullOrEmpty(requestDto.Status))
            {
                var status = int.Parse(requestDto.Status);
                query = query.Where(s => s.Status == status);
            }

            int total = await query.CountAsync();
            List<SetmealDto> records = await query
                .Skip((requestDto.Page - 1) * requestDto.PageSize)
                .Take(requestDto.PageSize)
                .ToListAsync();

            return new SetmealPageResponseDto { Total = total, Records = records };
        }


        public async Task<SetmealVO> GetSetmealByIdAsync(long setmealId)
        {
            var setmeal = await _unitOfWork.GetRepository<Setmeal>().GetFirstOrDefaultAsync(
                predicate: sm => sm.Id == setmealId
            );

            if (setmeal == null) return null;

            var category = await _unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(
                predicate: c => c.Id == setmeal.CategoryId
            );

            var setmealDishes = await _unitOfWork.GetRepository<SetmealDish>().GetAll()
                .Where(smd => smd.SetmealId == setmeal.Id).ToListAsync();

            return new SetmealVO
            {
                Id = setmeal.Id,
                CategoryId = setmeal.CategoryId,
                CategoryName = category?.Name,
                Description = setmeal.Description,
                Image = setmeal.Image,
                Name = setmeal.Name,
                Price = setmeal.Price,
                Status = setmeal.Status,
                UpdateTime = setmeal.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                SetmealDishes = setmealDishes.Select(d => new SetmealDishVo
                {
                    Copies = d.Copies,
                    DishId = d.DishId,
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                    SetmealId = d.SetmealId
                }).ToList()
            };
        }

        public async Task<bool> CreateSetmealAsync(AddSetmealDto setmeal)
        {
            var success = decimal.TryParse(setmeal.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var priceValue);
            var _setmeal = new Setmeal
            {
                CategoryId = setmeal.CategoryId,
                Description = setmeal.Description,
                Image = setmeal.Image,
                Name = setmeal.Name,
                Price = priceValue,
                Status = setmeal.Status,
                UpdateTime = DateTime.UtcNow,
                CreateTime = DateTime.UtcNow,
                UpdateUser =2,
                CreateUser =2

            };
            _unitOfWork.GetRepository<Setmeal>().Insert(_setmeal);
            await _unitOfWork.SaveChangesAsync();
            
            if(setmeal != null)
            {

                

                
                _setmeal.setmealDishes=setmeal.setmealDishes.Select(f=> new SetmealDish {
                    Copies = f.Copies,
                    DishId = f.DishId,
                    Name = f.Name,
                    Price = f.Price,
                    SetmealId = f.SetmealId
                }).ToList();
                

                _unitOfWork.GetRepository<Setmeal>().Update(_setmeal);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;

        }


        public async Task<bool> DeleteSetmealsAsync(string ids)
        {
            var idsArray = ids.Split(',').Select(id => long.Parse(id)).ToList();

            

            foreach (var id in idsArray)
            {
                var setmeal = await _unitOfWork.GetRepository<Setmeal>().GetFirstOrDefaultAsync(predicate: d => d.Id == id);
                if (setmeal != null)
                {
                    // 删除菜品相关的口味信息
                    var setmealDish = await _unitOfWork.GetRepository<SetmealDish>().GetAll()
                                          .Where(df => df.SetmealId == id).ToListAsync();
                    foreach (var flavor in setmealDish)
                    {
                        _unitOfWork.GetRepository<SetmealDish>().Delete(flavor);
                    }

                    // 删除菜品
                    _unitOfWork.GetRepository<Setmeal>().Delete(setmeal);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateSetmealStatusAsync(long id, int status)
        {
            var setmeal = await _unitOfWork.GetRepository<Setmeal>().GetFirstOrDefaultAsync(predicate: c => c.Id == id);

            if (setmeal != null)
            {
                setmeal.Status = status;
                _unitOfWork.GetRepository<Setmeal>().Update(setmeal);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateSetmealAsync(Setmeal setmealDto)
        {
            

            var setmeal = await _unitOfWork.GetRepository<Setmeal>().GetFirstOrDefaultAsync(predicate: d => d.Id == setmealDto.Id);

            if (setmeal == null)
            {
                return false; // 如果不存在该菜品，返回false
            }

            // 更新菜品信息
            setmeal.Name = setmealDto.Name;
            setmeal.Price = setmeal.Price;
            setmeal.Description = setmealDto.Description ?? setmeal.Description; // 允许部分更新
            setmeal.Image = setmealDto.Image;
            setmeal.CategoryId = setmealDto.CategoryId;
            setmeal.Status = 1;


            

            setmeal.setmealDishes = setmealDto.setmealDishes.Select(f => new SetmealDish
            {

                Name = f.Name,
                Copies = f.Copies,
                DishId = f.DishId,
                Price = f.Price, 

            }).ToList();
            
            

            _unitOfWork.GetRepository<Setmeal>().Update(setmeal);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
