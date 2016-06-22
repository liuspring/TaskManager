using System.Reflection;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using TaskManager.Categories;

namespace TaskManager.Node
{
    [DependsOn(
    typeof(TaskManagerDataModule),
    typeof(TaskManagerApplicationModule))]
    public class TaskManagerNodeModule : AbpModule
    {

        public override void PostInitialize()
        {
            base.PostInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
