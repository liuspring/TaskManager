using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.LoseYou
{
    [Table("ly_main_info")]
    [Description("分类表")]
    [Serializable]
    public class MainInfo : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("main_name")]
        [Description("主体")]
        public string MainName { get; set; }

        [StringLength(500)]
        [Column("content")]
        [Description("内容")]
        public string Content { get; set; }

        [Required]
        [Column("category_id")]
        [Description("分类ID")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("source_url")]
        [Description("来源地址")]
        public string SourceUrl { get; set; }
    }
}
