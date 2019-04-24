using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IRoomOrderService : IServiceBase<RoomOrder>
    {
        void UpdateStatus(int id, RoomOrderStatus status);
    }

    public class RoomOrderService : ServiceBase<RoomOrder>, IRoomOrderService
    {
        #region Ctor

        public RoomOrderService(IRepository<RoomOrder> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        #endregion

        #region BaseMethod
        public void UpdateStatus(int id, RoomOrderStatus status)
        {
            var entity = FindById(id);
            entity.OrderStatus = status;
            //if (status == RoomOrderStatus.CheckIn)
            //{
            //    entity.CheckInDate = DateTime.Today;
            //    entity.CheckInTime = DateTime.Now.TimeOfDay;
            //}
            //else if (status == RoomOrderStatus.CheckOut)
            //{
            //    entity.CheckOutDate = DateTime.Today;
            //    entity.CheckOutTime = DateTime.Now.TimeOfDay;
            //   entity.Total = (entity.CheckOut - entity.CheckIn).TotalDays * entity.Room.Price;
            //}

            Edit(entity);
        }
        #endregion
    }
}