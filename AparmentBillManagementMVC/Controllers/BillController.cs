using AutoMapper;
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
    public class BillController : Controller
    {
        private readonly IBillService billService;
        private readonly IMapper mapper;

        public BillController(IBillService billService, IMapper mapper)
        {
            this.billService = billService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var idResult = GetApartmentComplexIdViaClaims();
            if (idResult.Success == false)
            {
                TempData["message"] = "An error occured. Please re login to website.";
                return View();
            }

            var billList = billService.GetListWithRelatedData(apartmentComplexId: idResult.Data).Data;
            return View(billList);
        }

        public IActionResult Bill(int apartmentId, string? tenantName)
        {
            BillDTO billDTO = new();
            billDTO.ApartmentId = apartmentId;
            ViewData["tenantName"] = tenantName;
            return View(billDTO);
        }

        [HttpPost]
        public IActionResult Bill([FromForm] BillDTO billDTO)
        {
            if(ModelState.IsValid)
            {
                var result = billService.Add(billDTO);
                TempData["message"] = result.Message;
                return RedirectToAction("Index");
            }
            return View(billDTO);
        }

        public IActionResult Delete([FromRoute] int id) 
        {
            var result = billService.DeleteById(id);
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }


        public IActionResult Update([FromRoute] int id)
        {
            var result = billService.GetById(id);
            var billDTO = mapper.Map<Bill,BillDTO>(result.Data);  
            return View(billDTO);
        }

        [HttpPost]
        public IActionResult Update([FromForm] BillDTO billDTO)
        {
            if(ModelState.IsValid)
            {
                var result = billService.Update(billDTO);
                TempData["message"] = result.Message;
                return RedirectToAction("Index");
            }
            return View(billDTO);
            
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
