using Bussiness.Abstract;
using Core.Utilities;
using Entity;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Controllers
{
    [Authorize(Roles = "manager")]
    public class ApartmentController : Controller
    {
        private readonly IApartmentService apartmentService;
        public ApartmentController(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
        }
        public IActionResult Index()
        {
            int apartmentComplexId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = apartmentService.GetApartmentVMsByComplexId(apartmentComplexId);
            return View(result.Data);
        }

        public IActionResult Apartment()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Apartment([FromForm] ApartmentDTO apartmentDTO)
        {
            var ApartmentComplexIdResult = GetApartmentComplexIdViaClaims();
            if (ApartmentComplexIdResult.Success == false)
            {
                TempData["message"] = "An error occured. Please re login to website.";
                return View();
            }

            if (ModelState.IsValid)
            {
                apartmentDTO.ApartmentComplexId = ApartmentComplexIdResult.Data;
                var result = apartmentService.Add(apartmentDTO);
                TempData["message"] = result.Message;
                if (result.Success == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Delete([FromRoute] int id)
        {
            var result = apartmentService.DeleteById(id);
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        public IActionResult Update([FromRoute] int id)
        {
            var apartmentForUpdate = apartmentService.GetById(id).Data;
            return View(apartmentForUpdate);
        }

        [HttpPost]
        public IActionResult Update([FromForm] ApartmentDTO apartmentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = apartmentService.Update(apartmentDTO);
                TempData["message"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        public PartialViewResult ApartmentsList(string? blockName, string? nameFilter, bool onlyHasDebt)
        {
            var getApartmentComplexIdResult = GetApartmentComplexIdViaClaims();
            if (getApartmentComplexIdResult.Success == false)
            {
                TempData["message"] = "An error occured. Please re login to website.";
                return PartialView(null);
            }

            int apartmentComplexId = getApartmentComplexIdResult.Data;

            if (blockName == "all")
                blockName = null;
            var result = apartmentService.GetApartmentVMsByComplexId(apartmentComplexId, blockName, nameFilter, onlyHasDebt);
            return PartialView(result.Data);
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
