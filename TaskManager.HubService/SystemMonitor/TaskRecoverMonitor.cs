using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Dependency;
using TaskManager.HubService.SystemRuntime;
using TaskManager.HubService.Tools;
using TaskManager.Nodes;
using TaskManager.Tasks;

namespace TaskManager.HubService.SystemMonitor
{
    /// <summary>
    /// 任务的回收监控者
    /// 用于任务异常卸载的资源回收
    /// </summary>
    public class TaskRecoverMonitor : BaseMonitor
    {
        private readonly ITaskAppService _taskAppService;

        public TaskRecoverMonitor()
        {
            _taskAppService = IocManager.Instance.Resolve<ITaskAppService>(); ;
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
            var tasks = _taskAppService.GetTasks(GlobalConfig.NodeId, (int)EnumTaskState.Stop);
            var currentscantaskids = new List<int>();
            foreach (var task in tasks)
            {
                try
                {
                    var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(task.Id.ToString());
                    if (taskruntimeinfo != null)
                    {
                        currentscantaskids.Add(task.Id);
                    }

                    var recovertaskids = (from o in _lastscantaskids
                                          from c in currentscantaskids
                                          where o == c
                                          select o).ToList();
                    if (recovertaskids.Count > 0)
                        recovertaskids.ForEach((c) => LogHelper.AddTaskError("任务资源运行异常,可能需要手动卸载", task.Id,
                            new Exception("任务处于停止状态，但是相应集群节点中，发现任务存在在运行池中未释放")));
                    _lastscantaskids = currentscantaskids;
                }
                catch (Exception exp)
                {
                    LogHelper.AddNodeError("任务" + task.Id + "资源回收出错", exp);
                }
            }
        }
    }
}
