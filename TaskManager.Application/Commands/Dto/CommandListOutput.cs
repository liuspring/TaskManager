using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace TaskManager.Commands.Dto
{
    [AutoMapFrom(typeof(Command))]
    public class CommandListOutput
    {
        public int Id { get; set; }

        public string CommandName { get; set; }

        public string CommandJson { get; set; }

        public byte CommandState { get; set; }

        public int NodeId { get; set; }

        public int TaskId { get; set; }
    }
}
