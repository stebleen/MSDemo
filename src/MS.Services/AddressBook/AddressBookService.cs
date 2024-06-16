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
    public class AddressBookService : BaseService, IAddressBookService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;

        public AddressBookService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AddressBook>> GetAddressBook(long userId)
        {
            var allItems = await _unitOfWork.GetRepository<AddressBook>().GetAllAsync(disableTracking: true);
            return allItems.Where(item => item.UserId == userId).ToList();
        }

        public async Task<AddressBook> GetDefaultAddressAsync(long userId)
        {
            return await _unitOfWork.GetRepository<AddressBook>().GetFirstOrDefaultAsync(
                predicate: ab => ab.UserId == userId && ab.IsDefault,
                disableTracking: true); // 默认关闭跟踪
        }

        public async Task<bool> SetDefaultAddressAsync(long addressBookId)
        {
            var repository = _unitOfWork.GetRepository<AddressBook>();

            // 查询和指定 addressBookId 相关的记录
            var addressToSetDefault = await repository.FindAsync(addressBookId);
          ;
            if (addressToSetDefault == null)
            {
                return false; // 地址不存在
            }

            // 清除该用户的所有默认地址
            var userAddresses = await repository.GetAllAsync(a => a.UserId == addressToSetDefault.UserId, disableTracking: false);
            foreach (var item in userAddresses)
            {
                //_unitOfWork.DbContext.Entry(item).State = EntityState.Modified;
                item.IsDefault = false;
                 repository.Update(item);
            }

            // _unitOfWork.DbContext.Entry(addressToSetDefault).State = EntityState.Modified;
            // addressToSetDefault.IsDefault = true;


            // 设置新的默认地址
            addressToSetDefault.IsDefault = true;
            repository.Update(addressToSetDefault);

            await _unitOfWork.SaveChangesAsync();
        
            return true;
        }

        public async Task<AddressBook> GetAddressBookByIdAsync(long id)
        {
            return await _unitOfWork.GetRepository<AddressBook>().FindAsync(id);
        }

        public async Task<bool> DeleteAddressBookByIdAsync(long id)
        {
            var repository = _unitOfWork.GetRepository<AddressBook>();
            var addressBook = await repository.FindAsync(id);
            if (addressBook != null)
            {
                repository.Delete(addressBook);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<bool> UpdateAddressBookAsync(AddressBook updateDto)
        {
            var addressBook = await _unitOfWork.GetRepository<AddressBook>().FindAsync(updateDto.Id);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(updateDto);
            System.Console.WriteLine(jsonString);
            System.Console.WriteLine("接下来打印的是 updateDto.Id:");
            System.Console.WriteLine(updateDto.Id);
            System.Console.WriteLine("接下来打印的是 updateDto.UserId:");
            System.Console.WriteLine(updateDto.UserId);
            if (addressBook == null)
            {
                return false;
            }
            

            // 更新属性
            addressBook.UserId = updateDto.UserId;
            System.Console.WriteLine("接下来打印的是 updateDto.UserId:");
            System.Console.WriteLine(updateDto.UserId);
            addressBook.UserId = 7;

            addressBook.Consignee = updateDto.Consignee;
            addressBook.Sex = updateDto.Sex;
            addressBook.Phone = updateDto.Phone;
            addressBook.Domitory = updateDto.Domitory;
            addressBook.IsDefault = updateDto.IsDefault;
            addressBook.AddressId = updateDto.AddressId;

            _unitOfWork.GetRepository<AddressBook>().Update(addressBook);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }



    }
}
