using System;
using Abp.AutoMapper;
using Common;
using TaskManager.Enum;

namespace TaskManager.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class TaskListOutput
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public Categories.Dto.CategoryOutput Category { get; set; }

        public string CategoryName
        {
            get { return Category.CategoryName; }
        }

        public int CategoryId { get; set; }

        public int CmdState { get; set; }

        public Nodes.Dto.NodeOutput Node { get; set; }

        public string NodeName
        {
            get { return Node.NodeName; }
        }

        public int NodeId { get; set; }

        public DateTime LastStartTime { get; set; }

        public string SLastStartTime
        {
            get { return LastStartTime.ToStrDateTime(); }
        }

        public DateTime LastEndTime { get; set; }

        public string SLastEndTime
        {
            get { return LastEndTime.ToStrDateTime(); }
        }

        public DateTime LastErrorTime { get; set; }

        public string SLastErrorTime
        {
            get { return LastErrorTime.ToStrDateTime(); }
        }

        public int ErrorCount { get; set; }

        public int RunCount { get; set; }

        public byte State { get; set; }

        public string SState
        {
            get
            {
                return
                    EnumExt.GetEnumDescription(
                        (EnumTaskState)System.Enum.ToObject(typeof(EnumTaskState), State.ToInt()));
            }
        }

        public int Version { get; set; }

        public string Cron { get; set; }

        public string Remark { get; set; }

        public DateTime CreationTime { get; set; }
        public string SCreationTime
        {
            get { return CreationTime.ToStrDateTime(); }
        }
        public DateTime? LastModificationTime { get; set; }
        public string SLastModificationTime
        {
            get { return LastModificationTime.ToStrDateTime(); }
        }
        public long? CreatorUserId { get; set; }
    }
}
