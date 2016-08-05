using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using TaskManager.Authorization;
using TaskManager.Errors;
using TaskManager.Errors.Dto;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class ErrorsController : TaskManagerControllerBase
    {

        private readonly IErrorAppService _errorAppService;

        public ErrorsController(IErrorAppService errorAppService)
        {
            _errorAppService = errorAppService;
        }

        //
        // GET: /Errors/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AjaxList()
        {
            var input = new ErrorListInput(Request) { };
            var count = _errorAppService.GetListTotal(input);
            var result = count == 0 ? new List<ErrorListOutput>() : _errorAppService.GetList(input);
            var response = new DataTablesResponse()
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}