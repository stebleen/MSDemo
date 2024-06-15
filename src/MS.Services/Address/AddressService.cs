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
    }
}
