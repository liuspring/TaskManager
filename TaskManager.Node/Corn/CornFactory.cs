using System;
using Quartz.Impl.Triggers;
using TaskManager.Node.SystemRuntime;
using Quartz;
using TaskManager.Tasks;

namespace TaskManager.Node.Corn
{
    public class CornFactory
    {
        public static ITrigger CreateTigger(NodeTaskRuntimeInfo taskruntimeinfo)
        {
            Task task;
            string name;
            string group;
            if (taskruntimeinfo.TaskModel.Cron.Contains("["))
            {
                var customcorn = CustomCornFactory.GetCustomCorn(taskruntimeinfo.TaskModel.Cron);
                customcorn.Parse();
                if (customcorn is SimpleCorn)
                {
                    var simplecorn = customcorn as SimpleCorn;
                    SimpleCornInfo cornInfo = simplecorn.ConInfo;
                    // 定义调度触发规则，比如每1秒运行一次，共运行8次
                    task = taskruntimeinfo.TaskModel;
                    name = task.Id.ToString();
                    group = task.CategoryId.ToString();
                    var startTimeUtc = cornInfo.StartTime == null
                        ? DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2)
                        : DateBuilder.NextGivenSecondDate(cornInfo.StartTime.Value, 2);
                    var endTimeUtc = cornInfo.EndTime == null
                        ? DateBuilder.NextGivenSecondDate(DateTime.MaxValue, 3)
                        : DateBuilder.NextGivenSecondDate(cornInfo.EndTime.Value, 2);
                    var repeatCount = cornInfo.RepeatCount == null
                        ? int.MaxValue
                        : cornInfo.RepeatCount.Value - 1;

                    var repeatInterval = cornInfo.RepeatInterval == null
                        ? TimeSpan.FromSeconds(1)
                        : TimeSpan.FromSeconds(cornInfo.RepeatInterval.Value);
                    ISimpleTrigger simpleTrigger = new SimpleTriggerImpl(name, group, startTimeUtc, endTimeUtc, repeatCount, repeatInterval);
                    return simpleTrigger;
                }
                return null;
            }
            task = taskruntimeinfo.TaskModel;
            name = task.Id.ToString();
            group = task.CategoryId.ToString();
            var trigger =
                (ICronTrigger)TriggerBuilder.Create().WithIdentity(name, group).WithCronSchedule(task.Cron).Build();
            return trigger;
        }
    }
}
