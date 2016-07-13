using System;
using System.Collections.Generic;
using System.Threading;
using Abp.Dependency;
using Common;
using TaskManager.Commands;
using TaskManager.HubService.Commands;
using TaskManager.Tasks;
using TaskManager.HubService.Tools;

namespace TaskManager.HubService
{
    public class CommandQueueProcessor
    {
        private static readonly ITaskAppService TaskAppService;
        private static readonly ICommandAppService CommandAppService;

        /// <summary>
        /// 上一次日志扫描的最大id
        /// </summary>
        public static int LastMaxId = -1;

        static CommandQueueProcessor()
        {
            TaskAppService = IocManager.Instance.Resolve<ITaskAppService>();
            CommandAppService = IocManager.Instance.Resolve<ICommandAppService>();
            var thread = new Thread(Running)
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 运行处理循环
        /// </summary>
        public static void Run()
        {

        }

        static void Running()
        {
            //lastMaxID = 0;//仅测试
            RecoveryStartTasks();
            RuningCommandLoop();
        }

        /// <summary>
        /// 恢复已开启的任务
        /// </summary>
        private static void RecoveryStartTasks()
        {
            try
            {
                LogHelper.AddNodeLog("当前节点启动成功,准备恢复已经开启的任务...");
                var tasks = TaskAppService.GetTasks(GlobalConfig.NodeId, (int)EnumTaskState.Running);
                foreach (var task in tasks)
                {
                    try
                    {
                        var command = new Command
                        {
                            CommandJson = string.Empty,
                            CommandName = EnumTaskCommandName.StartTask.ToString(),
                            CommandState = (int)EnumTaskCommandState.None,
                            NodeId = GlobalConfig.NodeId,
                            TaskId = task.Id,
                            Id = -1
                        };
                        CommandFactory.Execute(command);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddTaskError(string.Format("恢复已经开启的任务{0}失败", task.Id), task.Id, ex);
                    }
                }
                LogHelper.AddNodeLog(string.Format("恢复已经开启的任务完毕，共{0}条任务重启", tasks.Count));
            }
            catch (Exception ex)
            {
                LogHelper.AddNodeError("恢复已经开启的任务失败", ex);
            }

        }

        /// <summary>
        /// 运行消息循环
        /// </summary>
        static void RuningCommandLoop()
        {
            LogHelper.AddNodeLog("准备接受命令并运行消息循环...");
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    var commands = new List<Command>();
                    try
                    {
                        if (LastMaxId < 0)
                            LastMaxId = CommandAppService.GetMaxCommandId();
                        commands = CommandAppService.GetCommands(GlobalConfig.NodeId, LastMaxId);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddNodeError("获取当前节点命令集错误", ex);
                    }
                    if (commands.Count > 0)
                        LogHelper.AddNodeLog("当前节点扫描到" + commands.Count + "条命令,并执行中....");
                    foreach (var command in commands)
                    {
                        try
                        {
                            if (command == null)
                                continue;
                            command.CommandName = System.Enum.GetName(typeof(EnumTaskCommandName), command.CommandName.ToInt());
                            CommandFactory.Execute(command);
                            CommandAppService.UpdateStateById(command.Id, (byte)EnumTaskCommandState.Success);
                            LogHelper.AddNodeLog(string.Format("当前节点执行命令成功! id:{0},命令名:{1},命令内容:{2}", command.Id, command.CommandName, command.CommandJson));
                        }
                        catch (Exception ex)
                        {
                            CommandAppService.UpdateStateById(command.Id, (byte)EnumTaskCommandState.Error);
                            LogHelper.AddTaskError("执行节点命令失败", command.TaskId, ex);
                        }
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.AddNodeError("系统级不可恢复严重错误", ex);
                }
                Thread.Sleep(3000);
            }
        }

    }
}
