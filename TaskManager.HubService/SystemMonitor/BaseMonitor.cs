using System;
using System.Threading;
using TaskManager.HubService.Tools;

namespace TaskManager.HubService.SystemMonitor
{
    public abstract class BaseMonitor
    {
        protected Thread Thread;
        /// <summary>
        /// 监控间隔时间 （毫秒）
        /// </summary>
        public virtual int Interval { get; set; }

        protected BaseMonitor()
        {
            Thread = new Thread(TryRun)
            {
                IsBackground = true
            };
            Thread.Start();
        }

        private void TryRun()
        {
            while (true)
            {
                try
                {
                    Run();
                    Thread.Sleep(Interval);
                }
                catch (Exception exp)
                {
                    LogHelper.AddNodeError(this.GetType().Name + "监控严重错误", exp);
                }
            }
        }

        /// <summary>
        /// 监控执行方法约定
        /// </summary>
        protected virtual void Run()
        {

        }
    }
}
