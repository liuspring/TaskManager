using System;
using Abp.AutoMapper;
using Common;
using TaskManager.Enum;
using TaskManager.Nodes.Dto;
using TaskManager.Tasks.Dto;

namespace TaskManager.Logs.Dto
{
    [AutoMapFrom(typeof(Log))]
    public class LogListOutput
    {
        public int Id { get; set; }
        public TaskOutput Task { get; set; }
        public string TaskName
        {
            get { return Task.TaskName; }
        }
        public NodeOutput Node { get; set; }

        public string NodeName
        {
            get { return Node.NodeName; }
        }
        public string Msg { get; set; }
        public string SMsg
        {
            get { return Msg.InterceptionStr(15); }
        }
        public byte LogType { get; set; }

        public string SLogType
        {
            get
            {
                return
                EnumExt.GetEnumDescription(
                    (EnumTaskLogType)System.Enum.ToObject(typeof(EnumTaskLogType), LogType.ToInt()));
            }
        }

        public DateTime CreationTime { get; set; }

        public string SCreationTime
        {
            get { return CreationTime.ToStrDateTime(); }
        }

        public long? CreatorUserId { get; set; }


    }
}
