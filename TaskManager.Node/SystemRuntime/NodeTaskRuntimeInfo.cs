using System;
using TaskManager.Node.TaskManager;
using TaskManager.Node.TaskManager.SystemRuntime;
using TaskManager.Versions;
using Task = TaskManager.Tasks.Task;

namespace TaskManager.Node.SystemRuntime
{
    /// <summary>
    /// 任务运行时信息
    /// </summary>
    public class NodeTaskRuntimeInfo
    {
        /// <summary>
        /// 任务所在的应用程序域
        /// </summary>
        public AppDomain Domain;
        /// <summary>
        /// 任务信息
        /// </summary>
        public Task TaskModel;
        /// <summary>
        /// 任务当前版本信息
        /// </summary>
        public VersionInfo TaskVersionModel;
        /// <summary>
        /// 应用程序域中任务dll实例引用
        /// </summary>
        public BaseDllTask DllTask;
        /// <summary>
        /// 任务锁机制,用于执行状态的锁定，保证任务单次运行
        /// </summary>
        public TaskLock TaskLock;
    }
}
