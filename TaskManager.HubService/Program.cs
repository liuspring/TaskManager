using System;
using System.ServiceProcess;
using System.Windows.Forms;
using Abp;
using Castle.Facilities.Logging;

namespace TaskManager.HubService
{
    static class Program
    {
        private const bool IsService = false; //是否Windows服务
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
            if (IsService)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new Service1()
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new HubMain());
            }

        }
    }
}
