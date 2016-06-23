using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Node.Model
{
    public class LogModel
    {
        public int Id { get; set; }

        public int NodeId { get; set; }

        public int TaskId { get; set; }

        public string Msg { get; set; }

        public byte LogType { get; set; }

        public bool IsDeleted { get; set; }

        public long? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }
    }
}
