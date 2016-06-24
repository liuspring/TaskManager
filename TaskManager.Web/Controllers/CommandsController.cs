using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Common;
using TaskManager.Authorization;
using TaskManager.Commands;
using TaskManager.Commands.Dto;
using TaskManager.Nodes;
using TaskManager.Tasks;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class CommandsController : TaskManagerControllerBase
    {
        private readonly ICommandAppService _commandAppService;
        private readonly ITaskAppService _taskAppService;
        private readonly INodeAppService _nodeAppService;

        public CommandsController(ICommandAppService commandAppService, ITaskAppService taskAppService, NodeAppService nodeAppService)
        {
            _commandAppService = commandAppService;
            _taskAppService = taskAppService;
            _nodeAppService = nodeAppService;
        }
        //
        // GET: /Commands/
        public ActionResult Index()
        {
            var nodes = _nodeAppService.GetAllList();
            ViewData["Nodes"] = new SelectList(nodes, "Id", "NodeName");
            var tasks = _taskAppService.GetAllList();
            ViewData["Tasks"] = new SelectList(tasks, "Id", "TaskName");
            return View();
        }

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxCommandList()
        {
            var input = new CommandListInput(Request)
            {
                CommandName = Request["CommandName"].RequestToString()
            };
            var count = _commandAppService.GetListTotal(input);
            var result = count == 0 ? new List<CommandListOutput>() : _commandAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}