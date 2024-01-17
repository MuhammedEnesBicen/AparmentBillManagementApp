using Bussiness.Abstract;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Areas.TenantUser.Controllers
{
    [Area("TenantUser")]
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
            int tenantId;
            var getIdViaClaim = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out tenantId);
            if (!getIdViaClaim)
            {
                TempData["message"] = "You must login first";
                return RedirectToAction("Login", "Auth", new { area = "Identity" });
            }

            var tenantResult = tenantService.GetTenantVMById(tenantId);

            return View(tenantResult.Data);

        }

        public PartialViewResult GetBills(int tenantId, int billPage)
        {
            var tenantVM = tenantService.GetTenantVMById(tenantId).Data;
            if (tenantVM == null)
                return PartialView("_BillsPartial", null);
            var billResult = billService.GetListByApartmentId(tenantVM.ApartmentId, billPage - 1);
            return PartialView("_BillsPartial", billResult.Data);
        }


        public ActionResult UpdateProfile(int id)
        {
            var result = tenantService.GetAsDTOById(id);
            if (result.Success == false)
            {
                ViewBag.message = result.Message;
            }

            return View(result.Data);
        }
        [HttpPost]
        public ActionResult UpdateProfile(TenantDTO tenantDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(tenantDTO);
            }
            var result = tenantService.Update(tenantDTO);
            ViewBag.message = result.Message;
            return View(tenantDTO);
        }
    }
}
