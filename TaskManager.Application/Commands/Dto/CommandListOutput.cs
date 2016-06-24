using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Common;
using TaskManager.Enum;

namespace TaskManager.Commands.Dto
{
    [AutoMapFrom(typeof(Command))]
    public class CommandListOutput
    {
        public int Id { get; set; }

        public string CommandName { get; set; }

        public string SCommandName {
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

        public int NodeId { get; set; }

        public int TaskId { get; set; }
    }
}
