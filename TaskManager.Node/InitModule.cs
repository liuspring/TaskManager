﻿using System;
using Abp.Web;
using Castle.Facilities.Logging;

namespace TaskManager.Node
{
    public class InitModule : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
        }

        public InitModule()
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
        }
    }
}
