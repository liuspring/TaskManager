using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Logs
{
    [Table("qrtz_log")]
    [Description("一般日志表")]
    [Serializable]
    public class Log : BaseEntity
    {

        [Required]
        [Column("node_id")]
        [Description("节点ID")]
        public int NodeId { get; set; }

        [Column("task_id")]
        [Description("任务ID")]
        public int TaskId { get; set; }

        [Required]
        [StringLength(4000)]
        [Column("mgs")]
        [Description("错误信息")]
        public string Msg { get; set; }

        [Required]
        [Column("log_type")]
        [Description("日志类型")]
        public byte LogType { get; set; }

        public static Log Create(int nodeId, int taskId, string msg, byte logType)
        {
            var log = new Log
            {
                NodeId = nodeId,
                TaskId = taskId,
                Msg = msg,
                LogType = logType
            };
            return log;
        }

    }
}
