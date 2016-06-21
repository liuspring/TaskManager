using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
//using Abp.Web.SignalR;
using TaskManager.Api;
using Hangfire;

namespace TaskManager.Web
{
    [DependsOn(
        typeof(TaskManagerDataModule),
        typeof(TaskManagerApplicationModule),
        typeof(TaskManagerWebApiModule),
        //typeof(AbpWebSignalRModule),
        typeof(AbpHangfireModule),
        typeof(AbpWebMvcModule))]
    public class TaskManagerWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<TaskManagerNavigationProvider>();

            //Configure Hangfire
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
