using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using TaskManager.Tasks;

namespace TaskManager.Node
{
    public class Test
    {
        private readonly ITaskAppService _taskAppService;
        public Test()
        {
            _taskAppService = IocManager.Instance.Resolve<ITaskAppService>();

            var a = _taskAppService.GetAllList().Count;
        }
    }
}
