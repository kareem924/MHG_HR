using Domain.Entities;
using Domain.Service.Abstract;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using mhg_internalnet2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mhg_internalnet2.Controllers
{
    public class OfficalVacationController : Controller
    {
        private readonly IOfficalHolidaysService _OfficalHolidaysService;
        public OfficalVacationController(IOfficalHolidaysService OfficalHolidaysService)
        {
            _OfficalHolidaysService = OfficalHolidaysService;
        }
        // GET: OfficalVacation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var model = new List<OfficalVacationsViewModel>();
            foreach (var item in _OfficalHolidaysService.GetAllOfficalHolidays())
            {
                var OfficalHoliday = new OfficalVacationsViewModel();
                OfficalHoliday.OfficialVactionId = item.OfficialVactionId;
                OfficalHoliday.Day = item.Day;
                OfficalHoliday.Description = item.Description;
                model.Add(OfficalHoliday);
            }
            return Json(model.ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, OfficalVacationsViewModel officalVacation)
        {
            if (officalVacation != null && ModelState.IsValid)
            {
                var officalHoliday = new OfficalHolidays() { OfficialVactionId = officalVacation.OfficialVactionId, Description = officalVacation.Description, Day = officalVacation.Day };
                _OfficalHolidaysService.InsertOfficalHoliday(officalHoliday);
            }
            return Json(new[] { officalVacation }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, OfficalVacationsViewModel officalVacation)
        {
            if (officalVacation != null && ModelState.IsValid)
            {
                var officalHoliday = new OfficalHolidays() { OfficialVactionId = officalVacation.OfficialVactionId, Description = officalVacation.Description, Day = officalVacation.Day };
                _OfficalHolidaysService.UpdateOfficalHoliday(officalHoliday);
            }
            return Json(new[] { officalVacation }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, OfficalVacationsViewModel officalVacation)
        {
            if (officalVacation != null)
            {
                var officalHoliday = new OfficalHolidays() { OfficialVactionId = officalVacation.OfficialVactionId, Description = officalVacation.Description, Day = officalVacation.Day };
                _OfficalHolidaysService.UpdateOfficalHoliday(officalHoliday);
            }
            return Json(new[] { officalVacation }.ToDataSourceResult(request, ModelState));
        }
    }
}