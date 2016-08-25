using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Service.Abstract;
using mhg_internalnet2.Models;

namespace mhg_internalnet2.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IUseresRolesServices _useresRolesServices;

        public BorrowController(IBorrowService borrowService, IUseresRolesServices useresRolesServices)
        {
            _borrowService = borrowService;
            _useresRolesServices = useresRolesServices;
        }

        // GET: Borrow
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetEmployees(string text)
        {
            var emps = _useresRolesServices.GetAllEmployees().Select(user => new RegisterViewModel
            {
                FirstName = user.FirstName,
                UserId = user.User.Id

            } );
            if (!string.IsNullOrEmpty(text))
            {
                emps = emps.Where(p => p.FirstName.Contains(text));
            }
            return Json(emps, JsonRequestBehavior.AllowGet);
        }
    }
}