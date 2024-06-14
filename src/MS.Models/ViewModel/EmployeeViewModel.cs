using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MS.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "姓名不能超过32个字符")]
        public string Name { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "用户名不能超过32个字符")]
        public string UserName { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "密码不能超过64个字符")]
        public string Password { get; set; }

        [Required]
        [Phone]
        [StringLength(11, ErrorMessage = "手机号码长度不能超过11位")]
        public string Phone { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "性别信息长度不能超过2个字符")]
        public string Sex { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "身份证号长度应为18位")]
        public string IdNumber { get; set; }

        [Required]
        public int Status { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long? CreateUser { get; set; }
        public long? UpdateUser { get; set; }
        public long? AddressId { get; set; }


        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Employee>();
            //如果不是新增员工，操作之前都要先检查员工是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("员工不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    //如果存在Id不同，用户名相同的实体，则返回报错
                    if (repo.Exists(a => a.Username == UserName && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的用户名：{UserName}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    //如果存在相同的角色名，则返回报错
                    if (repo.Exists(a => a.Username == UserName))
                    {
                        return result.SetFailMessage($"已存在相同的角色名称：{UserName}");
                    }
                    break;
            }


            return result;//没有错误，默认返回成功
        }
    }
}