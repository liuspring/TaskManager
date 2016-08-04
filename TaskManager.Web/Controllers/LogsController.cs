using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Logs;
using TaskManager.Logs.Dto;

namespace TaskManager.Web.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogAppService _logAppService;

        public LogsController(ILogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        //
        // GET: /Logs/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AjaxList()
        {
            var input = new LogListInput(Request) { };
            var count = _logAppService.GetListTotal(input);
            var result = count == 0 ? new List<LogListOutput>() : _logAppService.GetList(input);
            var response = new DataTablesResponse()
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}