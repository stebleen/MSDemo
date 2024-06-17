using MS.Entities;
using MS.Entities.admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IEmployeeService : IBaseService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);

        Task<EmployeePageResponseDto> GetEmployeePageAsync(EmployeePageRequestDto requestDto);
    }
}
