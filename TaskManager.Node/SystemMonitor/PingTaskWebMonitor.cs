﻿using System;
using TaskManager.Node.Tools;

namespace TaskManager.Node.SystemMonitor
{
    public class PingTaskWebMonitor : BaseMonitor
    {
        public override int Interval
        {
            get
            {
                return 1000 * 60;
            }
        }
        protected override void Run()
        {
            try
            {
                string url = GlobalConfig.TaskManagerWebUrl.TrimEnd('/') + "/OpenApi/" + "Ping/";
                //检测到任务平台站点异常
                //ClientResult r = ApiHelper.Get(url, new
                //{

                //});
                //if (r.success == false)
                //{
                //    LogHelper.AddNodeError("检测到任务平台站点异常", new Exception("节点任务平台Web保持心跳连接时出错"));
                //}
            }
            catch (Exception exp)
            {
                LogHelper.AddNodeError("检测到任务平台站点异常", exp);
            }

        }
    }
}
