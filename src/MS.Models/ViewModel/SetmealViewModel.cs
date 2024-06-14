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
    public class SetmealViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "主键")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "菜品分类ID")]
        public long CategoryId { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "套餐名称的最大长度是32个字符")]
        [Display(Name = "套餐名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "套餐价格")]
        public decimal Price { get; set; }

        [Display(Name = "售卖状态")]
        public int? Status { get; set; }

        [StringLength(255, ErrorMessage = "描述信息的最大长度是255个字符")]
        [Display(Name = "描述信息")]
        public string Description { get; set; }

        [StringLength(255, ErrorMessage = "套餐图片的最大长度是255个字符")]
        [Display(Name = "套餐图片")]
        public string Image { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "更新时间")]
        public DateTime? UpdateTime { get; set; }

        [Display(Name = "创建人")]
        public long? CreateUser { get; set; }

        [Display(Name = "修改人")]
        public long? UpdateUser { get; set; }


        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Setmeal>();
            //如果不是新增套餐，操作之前都要先检查套餐是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("套餐不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    //如果存在Id不同，套餐名称相同的实体，则返回报错
                    if (repo.Exists(a => a.Name == Name && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的套餐名称：{Name}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    //如果存在相同的套餐名称，则返回报错
                    if (repo.Exists(a => a.Name == Name))
                    {
                        return result.SetFailMessage($"已存在相同的套餐名称：{Name}");
                    }
                    break;
            }

            return result;//没有错误，默认返回成功
        }
    }
}
