using AutoMapper;
using Bussiness.Abstract;
using Entity;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AparmentBillManagementMVC.Controllers
{
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
            var billList = billService.GetListWithRelatedData().Data;
            return View(billList);
        }

        public IActionResult Bill()
        {
            return View();
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
    }
}
