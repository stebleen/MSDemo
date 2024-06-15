using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IAddressBookService : IBaseService
    {
        Task<IEnumerable<AddressBook>> GetAddressBook(long UserId);
        Task<AddressBook> GetDefaultAddressAsync(long userId);
        Task<bool> SetDefaultAddressAsync(long addressBookId);
    }
}
