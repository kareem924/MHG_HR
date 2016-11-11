using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
   public class NotificationService : INotificationService
    {
        private readonly IRepository<Notifications> _notificationRepository;

        public NotificationService(IRepository<Notifications> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<Notifications> GetAllNotificationes()
        {
            return _notificationRepository.Table;
        }

       public void UpdateNotification(Notifications notifications)
       {
            if (notifications == null)
                throw new ArgumentNullException("notifications");
            _notificationRepository.Update(notifications);
        }

       public Notifications GetNotificationById(int notificationId)
       {
            return _notificationRepository.GetById(notificationId);
        }
    }
}
