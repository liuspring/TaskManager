using System;
using Abp.Web;
using Castle.Facilities.Logging;
using StackExchange.Profiling;

namespace TaskManager.Web
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();//初始化性能监控
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
        }

        //protected void Application_BeginRequest()
        //{
        //    if (Request.IsLocal)//这里是允许本地访问启动监控,可不写
        //    {
        //        MiniProfiler.Start();

        //    }
        //}

        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}
    }
}
