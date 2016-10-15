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
            var result = _useresRolesServices.GetAllEmployees().ToTreeDataSourceResult(request,
              employee => employee.EmployeeId,
            employee => employee.ReportsTo,
            employee => new EmpCombox()
            {
                UserId = employee.EmployeeId.ToString(),
                ReportsTo = employee.ReportsTo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = "CEO",
                HasChildren = employee.RealtedEmployees.Any()
            }
            );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult All([DataSourceRequest] DataSourceRequest request)
        {
            var result = _useresRolesServices.GetAllEmployees().ToTreeDataSourceResult(request,
              employee => employee.EmployeeId,
            employee => employee.ReportsTo,
            employee => new EmpCombox()
            {
                UserId = employee.EmployeeId.ToString(),
                ReportsTo = employee.ReportsTo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = "CEO",
                HasChildren = employee.RealtedEmployees.Any()
            }
            );


            return Json(result, JsonRequestBehavior.AllowGet);
        }
      
    }
}