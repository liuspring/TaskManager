using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using TaskManager.Authorization;
using TaskManager.MultiTenancy;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : TaskManagerControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}