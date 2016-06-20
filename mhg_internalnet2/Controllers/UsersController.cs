using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Domain.Service.Abstract;
using mhg_internalnet2.Models;
using mhg_internalnet2.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mhg_internalnet2.Controllers
{
    public class UsersController : Controller
    {
        #region fields
        private readonly IUseresRolesServices _useresRolesServices;
        private readonly IRolesService _rolesServices;
        private readonly IDepartmentsService _departmentsService;
        private readonly IJobService _jobService;
        private readonly IBranchService _branchService;
        #endregion
        #region ctor

        public UsersController(IUseresRolesServices useresRolesServices, IRolesService rolesService, IDepartmentsService departmentsService, IJobService jobService, IBranchService branchService)
        {
            _useresRolesServices = useresRolesServices;
            _rolesServices = rolesService;
            _departmentsService = departmentsService;
            _jobService = jobService;
            _branchService = branchService;
        }
        #endregion
        // GET: Users
        public ActionResult Index()
        {
            var roles = _rolesServices.GetAllRoles();
            var allUsers = _useresRolesServices.GetAllUsers().Select(user => new UserRoleViewModel
            {
                Active = user.IsActive,
                UserId = user.Id,
                UserName = user.Employee.FirstName,
                Roles = roles.Where(x => x.Users.Any(y => y.UserId == user.Id)).Select(role => new RoleViewModel
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                }).ToList()

            }).OrderBy(y => y.UserName);

            return View(allUsers);
        }

        public ActionResult Edit(string userid)
        {
            var roles = _rolesServices.GetAllRoles();
            var selectedUser = _useresRolesServices.GetAllUsers().Where(user => user.Id == userid).Select(user => new UserRoleViewModel
            {
                UserName = user.Employee.FirstName,
                Roles = roles.Where(x => x.Users.Any(y => y.UserId == user.Id)).Select(role => new RoleViewModel
                {
                    RoleName = role.Name,

                }).ToList()
            }).FirstOrDefault();

            ViewBag.roleName = new SelectList(_rolesServices.GetAllRoles(), "Name", "Name");

            if (selectedUser != null)
            {
                ViewBag.UserRoles = new SelectList(selectedUser.Roles, "RoleName", "RoleName");
                return View("edit", selectedUser);
            }
            return View("edit", selectedUser);
        }

        public ActionResult AddToRole(string userId, string roleName)
        {
            var result = this.AddUserToRole(userId, roleName);
            if (result.Succeeded)
            {
                ViewBag.Message = "User has been added to role successfully!.";
                ViewBag.Status = "success";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return this.Edit(userId);
        }


        public ActionResult CreateUser()
        {
            ViewBag.Departments = new SelectList(_departmentsService.GetAllDepartments(), "Id", "Name");
            ViewBag.JobPlaces = new SelectList(_branchService.GetAllBranches(), "StoreId", "StoreName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PasswordHash = Crypto.HashPassword(model.ConfirmPassword),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    IsActive = true,
                    Employee = new Employee()
                    {
                        FirstName = model.FirstName,
                        MiddleName = model.MobilePhone,
                        LastName = model.LastName,
                        Address = model.Address,
                        IdNumber = model.NationalId,
                        MobilePhone = model.MobilePhone,
                        HomePhone = model.HomePhone,
                        BirthDate = model.BirthDate,
                        HireDate = model.HireDate,
                        JobId = model.JobId,
                        IsMale = model.IsMale,
                        BranchId = model.StoreId,
                        BankAccount = model.BankAccount,
                        FingerPrintNumber = model.FingerPrintNumber

                    }
                };
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    model.ProfilePicPath = Guid.NewGuid().ToString();
                    file?.SaveAs(Path.Combine(Server.MapPath("~/Content/ProfileImages"), model.ProfilePicPath + ".jpg"));
                }
                _useresRolesServices.InsertUser(user);
                ViewBag.SucessMessage = model.FirstName + " " + "Has been Add successfuly";
                return RedirectToAction("Index", "Users");
            }
            ViewBag.Departments = new SelectList(_departmentsService.GetAllDepartments(), "Id", "Name");
            ViewBag.JobPlaces = new SelectList(_branchService.GetAllBranches(), "StoreId", "StoreName");
            return View(model);
        }

        public ActionResult EditUser(string userId)
        {
            ViewBag.Departments = new SelectList(_departmentsService.GetAllDepartments(), "Id", "Name");
            ViewBag.JobPlaces = new SelectList(_branchService.GetAllBranches(), "StoreId", "StoreName");
            var user = _useresRolesServices.GetUser(userId);
            var model = new EditUserViewModel()
            {
                UserId = user.Id,
                FirstName = user.Employee.FirstName,
                MobilePhone = user.Employee.MobilePhone,
                Address = user.Employee.Address,
                JobId = user.Employee.JobId,
                StoreId = user.Employee.BranchId,
                Color = user.Employee.Color,
                LastName = user.Employee.LastName,
                HomePhone = user.Employee.HomePhone,
                Email = user.Email,
                IsMale = user.Employee.IsMale.HasValue,
                MiddleName = user.Employee.MiddleName,
                HireDate = user.Employee.HireDate,
                BankAccount = user.Employee.BankAccount,
                BirthDate = user.Employee.BirthDate,
                FingerPrintNumber = user.Employee.FingerPrintNumber,
                NationalId = user.Employee.IdNumber,
                ShortDescription = user.Employee.ShortDescription,


            };

            return View(model);
        }
        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            ViewBag.Departments = new SelectList(_departmentsService.GetAllDepartments(), "Id", "Name");
            ViewBag.JobPlaces = new SelectList(_branchService.GetAllBranches(), "StoreId", "StoreName");
            ViewBag.jobs = new SelectList(_jobService.GetAllJobs(), "Id", "Name");

            if (ModelState.IsValid)
            {
                var userToupdate = _useresRolesServices.GetUser(model.UserId);

                userToupdate.UserName = model.Email;
                userToupdate.Email = model.Email;

                userToupdate.SecurityStamp = Guid.NewGuid().ToString("D");
                userToupdate.IsActive = true;
                userToupdate.Employee.FirstName = model.FirstName;
                userToupdate.Employee.MiddleName = model.MiddleName;
                userToupdate.Employee.LastName = model.LastName;
                userToupdate.Employee.Address = model.Address;
                userToupdate.Employee.IdNumber = model.NationalId;
                userToupdate.Employee.HomePhone = model.HomePhone;
                userToupdate.Employee.BirthDate = model.BirthDate;
                userToupdate.Employee.HireDate = model.HireDate;
                userToupdate.Employee.JobId = model.JobId;
                userToupdate.Employee.IsMale = model.IsMale;
                userToupdate.Employee.BankAccount = model.BankAccount;
                userToupdate.Employee.FingerPrintNumber = model.FingerPrintNumber;

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    userToupdate.Employee.ProfilePicPath = Guid.NewGuid().ToString();
                    file?.SaveAs(Path.Combine(Server.MapPath("~/Content/ProfileImages"), model.ProfilePicPath + ".jpg"));
                }
                _useresRolesServices.UpdateUser(userToupdate);
                ViewBag.SucessMessage = model.FirstName + " " + "Has been Add successfuly";
                return RedirectToAction("Index", "Users");
            }
            return View(model);

        }
        public ActionResult RemoveFromRole(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MhgHrDataContext()));
            var result = userManager.RemoveFromRole(userId, roleName);
            if (result.Succeeded)
            {
                ViewBag.Message = "User has been removed from role successfully!.";
                ViewBag.Status = "danger";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return Edit(userId);
        }
        private IdentityResult AddUserToRole(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MhgHrDataContext()));
            IdentityResult result;
            if (!userManager.IsInRole(userId, roleName))
            {
                result = userManager.AddToRole(userId, roleName);
            }
            else
            {
                string message = $"User is already existed in {roleName} Role";
                result = new IdentityResult(message);
            }
            return result;
        }

        public ActionResult FillJobs(int id)
        {
            var jobs = _jobService.GetAllJobsByDepartment(id);
            return Json(jobs, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult UpdateActive(string UserId)
        {
           
            var user = _useresRolesServices.GetUser(UserId);
            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            _useresRolesServices.UpdateUser(user);
           return RedirectToAction("Index", "Users");

        }
    }
}