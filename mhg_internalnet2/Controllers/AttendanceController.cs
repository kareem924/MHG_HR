﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Service.Abstract;
using mhg_internalnet2.ViewModel;

namespace mhg_internalnet2.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendancesService _attendanceServices;

        public AttendanceController(IAttendancesService attendanceServices)
        {
            _attendanceServices = attendanceServices;
        }
        [HttpGet]
        // GET: Attendance
        public ActionResult Index()
        {
            ViewBag.Attendance = new List<AttendanceSheet>();
            ViewBag.totalWorkingHours = 0;
            ViewBag.totalDelay = 0;
            return View();
        }
        [HttpPost]
        public ActionResult Index(AttendanceFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string startTime = "9:30";
            var listAttendanceModel = new List<AttendanceSheet>();
            IEnumerable<IGrouping<int, Attendance>> attendanceSheet = _attendanceServices.GetAllAttendances().Where(x => x.FingerPrint == model.FingerPrintNumber).GroupBy(x => x.CheckedInAt.Day);
            foreach (var attendanceDay in attendanceSheet)
            {
                var attendanceModel = new AttendanceSheet();
                var firstOrDefault = attendanceDay.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    attendanceModel.CheckIn = firstOrDefault.CheckedInAt.ToString();

                    attendanceModel.Day = firstOrDefault.CheckedInAt.Date.ToLongDateString();
                }
                else
                {
                    attendanceModel.CheckIn = "Absence";
                    attendanceModel.Day = "Absence";
                }

                var lastOrDefault = attendanceDay.LastOrDefault();
                if (lastOrDefault != null)
                { attendanceModel.CheckOut = lastOrDefault.CheckedInAt.ToString(); }
                else
                {
                    attendanceModel.CheckOut = "Absence";
                }
                if (lastOrDefault!=null && firstOrDefault!=null)
                {
                    attendanceModel.WorkingHours = (lastOrDefault.CheckedInAt.TimeOfDay.Subtract(firstOrDefault.CheckedInAt.TimeOfDay)).ToString();
                }
                if (firstOrDefault != null)
                {
                    var delay = (firstOrDefault.CheckedInAt.TimeOfDay.Subtract(TimeSpan.Parse(startTime)).Minutes).ToString();
                    if (int.Parse(delay)>0)
                    {
                        attendanceModel.Delay = delay;
                    }
                    else
                    {
                        attendanceModel.Delay = "00";
                    }
                    
                }
                listAttendanceModel.Add(attendanceModel);


            }
            var totalWorkingHours = listAttendanceModel.Sum(x=>DateTime.Parse(x.WorkingHours).Hour);
            var totalDelay = listAttendanceModel.Sum(x => int.Parse(x.Delay));
            ViewBag.totalWorkingHours = totalWorkingHours;
            ViewBag.totalDelay = totalDelay;
            ViewBag.Attendance = listAttendanceModel;
            return View();
        }
    }
}