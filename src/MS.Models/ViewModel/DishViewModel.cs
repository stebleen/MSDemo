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
    public class DishViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "主键是必需的")]
        [Display(Name = "主键")]
        public long Id { get; set; }

        [Required(ErrorMessage = "菜品名称是必需的")]
        [StringLength(32, ErrorMessage = "菜品名称的长度不能超过32个字符")]
        [Display(Name = "菜品名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "菜品分类Id是必需的")]
        [Display(Name = "菜品分类Id")]
        public long CategoryId { get; set; }

        [Display(Name = "菜品价格")]
        public decimal? Price { get; set; }

        [StringLength(255, ErrorMessage = "图片URL的长度不能超过255个字符")]
        [Display(Name = "图片")]
        public string Image { get; set; }

        [StringLength(255, ErrorMessage = "描述信息的长度不能超过255个字符")]
        [Display(Name = "描述信息")]
        public string Description { get; set; }

        [Display(Name = "状态")]
        public int? Status { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Dish>();
            //如果不是新增菜品，操作之前都要先检查菜品是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("菜品不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    //如果存在Id不同，菜品名称相同的实体，则返回报错
                    if (repo.Exists(a => a.Name == Name && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的菜品名称：{Name}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    //如果存在相同的菜品名称，则返回报错
                    if (repo.Exists(a => a.Name == Name))
                    {
                        return result.SetFailMessage($"已存在相同的菜品名称：{Name}");
                    }
                    break;
            }

            return result;//没有错误，默认返回成功
        }
    }
}
