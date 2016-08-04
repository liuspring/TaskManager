using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using TaskManager.Authorization;
using TaskManager.Categories;
using TaskManager.Nodes;
using TaskManager.Tasks;
using TaskManager.Tasks.Dto;
using Common;
using StackExchange.Profiling;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
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
            var input = new TaskListInput(Request)
            {
                TaskName = Request["TaskName"].RequestToString()
            };
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
            var output = new TaskOutput();
            return View(output);
        }

        //
        // GET: /Task/Edit/5
        public ActionResult Edit(int id)
        {
            var categorys = _categoryAppService.GetAllList();
            ViewData["Categories"] = new SelectList(categorys, "Id", "CategoryName");
            var nodes = _nodeAppService.GetAllList();
            ViewData["Nodes"] = new SelectList(nodes, "Id", "NodeName");
            TaskOutput output = _taskAppService.GetTask(id);
            return View("Create", output);
        }



        //
        // POST: /Tasks/Create
        [HttpPost]
        public ActionResult AjaxCreate(TaskInput input)
        {
            var res = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                if (input.Id == 0)
                    _taskAppService.Create(input);
                else
                {
                    _taskAppService.Update(input);
                }
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