using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using TaskManager.Categories.Dto;

namespace TaskManager.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        int Create(CategoryInput input);

        CategoryOutput GetCategory(int id);

        void Update(CategoryInput input);

        List<CategoryListOutput> GetList(CategoryListInput input);

        int GetListTotal(CategoryListInput input);

        List<Category> GetAllList();
    }
}
