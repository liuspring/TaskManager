using TaskManager.Commands;

namespace TaskManager.HubService.Commands
{
    public abstract class BaseCommand
    {
        /// <summary>
        /// 任务信息model
        /// </summary>
        public Command CommandInfo { get; set; }

        /// <summary>
        /// 命令执行方法约定
        /// </summary>
        public virtual void Execute()
        {

        }
    }
}
