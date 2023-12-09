﻿using Bussiness.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace AparmentBillManagementMVC.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentService apartmentService;
        public ApartmentController(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
        }
        public IActionResult Index()
        {
            
            var result = apartmentService.GetList();
            return View(result.Data);
        }


        public IActionResult Apartment()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Apartment([FromForm]Apartment apartment) 
        {
        
            if (ModelState.IsValid)
            {
              var result = apartmentService.Add(apartment);
            if(result.Success ==true) return RedirectToAction("Index");
            return View("not added");
            }
            return View();
            

        }

        public IActionResult Delete([FromRoute] int id) {
            var result = apartmentService.DeleteById(id);

TempData["message"]=result.Message;
            
            
            return RedirectToAction("Index");
        
        }

        public IActionResult Update([FromRoute] int id)
        {
            var apartmentForUpdate  = apartmentService.GetById(id).Data;

            return View(apartmentForUpdate);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Apartment apartment)
        {
            if(ModelState.IsValid)
            {
            var result = apartmentService.Update(apartment);

            TempData["message"] = result.Message;


            return RedirectToAction("Index");
            }
            return View();


        }
    }
}
