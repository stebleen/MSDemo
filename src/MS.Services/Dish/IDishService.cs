using MS.Entities;
using MS.Entities.admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IDishService : IBaseService
    {
        Task<IEnumerable<Dish>> GetDishesByCategoryIdAsync(long categoryId);

        // admin
        Task<DishPageResponseDto> GetDishPageAsync(DishPageRequestDto requestDto);

        Task<DishByIdResponse> GetDishByIdAsync(long dishId);


    }
}
