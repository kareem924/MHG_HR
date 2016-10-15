using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Service.Abstract;
using mhg_internalnet2.ViewModel;

namespace mhg_internalnet2.Controllers
{
    public class VacationsController : Controller
    {
        private readonly IVacationService _vacationService;

        public VacationsController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }
        // GET: Vacations
        public ActionResult Index()
        {
            var model = _vacationService.GetAllVacationByUserId(1,1).Select(x => new VacationsModel()
            {
                CreatedAt = x.CreatedAt,
                IsApproved = x.IsApproved,
                Reason = x.Reason
            });
            return View(model);
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
       
        public ActionResult Create(VacationsModel model)
        {
            if (ModelState.IsValid)
            {
                _vacationService.InsertVacation(new Vacation()
                {
                    EmployeeId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    FromDate = model.FromDate,
                    TypeId =1,
                    ToDate = model.ToDate,
                    Reason = model.Reason,
                    
                });
                string url = Url.Action("Index", "Vacations");

                return Json(new { success = true, url = url });
            }

            return PartialView("_Create");
        }
    }
}