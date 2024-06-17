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

namespace MS.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;
        public CategoryService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByType(int? type)
        {
            var categoriesQuery = _unitOfWork.GetRepository<Category>().GetAll();

            if (type.HasValue)
            {
                categoriesQuery = categoriesQuery.Where(c => c.Type == type.Value);
            }

            return await categoriesQuery.ToListAsync();
        }


        public async Task<CategoryPageResponseDto> GetCategoryPageAsync(CategoryPageRequestDto requestDto)
        {
            int.TryParse(requestDto.Page, out int pageNumber);
            int.TryParse(requestDto.PageSize, out int pageSize);
            int.TryParse(requestDto.Type, out int type);

            var query = _unitOfWork.GetRepository<Category>().GetAll();

            // Apply filters
            if (!string.IsNullOrEmpty(requestDto.Name))
            {
                query = query.Where(c => c.Name.Contains(requestDto.Name));
            }
            if (!string.IsNullOrEmpty(requestDto.Type))
            {
                query = query.Where(c => c.Type == type);
            }

            var total = await query.CountAsync();

            var records = await query
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Type = c.Type,
                    Name = c.Name,
                    Sort = c.Sort,
                    Status = c.Status,
                    CreateTime = c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdateTime = c.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateUser = c.CreateUser,
                    UpdateUser = c.UpdateUser
                })
                .ToListAsync();

            return new CategoryPageResponseDto
            {
                Total = total,
                Records = records
            };
        }


        public async Task<IEnumerable<CategoryDto>> GetCategoryListAsync(CategoryListRequestDto requestDto)
        {
            int type = 0;
            if (!string.IsNullOrEmpty(requestDto.Type))
            {
                int.TryParse(requestDto.Type, out type);
            }

            var categories = await _unitOfWork.GetRepository<Category>()
                .GetAll()
                .Where(c => c.Type == type || type == 0)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Type = c.Type,
                    Name = c.Name,
                    Sort = c.Sort,
                    Status = c.Status,
                    CreateTime = c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdateTime = c.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateUser = c.CreateUser,
                    UpdateUser = c.UpdateUser
                })
                .ToListAsync();

            return categories;
        }


        public async Task<bool> UpdateCategoryAsync(ModifyCategoryDto categoryDto)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(predicate:c => c.Id == categoryDto.Id);

            if (category == null)
            {
                return false;
            }

            category.Name = categoryDto.Name;
            category.Sort = categoryDto.Sort;
            category.Type = categoryDto.Type;

            //category.UpdateTime = DateTime.UtcNow; 

            _unitOfWork.GetRepository<Category>().Update(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteCategoryByIdAsync(long categoryId)
        {
            var repo = _unitOfWork.GetRepository<Category>();
            var category = await repo.GetFirstOrDefaultAsync(predicate: a => a.Id == categoryId);

            if (category != null)
            {
                repo.Delete(category);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            // Id 自增
            _unitOfWork.GetRepository<Category>().Insert(category);
            await _unitOfWork.SaveChangesAsync();


            return category;
        }

        public async Task<Category> AddCategoryAsync(AddCategoryDto categoryDto)
        {

            // 尝试将字符串转换为整数
            var successSort = int.TryParse(categoryDto.Sort, out var sort);
            var successType = int.TryParse(categoryDto.Type, out var type);



            var newCategory = new Category
            {
                Name = categoryDto.Name,
                Sort = sort,
                Type = type,
                Status = 1, // 默认启用状态
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
            };

            _unitOfWork.GetRepository<Category>().Insert(newCategory);
            await _unitOfWork.SaveChangesAsync();
            return newCategory;
        }


        public async Task<bool> UpdateCategoryStatusAsync(long id, int status)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(predicate:c => c.Id == id);

            if (category != null)
            {
                category.Status = status; 
                _unitOfWork.GetRepository<Category>().Update(category);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }



    }
}
