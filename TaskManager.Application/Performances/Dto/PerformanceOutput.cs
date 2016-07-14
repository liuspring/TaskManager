using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace TaskManager.Performances.Dto
{
    [AutoMapFrom(typeof(Performance))]
    public class PerformanceOutput
    {
        public int Id { get; set; }
        public int NodeId { get; set; }

        public int TaskId { get; set; }

        public float Cpu { get; set; }

        public float Memory { get; set; }

        public float InstallDirSize { get; set; }
    }
}
