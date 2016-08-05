using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using TaskManager.HubService.SystemRuntime;
using TaskManager.HubService.Tools;
using TaskManager.Tasks;

namespace TaskManager.HubService.SystemMonitor
{
    /// <summary>
    /// 任务的停止监控者
    /// 用于任务异常停止的检测
    /// </summary>
    public class TaskStopMonitor : BaseMonitor
    {
        private ITaskAppService _taskAppService;

        public TaskStopMonitor()
        {
            _taskAppService = IocManager.Instance.Resolve<ITaskAppService>(); 
        }
        public override int Interval
        {
            get
            {
                return 1000 * 60;//1分钟扫描
            }
        }

        private List<int> _lastscantaskids = new List<int>();

        protected override void Run()
        {
            if(_taskAppService==null)
                _taskAppService = IocManager.Instance.Resolve<ITaskAppService>();
            var tasks = _taskAppService.GetTasks(GlobalConfig.NodeId, (int)EnumTaskState.Running);
            var currentscantaskids = new List<int>();
            foreach (var task in tasks)
            {
                try
                {
                    var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(task.Id.ToString());
                    if (taskruntimeinfo == null)
                    {
                        currentscantaskids.Add(task.Id);
                    }

                    var recovertaskids = (from o in _lastscantaskids
                                          from c in currentscantaskids
                                          where o == c
                                          select o).ToList();
                    if (recovertaskids.Count > 0)
                        recovertaskids.ForEach((c) => LogHelper.AddTaskError("任务资源运行可能异常停止了", task.Id, new Exception("任务处于运行状态，但是相应集群节点中，未发现任务在运行")));
                    _lastscantaskids = currentscantaskids;
                }
                catch (Exception exp)
                {
                    LogHelper.AddNodeError("任务" + task.Id + "检测是否异常终止出错", exp);
                }
            }
        }
    }
}
