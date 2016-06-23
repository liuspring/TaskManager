using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Modules;
using TaskManager.EntityFramework;

namespace TaskManager.Node
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
            var bootstrapper=new AbpBootstrapper();
            bootstrapper.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
