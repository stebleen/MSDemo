
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
    public class ShoppingCartViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "主键")]
        public long Id { get; set; }

        [StringLength(32, ErrorMessage = "商品名称的最大长度是32个字符")]
        [Display(Name = "商品名称")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "图片的最大长度是255个字符")]
        [Display(Name = "图片")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "用户ID")]
        public long UserId { get; set; }

        [Display(Name = "菜品ID")]
        public long? DishId { get; set; }

        [Display(Name = "套餐ID")]
        public long? SetmealId { get; set; }

        [StringLength(50, ErrorMessage = "口味的最大长度是50个字符")]
        [Display(Name = "口味")]
        public string DishFlavor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于或等于1")]
        [Display(Name = "数量")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "金额")]
        public decimal Amount { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<ShoppingCart>();

            /*
            // 如果不是新增购物车项目，操作之前都要先检查购物车项目是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("购物车项目不存在");
            }
            */

            // 针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    // 如果存在Id不同，商品名称相同的实体，则返回报错
                    if (repo.Exists(a => a.Name == Name && a.Id != Id))
                    {
                        return result.SetFailMessage($"购物车中已存在相同名称的商品：{Name}");
                    }
                    break;

                case ExecuteType.Create:
                default:
                    // 如果存在相同的商品名称，则返回报错
                    if (repo.Exists(a => a.Name == Name))
                    {
                        return result.SetFailMessage($"购物车中已存在相同名称的商品：{Name}");
                    }
                    break;
            }

            return result; // 没有错误，默认返回成功
        }
    }
}
