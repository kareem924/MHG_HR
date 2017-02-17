using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Service.Abstract;
using mhg_internalnet2.Models;
using mhg_internalnet2.ViewModel;

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
            var allLoans = _borrowService.GetAllBorrows().Select(loan => new BorrowViewModel()
            {
                EmployeeName = GetEmployeesName(loan.UserId),
                Amount = loan.Amount,
                BorrowDate = loan.BorrowDate,
                BorrowId = loan.BorrowId,
                UserId = loan.UserId
            });
            return View(allLoans);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BorrowViewModel model)
        {
            if (ModelState.IsValid)
            {
                var borrowDisrtibution = new List<BorrowDistribution>();
                for (DateTime date = model.FromDate; date.Date <= model.ToDate.Date; date = date.AddMonths(1))
                {
                    var borrowDistrbuted = new BorrowDistribution()
                    {
                        Amount = model.MonthlyInstallment,
                        Date = date
                    };

                    borrowDisrtibution.Add(borrowDistrbuted);
                }
                _borrowService.InsertBorrow(new Borrow()
                {
                    PaymentNumber = model.PaymentNumber,
                    Amount = model.Amount,
                    BorrowDate = DateTime.Now,
                    Describtion = model.Describtion,
                    UserId = model.UserId,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    MonthlyInstallment = model.MonthlyInstallment,
                    BorrowDistributions = borrowDisrtibution,
                    CreatedAt = DateTime.Now
                });


                return RedirectToAction("Index", "Borrow");
            }
            return View(model);
        }

        public ActionResult LoanDetail(int userid)
        {
            
            var entity = _borrowService.GetBorrowById(userid);
            List<BorrowDistributionViewModel> model = new List<BorrowDistributionViewModel>();
            if (entity.BorrowDistributions!= null)
            {
                model.AddRange(entity.BorrowDistributions.Select(borrowDistribution => new BorrowDistributionViewModel()
                {
                    Amount = borrowDistribution.Amount, Date = borrowDistribution.Date, IsPaid = borrowDistribution.IsPaid
                }));
            }

            return PartialView("_LoanDetails",model);
        
        }
        public JsonResult GetEmployees(string text)
        {
            var emps = _useresRolesServices.GetAllEmployees();
            if (!string.IsNullOrEmpty(text))
            {
                emps = emps.Where(p => p.FirstName.Contains(text));
            }
            return Json(emps.Select(user => new EmpCombox()
            {
                FirstName = user.FirstName + " " + user.LastName,
                UserId = user.EmployeeId.ToString(),
                Color = user.Color,
                FingerPrintNumber=user.FingerPrintNumber

            }), JsonRequestBehavior.AllowGet);
        }

        public string GetEmployeesName(int userId)
        {
            var emps = _useresRolesServices.GetAllEmployees().FirstOrDefault(x => x.EmployeeId == userId);
            if (emps != null) return emps.FirstName + " " + emps.LastName;
            return String.Empty;
        }

        #region functions 

        #endregion
    }
}