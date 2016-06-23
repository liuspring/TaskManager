using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Common;
using TaskManager.Authorization;
using TaskManager.Nodes;
using TaskManager.Nodes.Dto;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class NodesController : TaskManagerControllerBase
    {
         private readonly INodeAppService _nodeAppService;

         public NodesController(INodeAppService nodeAppService)
        {
            _nodeAppService = nodeAppService;
        } 
        //
        // GET: /Node/
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
            var input = new NodeListInput(Request)
            {
                NodeName = Request["NodeName"].RequestToString()
            };
            var count = _nodeAppService.GetListTotal(input);
            var result = count == 0 ? new List<NodeListOutput>() : _nodeAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}