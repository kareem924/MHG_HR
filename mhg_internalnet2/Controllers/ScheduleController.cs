using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Service.Abstract;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using mhg_internalnet2.ViewModel;

namespace mhg_internalnet2.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ITasksService _tasksService;

        public ScheduleController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }
        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var model = _tasksService.GetAlltasks().Select(x => new TaskViewModel()
            {
              Title = x.Title,
              Description = x.Description,
              CreatedBy = x.CreatedBy,
              CreatedAt = x.CreatedAt,
              StartTimezone = x.StartTimezon,
              RecurrenceRule = x.RecurrenceRule,
              EndTimezone = x.EndTimezone,
              IsAllDay = x.IsAllDay,
              RecurrenceException = x.RecurrenceException,
              Start = x.Start,
              End = x.End,
              UserId = x.UserId,
              RecurrenceId = x.RecurrenceId,
              TaskId = x.TaskId
            }).ToList();
            return Json(model.ToDataSourceResult(request));
        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var tasks = new Tasks()
                {
                    Title = task.Title,
                    Description = task.Description,
                    CreatedBy = task.CreatedBy,
                    CreatedAt = task.CreatedAt,
                    StartTimezon = task.StartTimezone,
                    RecurrenceRule = task.RecurrenceRule,
                    EndTimezone = task.EndTimezone,
                    IsAllDay = task.IsAllDay,
                    RecurrenceException = task.RecurrenceException,
                    Start = task.Start,
                    End = task.End,
                    UserId = task.UserId,
                    RecurrenceId = task.RecurrenceId,
                    TaskId = task.TaskId
                };
                _tasksService.DeleteTask(tasks);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                if (task.Start > task.End)
                {
                    ModelState.AddModelError("Start", "End date must be greater or equal to Start date.");
                    return Json(new[] { task }.ToDataSourceResult(request, ModelState));
                }
                var tasks = new Tasks()
                {
                    Title = task.Title,
                    Description = task.Description,
                    CreatedBy = task.CreatedBy,
                    CreatedAt = task.CreatedAt,
                    StartTimezon = task.StartTimezone,
                    RecurrenceRule = task.RecurrenceRule,
                    EndTimezone = task.EndTimezone,
                    IsAllDay = task.IsAllDay,
                    RecurrenceException = task.RecurrenceException,
                    Start = task.Start,
                    End = task.End,
                    UserId = task.UserId,
                    RecurrenceId = task.RecurrenceId,
                    TaskId = task.TaskId
                };
                _tasksService.Inserttask(tasks);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                if (task.Start > task.End)
                {
                    ModelState.AddModelError("Start", "End date must be greater or equal to Start date.");
                    return Json(new[] { task }.ToDataSourceResult(request, ModelState));
                }
                var tasks = new Tasks()
                {
                    Title = task.Title,
                    Description = task.Description,
                    CreatedBy = task.CreatedBy,
                    CreatedAt = task.CreatedAt,
                    StartTimezon = task.StartTimezone,
                    RecurrenceRule = task.RecurrenceRule,
                    EndTimezone = task.EndTimezone,
                    IsAllDay = task.IsAllDay,
                    RecurrenceException = task.RecurrenceException,
                    Start = task.Start,
                    End = task.End,
                    UserId = task.UserId,
                    RecurrenceId = task.RecurrenceId,
                    TaskId = task.TaskId
                };
                _tasksService.Updatetask(tasks);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }
    }
}