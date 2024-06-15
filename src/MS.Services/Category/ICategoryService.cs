using MS.Entities;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<IEnumerable<Category>> GetCategoriesByType(int? type);
    }
}
