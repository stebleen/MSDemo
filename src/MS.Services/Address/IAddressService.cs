using MS.Entities;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IAddressService : IBaseService
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync();
    }
}
