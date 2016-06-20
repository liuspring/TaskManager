using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : TaskManagerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}