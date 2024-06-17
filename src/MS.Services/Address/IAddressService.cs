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


        // admin
        Task<IEnumerable<Address>> GetAddressesByCampusNameAsync(string campusName);

        Task<Address> GetAddressByIdAsync(long addressId);

        Task<bool> UpdateAddressAsync(Address address);

        Task<Address> CreateAddressAsync(Address address);

        Task<bool> DeleteAddressByIdAsync(long addressId);

    }
}
