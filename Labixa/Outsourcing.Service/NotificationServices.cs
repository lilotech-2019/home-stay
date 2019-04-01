using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
namespace Outsourcing.Service
{

    public interface INotificationService
    {

        IEnumerable<Notification> GetNotifications();
        Notification GetNotificationById(int NotificationId);
        void CreateNotification(Notification Notification);
        void EditNotification(Notification NotificationToEdit);
        void DeleteNotification(int NotificationId);
        void SaveNotification();
        IEnumerable<ValidationResult> CanAddNotification(Notification Notification);

    }
    public class NotificationService : INotificationService
    {
        #region Field
        private readonly INotificationRepository NotificationRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public NotificationService(INotificationRepository NotificationRepository, IUnitOfWork unitOfWork)
        {
            this.NotificationRepository = NotificationRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Notification> GetNotifications()
        {
            var Notifications = NotificationRepository.GetAll();
            return Notifications;
        }

        public Notification GetNotificationById(int NotificationId)
        {
            var Notification = NotificationRepository.GetById(NotificationId);
            return Notification;
        }

        public void CreateNotification(Notification Notification)
        {
            NotificationRepository.Add(Notification);
            SaveNotification();
        }

        public void EditNotification(Notification NotificationToEdit)
        {
            NotificationRepository.Update(NotificationToEdit);
            SaveNotification();
        }

        public void DeleteNotification(int NotificationId)
        {
            //Get Notification by id.
            var Notification = NotificationRepository.GetById(NotificationId);
            if (Notification != null)
            {
                NotificationRepository.Delete(Notification);
                SaveNotification();
            }
        }

        public void SaveNotification()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddNotification(Notification Notification)
        {

            //    yield return new ValidationResult("Notification", "ErrorString");
            return null;
        }

        #endregion
    }
}
