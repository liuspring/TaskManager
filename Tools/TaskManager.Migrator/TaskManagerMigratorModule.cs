using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using TaskManager.EntityFramework;

namespace TaskManager.Migrator
{
    [DependsOn(typeof(TaskManagerDataModule))]
    public class TaskManagerMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<TaskManagerDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}