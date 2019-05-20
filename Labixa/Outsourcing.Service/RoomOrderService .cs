using System;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Service
{
    public interface IRoomOrderService : IServiceBase<RoomOrder>
    {
        void UpdateStatus(int id, RoomOrderStatus status);
        double GetTotalPrice(int id);
    }

    public class RoomOrderService : ServiceBase<RoomOrder>, IRoomOrderService
    {
        #region Ctor

        public RoomOrderService(IRepository<RoomOrder> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        #endregion

        #region BaseMethod
        #region GetTotal
        public double  GetTotalPrice(int id)
        {

            var entity = FindById(id);
            TimeSpan dayTotal = entity.CheckOut - entity.CheckIn;
            entity.Total = (dayTotal.TotalDays * entity.Price) + entity.TotalBookPrice;
            return entity.Total;
        }
        #endregion

        public void UpdateStatus(int id, RoomOrderStatus status)
        {
            var entity = FindById(id);
            entity.OrderStatus = status;
            if (status == RoomOrderStatus.CheckIn)
            {
                entity.CheckIn = DateTime.Today;
                entity.CheckInTime = DateTime.Now.TimeOfDay;
            }
            else if (status == RoomOrderStatus.CheckOut)
            {
                entity.CheckOut = DateTime.Today;
                entity.CheckOutTime = DateTime.Now.TimeOfDay;
                entity.Total = (entity.CheckOut - entity.CheckIn).TotalDays * entity.Room.Price;
            }

            Edit(entity);
        }
        #endregion
    }
}