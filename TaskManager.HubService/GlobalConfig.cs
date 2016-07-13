using System.Collections.Generic;
using System.Configuration;
using Common;
using TaskManager.HubService.SystemMonitor;

namespace TaskManager.HubService
{
    public static class GlobalConfig
    {
        /// <summary>
        /// 任务数据库连接
        /// </summary>
        public static string TaskDataBaseConnectString
        {
            get
            {
                return ConfigurationManager.AppSettings["Default"];
            }
        }

        /// <summary>
        /// 当前节点标识
        /// </summary>
        public static int NodeId
        {
            get
            {
                var nodeId = ConfigurationManager.AppSettings["NodeId"];
                if (nodeId == null)
                    return 1;
                return nodeId.ToInt();
            }
        }

        /// <summary>
        /// 任务调度平台web url地址
        /// </summary>
        public static string TaskManagerWebUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["TaskManagerWebUrl"];
            }
        }
        /// <summary>
        /// 任务dll根目录
        /// </summary>
        public static string TaskDllDir = "任务dll根目录";
        /// <summary>
        /// 任务dll本地版本缓存
        /// </summary>
        public static string TaskDllCompressFileCacheDir = "任务dll版本缓存";
        /// <summary>
        /// 任务平台共享程序集
        /// </summary>
        public static string TaskSharedDllsDir = "任务dll共享程序集";
        /// <summary>
        /// 任务平台节点使用的监控插件
        /// </summary>
        public static List<BaseMonitor> Monitors = new List<BaseMonitor>();
    }
}
