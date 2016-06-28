using System;
using Common;
using Quartz;
using TaskManager.Node.Tools;

namespace TaskManager.Node.SystemRuntime
{
    /// <summary>
    /// 通用任务的回调job
    /// </summary>
    public class TaskJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                int taskid = context.JobDetail.Key.Name.ToInt();
                var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
                if (taskruntimeinfo == null || taskruntimeinfo.DllTask == null)
                {
                    LogHelper.AddTaskError("当前任务信息为空引用", taskid, new Exception());
                    return;
                }
                taskruntimeinfo.TaskLock.Invoke(() =>
                {
                    try
                    {
                        taskruntimeinfo.DllTask.TryRun();
                    }
                    catch (Exception exp)
                    {
                        LogHelper.AddTaskError("任务" + taskid + "TaskJob回调时执行失败", taskid, exp);
                    }
                });

            }
            catch (Exception exp)
            {
                LogHelper.AddNodeError("任务回调时严重系统级错误", exp);
            }
        }
    }
}
