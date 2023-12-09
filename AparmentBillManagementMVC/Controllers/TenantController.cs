using Bussiness.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace AparmentBillManagementMVC.Controllers
{
    public class TenantController : Controller
    {
        private readonly ITenantService tenantService;

        public TenantController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        public IActionResult Index()
        {
            var model = tenantService.GetList().Data;
            return View(model);
        }

        public IActionResult Tenant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Tenant([FromForm]Tenant tenant) {
        
            if (ModelState.IsValid)
            {
                var result = tenantService.Add(tenant);
                TempData["message"] = result.Message;
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(tenant);
        }

        public IActionResult Delete(int id)
        {
            var result = tenantService.DeleteById(id);
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        public IActionResult Update([FromRoute] int id)
        {
            var model = tenantService.GetById(id).Data;
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                var result = tenantService.Update(tenant);
                TempData["message"] = result.Message;
                return RedirectToAction("Index");
            }
            return View(tenant) ;
        }
    }
}
