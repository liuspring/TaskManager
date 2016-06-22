using Abp.Dependency;
using TaskManager.Categories;

namespace TaskManager.Node
{
    public class Test:TaskManagerNodeBase
    {
        private readonly ICategoryAppService _categoryAppService;
        public Test()
        {
            _categoryAppService = IocManager.Instance.Resolve<ICategoryAppService>();
        }

        public void Get()
        {
            var a = _categoryAppService.GetAllList();
        }
    }
}
