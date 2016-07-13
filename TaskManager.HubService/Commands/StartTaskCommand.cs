using TaskManager.HubService.SystemRuntime;

namespace TaskManager.HubService.Commands
{
    /// <summary>
    /// 开启任务命令
    /// </summary>
    public class StartTaskCommand : BaseCommand
    {
        public override void Execute()
        {
            var tp = new TaskProvider();
            tp.Start(CommandInfo.TaskId);
        }
    }
}
