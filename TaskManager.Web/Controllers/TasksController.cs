using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Categories;
using TaskManager.Nodes;
using TaskManager.Tasks;
using TaskManager.Tasks.Dto;

namespace TaskManager.Web.Controllers
{
    public class TasksController : TaskManagerControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly INodeAppService _nodeAppService;

        public TasksController(ITaskAppService taskAppService, ICategoryAppService categoryAppService, INodeAppService nodeAppService)
        {
            _taskAppService = taskAppService;
            _categoryAppService = categoryAppService;
            _nodeAppService = nodeAppService;
        }
        //
        // GET: /Tasks/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获得任务列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxTaskList()
        {
            var input = new TaskListInput(Request);
            var count = _taskAppService.GetListTotal(input);
            var result = count == 0 ? new List<TaskListOutput>() : _taskAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Task/Create
        public ActionResult Create()
        {
            var categorys = _categoryAppService.GetAllList();
            ViewData["Categories"] = new SelectList(categorys, "Id", "CategoryName");
            var nodes = _nodeAppService.GetAllList();
            ViewData["Nodes"] = new SelectList(nodes, "Id", "NodeName");
            return View();
        }

        //
        // POST: /Tasks/Create
        [HttpPost]
        public ActionResult AjaxCreate(CreateTaskInput input)
        {
            var res = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                _taskAppService.Create(input);
                res.Data = new { ret = true };
            }
            catch (Exception ex)
            {
                res.Data = new { ret = false, msg = "保存失败：" + ex.Message };
            }
            return res;
        }
    }
}