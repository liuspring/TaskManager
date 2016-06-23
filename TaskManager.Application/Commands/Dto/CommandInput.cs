using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace TaskManager.Commands.Dto
{
    [AutoMapFrom(typeof(Command))]
    public class CommandInput
    {
        public int Id { get; set; }
        [Required]
        public string CommandName { get; set; }

        [Required]
        public string CommandJson { get; set; }


        [Required]
        public byte CommandState { get; set; }

        [Required]

        public int NodeId { get; set; }

        [Required]

        public int TaskId { get; set; }

        public CommandInput()
        {
            CommandState = 0;
        }

    }
}
