using TaskManager.HubService.SystemRuntime;
namespace TaskManager.HubService.Commands
{
    /// <summary>
    /// 重启任务命令
    /// </summary>
    public class ReStartTaskCommand : BaseCommand
    {
        public override void Execute()
        {
            var tp = new TaskProvider();
            //先关闭
            tp.Stop(CommandInfo.Id);
            //后开启
            tp.Start(CommandInfo.Id);
        }
    }
}
