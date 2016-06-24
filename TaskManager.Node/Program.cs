using System;
using System.Windows.Forms;
using Abp;
using Castle.Facilities.Logging;


namespace TaskManager.Node
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (args.Length == 1)
                {
                    GlobalConfig.NodeId = Convert.ToInt32(args[0]);
                }
                else if (args.Length == 2)
                {
                    GlobalConfig.NodeId = Convert.ToInt32(args[0]);
                }
            }
            using (var bootstrapper = new AbpBootstrapper())
            {
                bootstrapper.IocManager.IocContainer
                    .AddFacility<LoggingFacility>(f => f.UseLog4Net()
                        .WithConfig("log4net.config")
                    );
                bootstrapper.Initialize();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TaskNode());
        }
    }
}
