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
        private readonly IVacationTypes _vacationTypesService;
        private readonly INotificationService _notificationService;
        public VacationsController(IVacationService vacationService, IVacationTypes vacationTypesService, INotificationService notificationService)
        {
            _vacationService = vacationService;
            _vacationTypesService = vacationTypesService;
            _notificationService = notificationService;
        }
        // GET: Vacations
        public ActionResult Index()
        {
            var model = _vacationService.GetAllVacationByUserId(1, 1).Select(x => new VacationsModel()
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
                    TypeId = 1,
                    ToDate = model.ToDate,
                    Reason = model.Reason,

                });
                _notificationService.InsertNotification(new Notifications()
                {
                    Details = "Employee Asked For Vacation"
                });
                string url = Url.Action("Index", "Vacations");

                return Json(new { success = true, url = url });
            }

            return PartialView("_Create");
        }

        public ActionResult GetVacations()
        {
            var vacTypes = _vacationTypesService.GetAllVacationsType();
            
            return Json(vacTypes.Select(type => new VacationTypeModel()
            {
                VacationName = type.VacationName,
                Id = type.Id

            }), JsonRequestBehavior.AllowGet);
        }
    }
}