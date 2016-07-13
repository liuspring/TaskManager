using System;
using TaskManager.HubService.SystemRuntime;

namespace TaskManager.HubService.Commands
{
    /// <summary>
    /// 卸载任务命令
    /// </summary>
    public class UninstallTaskCommand : BaseCommand
    {
        public override void Execute()
        {
            var tp = new TaskProvider();
            tp.Uninstall(this.CommandInfo.TaskId);
        }
    }
}
