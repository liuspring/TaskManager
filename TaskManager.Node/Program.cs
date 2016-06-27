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
        static void Main()
        {
            //基于ABP项目加载程序集到Ioc容器中
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
