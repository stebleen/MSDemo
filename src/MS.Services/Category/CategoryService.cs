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


    }
}
