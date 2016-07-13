using TaskManager.HubService.SystemRuntime;

namespace TaskManager.HubService.Commands
{
    /// <summary>
    /// 关闭任务命令
    /// </summary>
    public class StopTaskCommand : BaseCommand
    {
        public override void Execute()
        {
            var tp = new TaskProvider();
            tp.Stop(this.CommandInfo.TaskId);
        }
    }
}
