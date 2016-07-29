using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.LoseYou
{
    [Table("ly_category")]
    [Description("分类表")]
    [Serializable]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("category_name")]
        [Description("分类名称")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("category_code")]
        [Description("分类编码")]
        public string CategoryCode { get; set; }

        protected Category()
        {
        }

        public static Category Create(string categoryName, string categoryCode, int creatorUserId)
        {
            var category = new Category
            {
                CategoryName = categoryName,
                CategoryCode = categoryCode,
                CreatorUserId = creatorUserId
            };
            return category;
        }
    }
}
