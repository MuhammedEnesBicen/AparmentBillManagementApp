using Bussiness.Abstract;
using Core.Utilities;
using Entity.DTOs;
using Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Controllers
{
    [Authorize(Roles = "manager")]
    public class TenantController : Controller
    {
        private readonly ITenantService tenantService;

        public TenantController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        public IActionResult Index()
        {
            var idResult = GetApartmentComplexIdViaClaims();
            if (idResult.Success == false)
            {
                TempData["message"] = "Something went wrong. Please re login to website.";
                return View(new List<TenantVM>());
            }

            var model = tenantService.GetTenantVMs(apartmentComplexId:idResult.Data).Data;
            return View(model);
        }

        public IActionResult Tenant([FromQuery]int apartmentId)
        {
            TenantDTO tenantDTO = new TenantDTO();
            tenantDTO.ApartmentId = apartmentId;
            return View(tenantDTO);
        }

        [HttpPost]
        public IActionResult Tenant([FromForm]TenantDTO tenantDTO) 
        {
        
            if (ModelState.IsValid)
            {
                var result = tenantService.Add(tenantDTO);
                TempData["message"] = result.Message;
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(tenantDTO);
        }

        public IActionResult Delete(int id)
        {
            var result = tenantService.DeleteById(id);
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        public IActionResult Update([FromRoute] int id)
        {
            var model = tenantService.GetAsDTOById(id);
            if(model == null)
            {
                TempData["message"] = model.Message;
            }
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult Update(TenantDTO tenantDTO)
        {
            if (ModelState.IsValid)
            {
                var result = tenantService.Update(tenantDTO);
                TempData["message"] = result.Message;
                return RedirectToAction("Index");
            }
            return View(tenantDTO) ;
        }

        [HttpPost]
        public PartialViewResult TenantsList(string? blockName, string? nameFilter, bool onlyHasDebt)
        {
            TempData["message"] = "Something went wrong. Please re login to website.";
            var idResult = GetApartmentComplexIdViaClaims();
            if (idResult.Success = false)
            {
                TempData["message"] = "Something went wrong. Please re login to website.";
                return PartialView(new List<TenantVM>());
            }

            if (blockName == "all")
            {
                blockName = null;
            }
            var model = tenantService.GetTenantVMs(idResult.Data,blockName, nameFilter, onlyHasDebt);
            return PartialView(model.Data);
        }

        public DataResult<int> GetApartmentComplexIdViaClaims()
        {
            try
            {
                int apartmentComplexId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                return new DataResult<int>(true, "Complex Id fetched succesfully", apartmentComplexId);
            }
            catch
            {
                return new DataResult<int>(false, "Complex Id couldn't fetched", -1);
            }

        }
    }
}
