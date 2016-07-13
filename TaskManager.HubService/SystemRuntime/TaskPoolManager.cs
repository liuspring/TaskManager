using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using Quartz.Impl;
using TaskManager.HubService.Corn;

namespace TaskManager.HubService.SystemRuntime
{
    /// <summary>
    /// 全局任务池管理
    /// 管理任务的移入，移除任务运行池
    /// 全局仅一个实例
    /// </summary>
    public class TaskPoolManager : IDisposable
    {
        /// <summary>
        /// 任务运行池
        /// </summary>
        private static readonly Dictionary<string, NodeTaskRuntimeInfo> TaskRuntimePool = new Dictionary<string, NodeTaskRuntimeInfo>();
        /// <summary>
        /// 任务池管理者,全局仅一个实例
        /// </summary>
        private static readonly TaskPoolManager Taskpoolmanager;
        /// <summary>
        /// 任务池管理操作锁标记
        /// </summary>
        private static readonly object Locktag = new object();
        /// <summary>
        /// 任务池执行计划
        /// </summary>
        private static readonly IScheduler Sched;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static TaskPoolManager()
        {
            Taskpoolmanager = new TaskPoolManager();
            ISchedulerFactory sf = new StdSchedulerFactory();
            Sched = sf.GetScheduler();
            Sched.Start();
        }
        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            if (Sched != null && !Sched.IsShutdown)
                Sched.Shutdown();
        }
        /// <summary>
        /// 获取任务池的全局唯一实例
        /// </summary>
        /// <returns></returns>
        public static TaskPoolManager CreateInstance()
        {
            return Taskpoolmanager;
        }
        /// <summary>
        /// 将任务移入任务池
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <returns></returns>
        public bool Add(string taskid, NodeTaskRuntimeInfo taskruntimeinfo)
        {
            lock (Locktag)
            {
                if (!TaskRuntimePool.ContainsKey(taskid))
                {
                    var task = taskruntimeinfo.TaskModel;
                    var name = task.Id.ToString();
                    var group = task.CategoryId.ToString();
                    IJobDetail jobDetail = new JobDetailImpl(name, group, typeof(TaskJob));// 任务名，任务组，任务执行类 
                    var trigger = CornFactory.CreateTigger(taskruntimeinfo);
                    Sched.ScheduleJob(jobDetail, trigger);
                    TaskRuntimePool.Add(taskid, taskruntimeinfo);
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 将任务移出任务池
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Remove(string taskid)
        {
            lock (Locktag)
            {
                if (TaskRuntimePool.ContainsKey(taskid))
                {
                    var taskruntimeinfo = TaskRuntimePool[taskid];
                    var task = taskruntimeinfo.TaskModel;
                    var name = task.Id.ToString();
                    var group = task.CategoryId.ToString();
                    var triggerKey = new TriggerKey(name, group);
                    Sched.PauseTrigger(triggerKey);// 停止触发器  
                    Sched.UnscheduleJob(triggerKey);// 移除触发器
                    var jobKey = new JobKey(name, group);
                    Sched.DeleteJob(jobKey);// 删除任务

                    TaskRuntimePool.Remove(taskid);
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 获取任务池中任务运行时信息
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public NodeTaskRuntimeInfo Get(string taskid)
        {
            if (!TaskRuntimePool.ContainsKey(taskid))
            {
                return null;
            }
            lock (Locktag)
            {
                if (TaskRuntimePool.ContainsKey(taskid))
                {
                    return TaskRuntimePool[taskid];
                }
                return null;
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public List<NodeTaskRuntimeInfo> GetList()
        {
            return TaskRuntimePool.Values.ToList();
        }
    }
}
