using Abp.AutoMapper;
using Common;
using TaskManager.Enum;
using TaskManager.Nodes.Dto;
using TaskManager.Tasks.Dto;

namespace TaskManager.Commands.Dto
{
    [AutoMapFrom(typeof(Command))]
    public class CommandListOutput
    {
        public int Id { get; set; }

        public string CommandName { get; set; }

        public TaskOutput Task { get; set; }

        public string TaskName
        {
            get { return Task.TaskName; }
        }

        public string SCommandName
        {
            get
            {
                return
                   EnumExt.GetEnumDescription(
                       (EnumTaskCommandName)System.Enum.ToObject(typeof(EnumTaskCommandName), CommandName.ToInt()));
            }
        }

        public string CommandJson { get; set; }

        public byte CommandState { get; set; }

        public string SCommandState
        {
            get
            {
                return
                   EnumExt.GetEnumDescription(
                       (EnumTaskCommandState)System.Enum.ToObject(typeof(EnumTaskCommandState), CommandState.ToInt()));
            }
        }

        public NodeOutput Node { get; set; }

        public string NodeName
        {
            get { return Node.NodeName; }
        }
    }
}
