using System;
using System.IO;
using System.Windows.Forms;

namespace TaskManager.Node
{
    static class Program
    {
        private static bool IsTest = true;//是否测试
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var t = new Test();
            t.Get();


            if (args != null && args.Length > 0)
            {
                if (args.Length == 1)
                {
                    GlobalConfig.NodeId = Convert.ToInt32(args[0]);
                }
                else if (args.Length == 2)
                {
                    GlobalConfig.NodeId = Convert.ToInt32(args[0]);
                    GlobalConfig.TaskDataBaseConnectString = Convert.ToString(args[1]).Replace("**", " ");
                }
            }
            if (IsTest)
            {
                GlobalConfig.TaskDataBaseConnectString = "server=192.168.17.201;Initial Catalog=dyd_bs_task;User ID=sa;Password=Xx~!@#;";
                GlobalConfig.NodeId = 361;
                var path = GlobalConfig.TaskSharedDllsDir + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                CommandQueueProcessor.LastMaxId = 1;
                CommandQueueProcessor.Run();

                //注册后台监控
                //GlobalConfig.Monitors.Add(new SystemMonitor.TaskRecoverMonitor());
                //GlobalConfig.Monitors.Add(new SystemMonitor.TaskPerformanceMonitor());
                //GlobalConfig.Monitors.Add(new SystemMonitor.NodeHeartBeatMonitor());
                //GlobalConfig.Monitors.Add(new SystemMonitor.TaskStopMonitor());
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NodeMain());
            }

        }
    }
}
