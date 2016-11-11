using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Service;
using Domain.Service.Abstract;

namespace mhg_internalnet2.Filter
{
    public class NotificationFilter : ActionFilterAttribute
    {
        private readonly INotificationService _notificationService;

        public NotificationFilter()
        {
            _notificationService = DependencyResolver.Current.GetService<NotificationService>();
        }
       

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated) return;
           
            var notifications = _notificationService.GetAllNotificationes().Where(x=>x.IsRead!=true).ToList().Count();
            filterContext.Controller.ViewBag.notifications = notifications;
        }

    }
}