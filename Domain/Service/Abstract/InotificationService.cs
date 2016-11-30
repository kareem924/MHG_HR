using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Service.Abstract
{
   public interface INotificationService
    {
        IEnumerable<Notifications> GetAllNotificationes();
        void UpdateNotification(Notifications notifications);
        void InsertNotification(Notifications notifications);
        Notifications GetNotificationById(int notificationId);
    }
}
