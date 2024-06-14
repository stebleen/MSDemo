using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using MS.DbContexts;
using MS.Entities;
using MS.UnitOfWork;
using MS.WebCore.Core;

namespace MS.Models.ViewModel
{
    public class AddressViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "id字段是必需的")]
        [Display(Name = "地址id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "校区编号是必需的")]
        [StringLength(6, ErrorMessage = "校区编号的长度不能超过6个字符")]
        [Display(Name = "校区编号")]
        public string CampusCode { get; set; }

        [Required(ErrorMessage = "校区名称是必需的")]
        [StringLength(32, ErrorMessage = "校区名称的长度不能超过32个字符")]
        [Display(Name = "校区名称")]
        public string CampusName { get; set; }

        [Required(ErrorMessage = "楼宇编码是必需的")]
        [StringLength(6, ErrorMessage = "楼宇编码的长度不能超过6个字符")]
        [Display(Name = "楼宇编号")]
        public string BuildingCode { get; set; }

        [Required(ErrorMessage = "楼宇名称是必需的")]
        [StringLength(32, ErrorMessage = "楼宇名称的长度不能超过32个字符")]
        [Display(Name = "楼宇名称")]
        public string BuildingName { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Address>();
            // 如果不是新增地址，操作之前都要先检查地址是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("地址不存在");
            }

            // 针对不同的操作，检查逻辑可能不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    //如果存在Id不同，校区编码和楼宇编码相同的实体，则返回报错
                    if (repo.Exists(a => a.CampusCode == CampusCode && a.BuildingCode == BuildingCode && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的校区和楼宇编码：{CampusCode}-{BuildingCode}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    // 如果存在相同的校区编码和楼宇编码，则返回报错
                    if (repo.Exists(a => a.CampusCode == CampusCode && a.BuildingCode == BuildingCode))
                    {
                        return result.SetFailMessage($"已存在相同的校区和楼宇编码：{CampusCode}-{BuildingCode}");
                    }
                    break;
            }

            return result; // 没有错误，默认返回成功
        }

    }
}
