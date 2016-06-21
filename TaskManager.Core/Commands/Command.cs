using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Nodes;
using TaskManager.Tasks;

namespace TaskManager.Commands
{
    [Table("qrtz_command")]
    [Description("任务命令表")]

    public class Command : BaseEntity
    {

        [Required]
        [StringLength(400)]
        [Column("command_Json")]
        [Description("命令JSON")]
        public string CommandJson { get; set; }

        [Required]
        [StringLength(20)]
        [Column("command_Name")]
        [Description("命令名称")]
        public string CommandName { get; set; }

        [Required]
        [Column("command_State")]
        [Description("命令状态")]
        public byte CommandState { get; set; }

        [ForeignKey("NodeId")]
        public Node Node { get; set; }

        [Required]
        [Column("node_id")]
        [Description("节点ID")]
        public int NodeId { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

        [Column("task_id")]
        [Description("任务ID")]
        public int TaskId { get; set; }

        public static Command Create(string commandName, string commandJson, byte commandState, int nodeId, int taskId)
        {
            var command = new Command
            {
                CommandName = commandName,
                CommandJson = commandJson,
                CommandState = commandState,
                NodeId = nodeId,
                TaskId = taskId
            };
            return command;
        }

    }
}
