using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.Entities.admin;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<EmployeePageResponseDto> GetEmployeePageAsync(EmployeePageRequestDto requestDto)
        {
            var query = _unitOfWork.GetRepository<Employee>().GetAll();

            if (!string.IsNullOrEmpty(requestDto.Name))
            {
                query = query.Where(e => e.Name.Contains(requestDto.Name));
            }

            int total = await query.CountAsync();

            var employeeList = await query
                .OrderBy(e => e.Id)
                .Skip((requestDto.Page - 1) * requestDto.PageSize)
                .Take(requestDto.PageSize)
                .ToListAsync();

            // 分离的 Address 信息查询
            var addressIds = employeeList.Select(e => e.AddressId).Distinct().ToList();
            var addresses = await _unitOfWork.GetRepository<Address>().GetAll()
                .Where(a => addressIds.Contains(a.Id))
                .ToListAsync();

            var records = employeeList.Select(e => new EmployeeDto
            {
                // 转换 Employee 信息
                Id = e.Id,
                Username = e.Username,
                Name = e.Name,
                Password = e.Password, // 注意: 实际应用中不应返回密码
                Phone = e.Phone,
                Sex = e.Sex,
                IdNumber = e.IdNumber,
                Status = e.Status,
                CreateTime = e.CreateTime,
                UpdateTime = e.UpdateTime,
                CreateUser = e.CreateUser,
                UpdateUser = e.UpdateUser,

                // 查询并赋值 CampusName 和 BuildingName
                CampusName = addresses.FirstOrDefault(a => a.Id == e.AddressId)?.CampusName,
                BuildingName = addresses.FirstOrDefault(a => a.Id == e.AddressId)?.BuildingName,
            }).ToList();

            return new EmployeePageResponseDto
            {
                Total = total,
                Records = records
            };
        }


    }
}
