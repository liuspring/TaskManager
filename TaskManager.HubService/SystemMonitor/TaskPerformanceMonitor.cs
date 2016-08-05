using System;
using System.IO;
using Abp.Dependency;
using Common;
using TaskManager.HubService.Tools;
using TaskManager.Performances;
using TaskManager.Performances.Dto;

namespace TaskManager.HubService.SystemMonitor
{
    /// <summary>
    /// 任务性能监控者
    /// 用于检测当前任务运行的性能情况，通知到数据库
    /// </summary>
    public class TaskPerformanceMonitor : BaseMonitor
    {
        private readonly IPerformanceAppService _performanceAppService;

        public TaskPerformanceMonitor()
        {
            _performanceAppService = IocManager.Instance.Resolve<IPerformanceAppService>(); ;
        }

        public override int Interval
        {
            get
            {
                return 5000;
            }
        }
        protected override void Run()
        {
            foreach (var taskruntimeinfo in SystemRuntime.TaskPoolManager.CreateInstance().GetList())
            {
                try
                {
                    if (taskruntimeinfo == null)
                        continue;
                    string fileinstallpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDllDir + @"\" + taskruntimeinfo.TaskModel.Id;
                    double dirsizeM = -1;
                    if (Directory.Exists(fileinstallpath))
                    {
                        long dirsize = IoHelper.DirSize(new DirectoryInfo(fileinstallpath));
                        dirsizeM = (double)dirsize / 1024 / 1024;
                    }
                    try
                    {
                        if (taskruntimeinfo.Domain != null)
                        {
                            var input = new PerformanceInput
                            {
                                Cpu = (float)taskruntimeinfo.Domain.MonitoringTotalProcessorTime.TotalSeconds,
                                Memory = taskruntimeinfo.Domain.MonitoringTotalAllocatedMemorySize / 1024 / 1024,
                                InstallDirSize = (float)dirsizeM,
                                TaskId = taskruntimeinfo.TaskModel.Id,
                                NodeId = GlobalConfig.NodeId
                            };
                            var performance = _performanceAppService.GetPerformance(taskruntimeinfo.TaskModel.Id);
                            if (performance == null)
                            {
                                _performanceAppService.Create(input);
                            }
                            else
                            {
                                _performanceAppService.Update(input);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddTaskError("任务性能监控时出错", taskruntimeinfo.TaskModel.Id, ex);
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.AddNodeError("任务性能监控者出错", exp);
                }
            }
        }
    }
}
