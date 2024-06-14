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
    public class OrdersViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "订单主键")]
        public long Id { get; set; }

        [StringLength(50, ErrorMessage = "订单号的最大长度是50个字符")]
        [Display(Name = "订单号")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "订单状态")]
        public int Status { get; set; }

        [Required]
        [Display(Name = "下单用户")]
        public long UserId { get; set; }

        [Required]
        [Display(Name = "地址Id")]
        public long AddressBookId { get; set; }

        [Required]
        [Display(Name = "下单时间")]
        public DateTime OrderTime { get; set; }

        [Display(Name = "结账时间")]
        public DateTime? CheckoutTime { get; set; }

        [Required]
        [Display(Name = "支付方式")]
        public int PayMethod { get; set; }

        [Required]
        [Display(Name = "支付状态")]
        public int PayStatus { get; set; }

        [Required]
        [Display(Name = "实收金额")]
        public decimal Amount { get; set; }

        [StringLength(100, ErrorMessage = "备注的最大长度是100个字符")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        [StringLength(11, ErrorMessage = "手机号的最大长度是11个字符")]
        [Display(Name = "手机号")]
        public string Phone { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Orders>();
            //如果不是新增订单，操作之前都要先检查订单是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("订单不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    //如果存在Id不同，订单号相同的实体，则返回报错
                    if (repo.Exists(a => a.Number == Number && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的订单号：{Number}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    //如果存在相同的订单号，则返回报错
                    if (repo.Exists(a => a.Number == Number))
                    {
                        return result.SetFailMessage($"已存在相同的订单号：{Number}");
                    }
                    break;
            }

            return result;//没有错误，默认返回成功
        }
    }
}
