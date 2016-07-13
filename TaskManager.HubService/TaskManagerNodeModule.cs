using System.Data.Entity;
using System.Reflection;
using Abp;
using Abp.Modules;
using TaskManager.EntityFramework;

namespace TaskManager.HubService
{
    [DependsOn(
    typeof(TaskManagerDataModule),
    typeof(TaskManagerApplicationModule))]
    public class TaskManagerNodeModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TaskManagerDbContext>());
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public void InitModule()
        {
            var bootstrapper = new AbpBootstrapper();
            bootstrapper.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
