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
    public class AgendaController : Controller
    {
        private readonly IAgendaService _agendaService;
        private readonly IDependencyAgendaService _dependencyAgendaService;
        // GET: Agenda
        public AgendaController(IAgendaService agendaService, IDependencyAgendaService dependencyAgendaService)
        {
            _agendaService = agendaService;
            _dependencyAgendaService = dependencyAgendaService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public virtual JsonResult ReadTasks([DataSourceRequest] DataSourceRequest request)
        {
            var model = _agendaService.GetAllAgendas().Select(x => new AgendaViewModel()
            {
                UserId = x.UserId,
                AgendaId = x.AgendaId,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                Summary = x.Summary,
                End = x.End,
                Start = x.Start,
                Title = x.Title,
                Expanded = x.Expanded,
                OrderId = x.OrderId,
                ParentId = x.ParentId,
                PercentComplete = x.PercentComplete
            }).ToList();


            return Json(model.ToDataSourceResult(request));
        }

        public virtual JsonResult DestroyTask([DataSourceRequest] DataSourceRequest request, AgendaViewModel agendaModel)
        {
            if (ModelState.IsValid)
            {
                var agenda = new Agenda()
                {
                    UserId = agendaModel.UserId,
                    AgendaId = agendaModel.AgendaId,
                    CreatedAt = agendaModel.CreatedAt,
                    CreatedBy = agendaModel.CreatedBy,
                    Summary = agendaModel.Summary,
                    End = agendaModel.End,
                    Start = agendaModel.Start,
                    Title = agendaModel.Title,
                    Expanded = agendaModel.Expanded,
                    OrderId = agendaModel.OrderId,
                    ParentId = agendaModel.ParentId,
                    PercentComplete = agendaModel.PercentComplete
                };
                _agendaService.DeleteAgenda(agenda);
            }

            return Json(new[] { agendaModel }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult CreateTask([DataSourceRequest] DataSourceRequest request, AgendaViewModel agendaModel)
        {
            if (ModelState.IsValid)
            {
                if (agendaModel.Start>agendaModel.End)
                {
                    ModelState.AddModelError("Start", "End date must be greater or equal to Start date.");
                    return Json(new[] { agendaModel }.ToDataSourceResult(request, ModelState));
                }
                var agenda = new Agenda()
                {
                    UserId = agendaModel.UserId,
                    AgendaId = agendaModel.AgendaId,
                    CreatedAt = agendaModel.CreatedAt,
                    CreatedBy = agendaModel.CreatedBy,
                    Summary = agendaModel.Summary,
                    End = agendaModel.End,
                    Start = agendaModel.Start,
                    Title = agendaModel.Title,
                    Expanded = agendaModel.Expanded,
                    OrderId = agendaModel.OrderId,
                    ParentId = agendaModel.ParentId,
                    PercentComplete = agendaModel.PercentComplete

                };
              
                _agendaService.InsertAgenda(agenda);
            }

            return Json(new[] { agendaModel }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult UpdateTask([DataSourceRequest] DataSourceRequest request, AgendaViewModel agendaModel)
        {
            if (ModelState.IsValid)
            {
                if (agendaModel.Start > agendaModel.End)
                {
                    ModelState.AddModelError("Start", "End date must be greater or equal to Start date.");
                    return Json(new[] { agendaModel }.ToDataSourceResult(request, ModelState));
                }
                var agenda = new Agenda()
                {
                    UserId = agendaModel.UserId,
                    AgendaId = agendaModel.AgendaId,
                    CreatedAt = agendaModel.CreatedAt,
                    CreatedBy = agendaModel.CreatedBy,
                    Summary = agendaModel.Summary,
                    End = agendaModel.End,
                    Start = agendaModel.Start,
                    Title = agendaModel.Title,
                    Expanded = agendaModel.Expanded,
                    OrderId = agendaModel.OrderId,
                    ParentId = agendaModel.ParentId,
                    PercentComplete = agendaModel.PercentComplete

                };

                _agendaService.UpdateAgenda(agenda);
            }

            return Json(new[] { agendaModel }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult ReadDependencies([DataSourceRequest] DataSourceRequest request)
        {
            var model = _dependencyAgendaService.GetAllDependencyAgendas().Select(x => new DependencyViewModel()
            {
              SuccessorId = x.SuccessorId,
              DependencyId = x.DependencyId,
              PredecessorId = x.PredecessorId,
              Type = (DependencyType)Enum.ToObject(typeof(DependencyType), x.Type)
        }).ToList();
            return Json(model.ToDataSourceResult(request));
        }

        public virtual JsonResult DestroyDependency([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
        {
            if (ModelState.IsValid)
            {
                var dependencyAgenda = new DependencyAgenda()
                {
                    SuccessorId = dependency.SuccessorId,
                    DependencyId = dependency.DependencyId,
                    PredecessorId = dependency.PredecessorId,
                    Type = (int)dependency.Type
                };
                _dependencyAgendaService.DeleteDependencyAgenda(dependencyAgenda);
            }

            return Json(new[] { dependency }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult CreateDependency([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
        {
            if (ModelState.IsValid)
            {
                var dependencyAgenda = new DependencyAgenda()
                {
                    SuccessorId = dependency.SuccessorId,
                    DependencyId = dependency.DependencyId,
                    PredecessorId = dependency.PredecessorId,
                    Type = (int)dependency.Type
                };
                _dependencyAgendaService.InsertDependencyAgenda(dependencyAgenda);
            }

            return Json(new[] { dependency }.ToDataSourceResult(request, ModelState));
        }

    }
}