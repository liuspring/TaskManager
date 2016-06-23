using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Node.Model
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public int CategoryId { get; set; }

        public int CmdState { get; set; }

        public int NodeId { get; set; }

        public DateTime LastStartTime { get; set; }

        public DateTime LastEndTime { get; set; }

        public DateTime LastErrorTime { get; set; }

        public int ErrorCount { get; set; }

        public int RunCount { get; set; }

        public byte State { get; set; }

        public int Version { get; set; }

        public string AppConfigJson { get; set; }

        public string Cron { get; set; }

        public string MainClassDllFileName { get; set; }

        public string MainClassNameSpace { get; set; }
        public string Remark { get; set; }
        public bool IsDeleted { get; set; }

        public long? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }
    }
}
