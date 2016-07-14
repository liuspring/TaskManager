using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.HubService.Model
{
    public class TimeWatchLogInfo
    {
        public string fromip { get; set; }

        public int logtag { get; set; }

        public EnumTimeWatchLogType logtype { get; set; }

        public string msg { get; set; }

        public string remark { get; set; }

        public string sqlip { get; set; }

        public string url { get; set; }
    }
}
