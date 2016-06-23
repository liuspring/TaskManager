using TaskManager.Node.SystemRuntime;

namespace TaskManager.Node.Commands
{
    /// <summary>
    /// 开启任务命令
    /// </summary>
    public class StartTaskCommand : BaseCommand
    {
        public override void Execute()
        {
            var tp = new TaskProvider();
            tp.Start(this.CommandInfo.TaskId);
        }
    }
}
