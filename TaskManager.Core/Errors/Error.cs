using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Nodes;
using TaskManager.Tasks;

namespace TaskManager.Errors
{
    [Table("qrtz_error")]
    [Description("错误日志表")]
    public class Error : BaseEntity
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
        [Column("error_type")]
        [Description("错误类型")]
        public byte ErrorType { get; set; }

        public static Error Create(int nodeId, int taskId, string msg, byte errorType)
        {
            var error = new Error
            {
                NodeId = nodeId,
                TaskId = taskId,
                Msg = msg,
                ErrorType = errorType
            };
            return error;
        }

    }
}
