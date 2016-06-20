using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    public class AboutController : TaskManagerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}