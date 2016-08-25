using Domain.Service.Abstract;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using mhg_internalnet2.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Entities;
using System;
using Microsoft.AspNet.Identity;
using System.Web;
using System.IO;

namespace mhg_internalnet2.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IUserDocumentsService _UserDocumentsService;
        private string FileName { get; set; }
        public DocumentsController(IUserDocumentsService UserDocumentsService)
        {
            _UserDocumentsService = UserDocumentsService;
        }
        // GET: Documents
        public ActionResult Index(string userId)
        {
            ViewBag.userId = userId;
            Session["UserId"] = userId;
            return View();
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, string userId)
        {
            var model = new List<UserDocumentsViewModel>();
            foreach (var item in _UserDocumentsService.GetAllUserDocumentByUserId(userId))
            {
                var UserDocuments = new UserDocumentsViewModel();
                UserDocuments.DocumentName = item.DocumentName;
                UserDocuments.CreatedAt = item.CreatedAt;
                UserDocuments.CreatedBy = item.CreatedBy;
                UserDocuments.DocumentHashName = item.DocumentHashName;
                UserDocuments.DocumentId = item.DocumentId;
                UserDocuments.DocumentName = item.DocumentName;
                UserDocuments.FileName = item.FileName;
                UserDocuments.UserId = item.UserId;

                model.Add(UserDocuments);
            }
            return Json(model.ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, UserDocumentsViewModel userDocumentModel)
        {
            if (userDocumentModel != null && ModelState.IsValid)
            {
                var userDocument = new UserDocuments()
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.Identity.GetUserId(),
                    FileName = userDocumentModel.FileName,
                    UserId = Session["UserId"].ToString(),
                    DocumentName= Session["filename"].ToString(),
                };
                _UserDocumentsService.InsertUserDocument(userDocument);
            }
            return Json(new[] { userDocumentModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, UserDocumentsViewModel userDocumentModel)
        {
            if (userDocumentModel != null)
            {
                var userDocument = new UserDocuments() { DocumentId = userDocumentModel.DocumentId, UserId = userDocumentModel.UserId, FileName = userDocumentModel.FileName, DocumentName = userDocumentModel.DocumentName, DocumentHashName = userDocumentModel.DocumentHashName, CreatedAt = userDocumentModel.CreatedAt, CreatedBy = userDocumentModel.CreatedBy};
                _UserDocumentsService.DeleteUserDocument(userDocument);
                var physicalPath = Path.Combine(Server.MapPath("~/Img/UserDocuments"), userDocumentModel.DocumentName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Json(new[] { userDocumentModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    Session["filename"] = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Img/UserDocuments"), Session["filename"].ToString());
                    file.SaveAs(physicalPath);
                }
            }
            return new EmptyResult();
        }
        public ActionResult Remove(string[] fileNames)
        {
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data/UserDocuments"), fileName);
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }
            return new EmptyResult();
        }

    }
}