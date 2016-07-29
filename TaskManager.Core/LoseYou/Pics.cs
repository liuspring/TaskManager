using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.LoseYou
{
    [Table("ly_pics")]
    [Description("图片表")]
    [Serializable]
    public class Pics : BaseEntity
    {
        [Required]
        [Column("main_id")]
        [Description("主体Id")]
        public int MainId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("title")]
        [Description("标题")]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [Column("current_path")]
        [Description("当前路径")]
        public string CurrentPath { get; set; }

        [Required]
        [StringLength(200)]
        [Column("original_url")]
        [Description("原网址")]
        public string OriginalUrl { get; set; }
    }
}
