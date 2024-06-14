using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using MS.WebCore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace MS.Models.ViewModel
{
    public class AddressBookViewModel
    {
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [StringLength(50, ErrorMessage = "收货人名称不能超过50个字符")]
        public string Consignee { get; set; }

        [StringLength(2, ErrorMessage = "性别信息长度不能超过2个字符")]
        public string Sex { get; set; }

        [Required]
        [Phone]
        [StringLength(11, ErrorMessage = "手机号码长度不能超过11位")]
        public string Phone { get; set; }

        [StringLength(200, ErrorMessage = "宿舍号信息长度不能超过200个字符")]
        public string Dormitory { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [Required]
        public long AddressId { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repoAddress = unitOfWork.GetRepository<Address>();
            var repoUser = unitOfWork.GetRepository<User>();
            var repoAddressBook = unitOfWork.GetRepository<User>();

            // 首先检查用户ID与地址ID在相应的表中是否存在
            if (!repoUser.Exists(u => u.Id == this.UserId))
            {
                return result.SetFailMessage("用户ID不存在");
            }

            if (!repoAddress.Exists(a => a.Id == this.AddressId))
            {
                return result.SetFailMessage("地址ID不存在");
            }

            // 根据不同的执行类型，进行不同的操作
            switch (executeType)
            {
                case ExecuteType.Create:
                    // 对于创建操作，我们需要确保既用户ID也地址ID都存在
                    if (!repoUser.Exists(u => u.Id == this.UserId) || !repoAddress.Exists(a => a.Id == this.AddressId))
                    {
                        return result.SetFailMessage("用户或地址不存在");
                    }

                    break;

                case ExecuteType.Delete:
                case ExecuteType.Update:
                    // 对于删除和更新操作，我们需要确保员工地址关系存在
                    if (!repoAddressBook.Exists(ab => ab.Id == this.Id))
                    {
                        return result.SetFailMessage($"员工地址关系不存在：ID {Id}");
                    }
                    break;
                default:
                    break;
            }

            return result;//没有错误，默认返回成功
        }
    }
}

