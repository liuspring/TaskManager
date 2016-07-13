using System;
using TaskManager.BaseService.Model;

namespace TaskManager.BaseService.SystemRuntime
{
    /// <summary>
    /// 任务系统运行时信息
    /// </summary>
    [Serializable]
    public class TaskSystemRuntimeInfo
    {
        /// <summary>
        /// 任务数据库连接字符串
        /// </summary>
        public string TaskConnectString { get; set; }
        /// <summary>
        /// 任务信息
        /// </summary>
        public TaskModel TaskModel { get; set; }
    }
}
