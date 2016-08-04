using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Categories
{
    [Table("qrtz_category")]
    [Description("任务分类表")]
    [Serializable]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("category_name")]
        [Description("分类名称")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 任务命令集合
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual ICollection<Tasks.Task> Tasks { get; protected set; }

        protected Category()
        {
        }

        public static Category Create(string categoryName)
        {
            var category = new Category
            {
                CategoryName = categoryName
            };
            return category;
        }
    }
}
