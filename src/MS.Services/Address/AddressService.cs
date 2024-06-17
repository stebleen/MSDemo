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
    public class AddressService : BaseService, IAddressService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;
        public AddressService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            var allItems = await _unitOfWork.GetRepository<Address>().GetAllAsync();
            return allItems.ToList();
        }



        public async Task<IEnumerable<Address>> GetAddressesByCampusNameAsync(string campusName)
        {
            var query = _unitOfWork.GetRepository<Address>().GetAll();

            if (!string.IsNullOrEmpty(campusName))
            {
                query = query.Where(a => a.CampusName.Contains(campusName));
            }

            return await query.ToListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(long addressId)
        {
            return await _unitOfWork.GetRepository<Address>().GetFirstOrDefaultAsync(predicate:a => a.Id == addressId);
        }


        public async Task<bool> UpdateAddressAsync(Address address)
        {
            var existingAddress = await _unitOfWork.GetRepository<Address>().GetFirstOrDefaultAsync(predicate:a => a.Id == address.Id);
            if (existingAddress != null)
            {
                existingAddress.CampusCode = address.CampusCode;
                existingAddress.CampusName = address.CampusName;
                existingAddress.BuildingCode = address.BuildingCode;
                existingAddress.BuildingName = address.BuildingName;


                _unitOfWork.GetRepository<Address>().Update(existingAddress);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<Address> CreateAddressAsync(Address address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));

            // Id 自增
            _unitOfWork.GetRepository<Address>().Insert(address);
            await _unitOfWork.SaveChangesAsync();

           
            return address;
        }

        public async Task<bool> DeleteAddressByIdAsync(long addressId)
        {
            var repo = _unitOfWork.GetRepository<Address>();
            var address = await repo.GetFirstOrDefaultAsync(predicate:a => a.Id == addressId);

            if (address != null)
            {
                repo.Delete(address);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }


    }
}
