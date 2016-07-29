using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.LoseYou
{
    [Table("ly_digests")]
    [Description("文摘表")]
    [Serializable]
    public class Digests:BaseEntity
    {
        [Required]
        [Column("main_id")]
        [Description("主体Id")]
        public int MainId { get; set; }

        [Required]
        [Column("content")]
        [Description("文摘内容")]
        public string Content { get; set; }
    }
}
