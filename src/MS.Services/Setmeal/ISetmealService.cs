using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface ISetmealService : IBaseService
    {
        Task<IEnumerable<Setmeal>> GetSetmealByCategoryIdAsync(long categoryId);
        Task<IEnumerable<DishDto>> GetDishesBySetmealIdAsync(long setmealId);
    }
}
