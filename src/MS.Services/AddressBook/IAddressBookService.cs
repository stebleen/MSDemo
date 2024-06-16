using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IAddressBookService : IBaseService
    {
        // 查询所有地址
        Task<IEnumerable<AddressBook>> GetAddressBook(long UserId);
        // 查询默认地址
        Task<AddressBook> GetDefaultAddressAsync(long userId);
        // 设置默认地址
        Task<bool> SetDefaultAddressAsync(long addressBookId);
        // 根据id查询地址
        Task<AddressBook> GetAddressBookByIdAsync(long id);
        // 根据id删除地址
        Task<bool> DeleteAddressBookByIdAsync(long id);
        // 根据id修改地址
        Task<bool> UpdateAddressBookAsync(AddressBook updateDto);
        // 新增地址
        Task<AddressBook> AddAddressBookAsync(AddressBook createDto);
    }
}
