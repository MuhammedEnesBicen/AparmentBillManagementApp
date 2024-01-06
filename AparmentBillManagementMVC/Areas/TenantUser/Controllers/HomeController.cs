using Bussiness.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Areas.TenantUser.Controllers
{
    [Area("TenantUser")]
    [Route("TenantUser/[controller]/[action]")]
    [Authorize(Roles = "tenant")]
    public class HomeController : Controller
    {
        private readonly ITenantService tenantService;
        private readonly IBillService billService;

        public HomeController(ITenantService tenantService, IBillService billService)
        {
            this.tenantService = tenantService;
            this.billService = billService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userMail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var tenantId = tenantService.GetByMail(userMail).Data.Id;
            var tenantResult = tenantService.GetTenantVMById(tenantId);
            //if(tenantResult.Success == false)
            //    return RedirectToAction("Index", "Home");
            TempData["message"] = tenantResult.Message;
            return View(tenantResult.Data);
            
        }

        public PartialViewResult GetBills(int tenantId)
        {
            var tenantVM = tenantService.GetTenantVMById(tenantId).Data;
            if (tenantVM == null)
                return PartialView("_BillsPartial", null);
            var billResult = billService.GetListByApartmentId(tenantVM.ApartmentId);
            return PartialView("_BillsPartial", billResult.Data);
        }
    }
}
