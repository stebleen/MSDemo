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
    }
}
