using Microsoft.AspNetCore.Mvc;
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
    public class UserViewModel
    {
        public long Id { get; set; }

        [StringLength(45, ErrorMessage = "微信用户唯一标识不能超过45个字符")]
        public string OpenId { get; set; }

        [StringLength(32, ErrorMessage = "姓名不能超过32个字符")]
        public string Name { get; set; }

        [Phone]
        [StringLength(11, ErrorMessage = "手机号码长度不能超过11位")]
        public string Phone { get; set; }

        [StringLength(2, ErrorMessage = "性别信息长度不能超过2个字符")]
        public string Sex { get; set; }

        [StringLength(18, ErrorMessage = "身份证号长度应为18位")]
        public string IdNumber { get; set; }

        [StringLength(500, ErrorMessage = "头像URL长度不能超过500个字符")]
        public string Avatar { get; set; }

        public DateTime? CreateTime { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<User>();

            return result;//没有错误，默认返回成功
        }
    }
}
