using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class EmployeePageResponseDto
    {
        public int Total { get; set; }
        public IEnumerable<EmployeeDto> Records { get; set; }
    }

    public class EmployeeDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string IdNumber { get; set; }
        public int Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long? CreateUser { get; set; }
        public long? UpdateUser { get; set; }
        public string CampusName { get; set; }
        public string BuildingName { get; set; }
    }
}
