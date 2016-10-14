using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Service.Abstract;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using mhg_internalnet2.Models;

namespace mhg_internalnet2.Controllers
{
    public class EmployeeDirectoryController : Controller
    {
        private readonly IUseresRolesServices _useresRolesServices;

        public EmployeeDirectoryController(IUseresRolesServices useresRolesServices)
        {
            _useresRolesServices = useresRolesServices;
        }
        // GET: EmployeeDirectory
        public JsonResult Index([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var result = GetDirectory().ToTreeDataSourceResult(request,
                e => e.UserId,
                e => e.ReportsTo,
                e => id.HasValue ? e.ReportsTo == id : e.ReportsTo == null,
                e => e
            );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult All([DataSourceRequest] DataSourceRequest request)
        {
            var result = GetDirectory().ToTreeDataSourceResult(request,
                e => e.UserId,
                e => e.ReportsTo,
                e => e
            );

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<EmpCombox> GetDirectory()
        {
            var emps = _useresRolesServices.GetAllEmployees().Select(x => new EmpCombox()
            {
                ReportsTo = x.ReportsTo,
                UserId = x.User.Id,
                FirstName = x.FirstName,
                Position = "CEO",
                Color = x.Color,
                HasChildren = x.RealtedEmployees.Any(),
                LastName = x.LastName
                
            });

            return emps;
        }
        //public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        employeeDirectory.Delete(employee, ModelState);
        //    }

        //    return Json(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        //}

        //public JsonResult Create([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        employeeDirectory.Insert(employee, ModelState);
        //    }

        //    return Json(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        //}

        //public JsonResult Update([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        employeeDirectory.Update(employee, ModelState);
        //    }

        //    return Json(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        //}
    }
}