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
    public class CategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "主键是必需的")]
        [Display(Name = "主键")]
        public long Id { get; set; }

        [Required(ErrorMessage = "类型是必需的")]
        [Range(1, 2, ErrorMessage = "类型必须是1（菜品分类）或2（套餐分类）")]
        [Display(Name = "类型")]
        public int Type { get; set; }

        [Required(ErrorMessage = "分类名称是必需的")]
        [StringLength(32, ErrorMessage = "分类名称的长度不能超过32个字符")]
        [Display(Name = "分类名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "顺序是必需的")]
        [Display(Name = "顺序")]
        public int Sort { get; set; }

        [Required(ErrorMessage = "分类状态是必需的")]
        [Display(Name = "分类状态")]
        public int Status { get; set; }

        public ExecuteResult CheckField(ExecuteType executeType, IUnitOfWork<MSDbContext> unitOfWork)
        {
            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Category>();
            //如果不是新增分类，操作之前都要先检查分类是否存在
            if (executeType != ExecuteType.Create && !repo.Exists(a => a.Id == Id))
            {
                return result.SetFailMessage("分类不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Delete:
                case ExecuteType.Update:
                    //如果存在Id不同，分类名称相同的实体，则返回报错
                    if (repo.Exists(a => a.Name == Name && a.Id != Id))
                    {
                        return result.SetFailMessage($"已存在相同的分类名称：{Name}");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    //如果存在相同的分类名称，则返回报错
                    if (repo.Exists(a => a.Name == Name))
                    {
                        return result.SetFailMessage($"已存在相同的分类名称：{Name}");
                    }
                    break;
            }

            return result;//没有错误，默认返回成功
        }
    }
}
