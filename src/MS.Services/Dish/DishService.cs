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
    public class DishService : BaseService, IDishService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;

        public DishService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Dish>> GetDishesByCategoryIdAsync(long categoryId)
        {
            /*
            return await _unitOfWork.GetRepository<Dish>()
                         .GetAll() 
                         .Where(dish => dish.CategoryId == categoryId && dish.Status == 1)
                         .ToListAsync();
            */

            var dishes = await _unitOfWork.GetRepository<Dish>()
                                   .GetAll()
                                   .Include(dish => dish.Flavors) // 预加载Flavors
                                   .Where(dish => dish.CategoryId == categoryId && dish.Status == 1)
                                   .ToListAsync();
            

            // 使用AutoMapper或手动映射到DishDto
            var dishDtos = dishes.Select(d => new Dish
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                Description = d.Description,
                Image = d.Image,
                Status = d.Status,
                UpdateTime = d.UpdateTime,
                Flavors = d.Flavors.Select(f => new DishFlavor
                {
                    Id = f.Id,
                    Name = f.Name,
                    Value = f.Value
                }).ToList()
            });

            return dishDtos;
        }


        public async Task<DishPageResponseDto> GetDishPageAsync(DishPageRequestDto requestDto)
        {
            var dishes = _unitOfWork.GetRepository<Dish>().GetAll();
            var categories = _unitOfWork.GetRepository<Category>().GetAll();

            var query = from dish in dishes
                        join category in categories
                        on dish.CategoryId equals category.Id
                        select new AdminDishDto
                        {
                            Id = dish.Id,
                            CategoryId = dish.CategoryId,
                            CategoryName = category.Name,
                            Name = dish.Name,
                            Price = dish.Price,
                            Status = dish.Status,
                            Description = dish.Description,
                            Image = dish.Image,
                            // UpdateTime = dish.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            UpdateTime = dish.UpdateTime.HasValue ? dish.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未知",
                        };

            if (!string.IsNullOrEmpty(requestDto.CategoryId))
            {
                var categoryId = long.Parse(requestDto.CategoryId);
                query = query.Where(d => d.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(requestDto.Name))
            {
                query = query.Where(d => d.Name.Contains(requestDto.Name));
            }
            if (!string.IsNullOrEmpty(requestDto.Status))
            {
                var status = int.Parse(requestDto.Status);
                query = query.Where(d => d.Status == status);
            }

            int total = await query.CountAsync();
            List<AdminDishDto> records = await query
                .OrderBy(d => d.Id)
                 .Skip((requestDto.Page - 1) * requestDto.PageSize)
                .Take(requestDto.PageSize)
                .ToListAsync();

            return new DishPageResponseDto
            {
                Total = total,
                Records = records
            };
        }



        public async Task<DishByIdResponse> GetDishByIdAsync(long dishId)
        {
            var dish = await _unitOfWork.GetRepository<Dish>().GetFirstOrDefaultAsync(
                predicate: d => d.Id == dishId,
                include: source => source.Include(d => d.Flavors)
            );

            if (dish == null) return null;

            var category = await _unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(
                predicate: c => c.Id == dish.CategoryId
            );

            return new DishByIdResponse
            {
                Id = dish.Id,
                CategoryId = dish.CategoryId,
                CategoryName = category?.Name,
                Description = dish.Description,
                Image = dish.Image,
                Name = dish.Name,
                Price = dish.Price,
                Status = dish.Status,
                UpdateTime = dish.UpdateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                
                Flavors = dish.Flavors.Select(f => new DishFlavor
                {
                    DishId = f.DishId,
                    Id = f.Id,
                    Name = f.Name,
                    Value = f.Value
                }).ToList()
            };
        }


        public async Task<bool> UpdateDishStatusAsync(long id, int status)
        {
            var dish = await _unitOfWork.GetRepository<Dish>().GetFirstOrDefaultAsync(predicate: c => c.Id == id);

            if (dish != null)
            {
                dish.Status = status;
                _unitOfWork.GetRepository<Dish>().Update(dish);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<Dish> AddDishAsync(AddDishDto dishDto)
        {
            // 尝试将价格字符串转换为decimal。如果转换失败，则使用默认值0
            var success = decimal.TryParse(dishDto.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var priceValue);
            if (!success)
            {
                priceValue = 0M;
            }
            var dish = new Dish
            {
                CategoryId = dishDto.CategoryId,
                Name = dishDto.Name,
                //Price = dishDto.Price,
                Price = priceValue,
                Status = 1, // 默认为起售状态
                //Image = dishDto.Image,
                Image= "https://se-project-tongji.oss-cn-shanghai.aliyuncs.com/de5e77f2-d72a-442b-a9d5-b5301dc3bc9d.jpg",
                Description = dishDto.Description,
                CreateUser = 2,
                UpdateUser = 2,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                /*
                Flavors = dishDto.Flavors.Select(f => new DishFlavor
                {
                    Name = f.Name,
                    Value = f.Value,
                    DishId = dishDto.Id,
                }).ToList()
                */
            };

            _unitOfWork.GetRepository<Dish>().Insert(dish);
            await _unitOfWork.SaveChangesAsync();

            // 现在Dish有了Id
            if (dishDto.Flavors != null)
            {
                dish.Flavors = dishDto.Flavors.Select(f => new DishFlavor
                {
                    Name = f.Name,
                    Value = f.Value,
                    DishId = dish.Id, 
                }).ToList();

                // 再次保存
                _unitOfWork.GetRepository<Dish>().Update(dish);
                await _unitOfWork.SaveChangesAsync();
            }

            return dish;
        }


        public async Task<bool> UpdateDishAsync(AddDishDto dishDto)
        {
            var success = decimal.TryParse(dishDto.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var priceValue);
            if (!success)
            {
                priceValue = 0M;
            }
            /*
            if (dishDto.Id == null)
            {
                return false; 
            }
            */

            var dish = await _unitOfWork.GetRepository<Dish>().GetFirstOrDefaultAsync(predicate:d => d.Id == dishDto.Id);

            if (dish == null)
            {
                return false; // 如果不存在该菜品，返回false
            }

            // 更新菜品信息
            dish.Name = dishDto.Name;
            dish.Price = priceValue;
            dish.Description = dishDto.Description ?? dish.Description; // 允许部分更新
            dish.Image = dishDto.Image;
            dish.CategoryId = dishDto.CategoryId;
            dish.Status = 1; 
            dish.Flavors = dishDto.Flavors.Select(f => new DishFlavor
            {
                Name = f.Name,
                Value = f.Value
                
            }).ToList();

            _unitOfWork.GetRepository<Dish>().Update(dish);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }
}
