using AutoMapper;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IUnitOfWork<MSDbContext> _unitOfWork;

        public EmployeeService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            // 示例：假设admin/123456是有效的登录凭证
            if (loginRequest.Username == "admin" && loginRequest.Password == "123456")
            {
                return new LoginResponseDto
                {
                    Id = 2, 
                    Name = "admin",
                    Username = "admin",
                    Token = "123" // 固定的示例token
                };
            }

            return null; // 登录失败
        }
    }
}
