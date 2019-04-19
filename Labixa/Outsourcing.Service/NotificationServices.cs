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
        Notification GetNotificationById(int notificationId);
        void CreateNotification(Notification notification);
        void EditNotification(Notification notificationToEdit);
        void DeleteNotification(int notificationId);
        void SaveNotification();
        IEnumerable<ValidationResult> CanAddNotification(Notification notification);

    }
    public class NotificationService : INotificationService
    {
        #region Field
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Notification> GetNotifications()
        {
            var notifications = _notificationRepository.GetAll();
            return notifications;
        }

        public Notification GetNotificationById(int notificationId)
        {
            var notification = _notificationRepository.GetById(notificationId);
            return notification;
        }

        public void CreateNotification(Notification notification)
        {
            _notificationRepository.Add(notification);
            SaveNotification();
        }

        public void EditNotification(Notification notificationToEdit)
        {
            _notificationRepository.Update(notificationToEdit);
            SaveNotification();
        }

        public void DeleteNotification(int notificationId)
        {
            //Get Notification by id.
            var notification = _notificationRepository.GetById(notificationId);
            if (notification != null)
            {
                _notificationRepository.Delete(notification);
                SaveNotification();
            }
        }

        public void SaveNotification()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddNotification(Notification notification)
        {

            //    yield return new ValidationResult("Notification", "ErrorString");
            return null;
        }

        #endregion
    }
}
