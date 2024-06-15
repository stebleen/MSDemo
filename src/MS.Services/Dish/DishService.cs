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
    }
}
