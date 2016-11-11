using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Service.Abstract;

namespace mhg_internalnet2.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        // GET: Notifications
        public JsonResult GetNotificationContacts()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            var list = _notificationService.GetAllNotificationes().Where(a => a.CreatedAt > notificationRegisterTime || a.IsRead != true).OrderByDescending(a => a.CreatedAt).ToList()
                .Select(notifi => new
                {
                    Details = notifi.Details,
                    controller = notifi.Controller,
                    action = notifi.Action,
                    parameter = notifi.Parameter,
                    createdAt = notifi.CreatedAt,
                    notifiId = notifi.Id
                });
            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult UpdateNotification(List<int> ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var notificationToUpdate = _notificationService.GetNotificationById(id);
                    notificationToUpdate.IsRead = true;
                    notificationToUpdate.ReadAt = DateTime.Now;
                    _notificationService.UpdateNotification(notificationToUpdate);
                }

            }
            return Json(new { success = true });
        }
    }
}