using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.LoseYou
{
    [Table("ly_videos")]
    [Description("视频表")]
    [Serializable]
    public class Videos:BaseEntity
    {
        [Required]
        [Column("main_id")]
        [Description("主体Id")]
        public int MainId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("video_current_path")]
        [Description("视频当前路径")]
        public string VideoCurrentPath { get; set; }

        [Required]
        [StringLength(200)]
        [Column("video_original_url")]
        [Description("视频原网址")]
        public string VideoOriginalUrl { get; set; }

        [Required]
        [StringLength(200)]
        [Column("pic_current_path")]
        [Description("图片当前路径")]
        public string PicCurrentPath { get; set; }

        [Required]
        [StringLength(200)]
        [Column("pic_original_url")]
        [Description("图片原网址")]
        public string PicOriginalUrl { get; set; }
    }
}
