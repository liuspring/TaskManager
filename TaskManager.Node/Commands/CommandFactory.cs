using System.Reflection;
using TaskManager.Commands;

namespace TaskManager.Node.Commands
{
    /// <summary>
    /// 命令执行工厂
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandInfo"></param>
        public static void Execute(Command commandInfo)
        {
            string namespacestr = typeof(BaseCommand).Namespace;
            var obj = Assembly.GetAssembly(typeof(BaseCommand)).CreateInstance(namespacestr + "." + commandInfo.CommandName + "Command", true);
            if (obj is BaseCommand)
            {
                var command = (obj as BaseCommand);
                command.CommandInfo = commandInfo;
                command.Execute();
            }
        }
    }
}
