using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Common;
using TaskManager.Authorization;
using TaskManager.Categories;
using TaskManager.Categories.Dto;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class CategoriesController : TaskManagerControllerBase
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoriesController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        //
        // GET: /Categories/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxCategoryList()
        {
            var input = new CategoryListInput(Request)
            {
                CategoryName = Request["CategoryName"].RequestToString()
            };
            var count = _categoryAppService.GetListTotal(input);
            var result = count == 0 ? new List<CategoryListOutput>() : _categoryAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}