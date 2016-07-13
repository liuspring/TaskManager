using System;
using System.ServiceProcess;
using Common;
using TaskManager.HubService.Tools;

namespace TaskManager.HubService
{
    partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO:  在此处添加代码以启动服务。
            try
            {
                var path = GlobalConfig.TaskSharedDllsDir + @"\";
                IoHelper.CreateDirectory(path);
                CommandQueueProcessor.Run();
                //注册后台监控
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskRecoverMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskPerformanceMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.NodeHeartBeatMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskStopMonitor());
                LogHelper.AddNodeLog("节点windows服务启动成功");
            }
            catch (Exception ex)
            {
                LogHelper.AddNodeError("节点windows服务启动失败", ex);
            }
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
            LogHelper.AddNodeLog("节点windows服务停止");
        }
    }
}
