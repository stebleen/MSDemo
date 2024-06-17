using MS.Entities;
using MS.Entities.admin;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<IEnumerable<Category>> GetCategoriesByType(int? type);

        // admin
        Task<CategoryPageResponseDto> GetCategoryPageAsync(CategoryPageRequestDto requestDto);

        Task<IEnumerable<CategoryDto>> GetCategoryListAsync(CategoryListRequestDto requestDto);
    }
}

