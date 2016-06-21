using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TaskManager.Authorization;
using TaskManager.Categories.Dto;

namespace TaskManager.Categories
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class CategoryAppService : TaskManagerAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category, int> _categoryRepository;

        public CategoryAppService(IRepository<Category, int> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public int Create(CategoryInput input)
        {
            var category = Category.Create(input.CategoryName);
            _categoryRepository.Insert(category);
            return category.Id;
        }

        public CategoryOutput GetCategory(int id)
        {
            var category = _categoryRepository.Get(id);
            return category.MapTo<CategoryOutput>();
        }

        public void Update(CategoryInput input)
        {
            var category = _categoryRepository.Get(input.Id);
            category.CategoryName = input.CategoryName;
        }

        public List<CategoryListOutput> GetList(CategoryListInput input)
        {
            var categorys = _categoryRepository.GetAll();
            if (!string.IsNullOrEmpty(input.CategoryName))
                categorys = categorys.Where(a => a.CategoryName.Contains(input.CategoryName));
            categorys = categorys
                .OrderBy(a => a.Id)
                .Skip(input.iDisplayStart)
                .Take(input.iDisplayLength);
            return categorys.MapTo<List<CategoryListOutput>>();
        }

        public int GetListTotal(CategoryListInput input)
        {
            return _categoryRepository.GetAllList().Count;
        }

        public List<Category> GetAllList()
        {
            return _categoryRepository.GetAllList();
        }
    }
}
