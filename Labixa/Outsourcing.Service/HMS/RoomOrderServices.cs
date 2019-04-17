using System;
using System.Linq;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface IRoomOrderService
    {
        IQueryable<RoomOrder> FindAll();
        RoomOrder FindById(int id);
        void UpdateStatus(int id, RoomOrderStatus status);
        void Create(RoomOrder entity);
        void Edit(RoomOrder entity);
        void Delete(int id);
        void Delete(RoomOrder entity);
    }

    public class RoomOrderService : IRoomOrderService
    {
        #region Field

        private readonly IRoomOrderRepository _roomOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public RoomOrderService(IRoomOrderRepository roomOrderRepository, IUnitOfWork unitOfWork)
        {
            _roomOrderRepository = roomOrderRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region BaseMethod

        public IQueryable<RoomOrder> FindAll()
        {
            return _roomOrderRepository.FindBy(w => w.Deleted == false);
        }

        public RoomOrder FindById(int id)
        {
            return _roomOrderRepository.FindBy(w => w.Id == id && w.Deleted == false).SingleOrDefault();
        }

        public void UpdateStatus(int id, RoomOrderStatus status)
        {
            var entity = FindById(id);
            entity.Status = status;
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

        public void Create(RoomOrder entity)
        {
            _roomOrderRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Edit(RoomOrder entity)
        {
            _roomOrderRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _roomOrderRepository.Delete(w => w.Id == id);
            _unitOfWork.Commit();
        }

        public void Delete(RoomOrder entity)
        {
            _roomOrderRepository.Delete(entity);
            _unitOfWork.Commit();
        }

        #endregion
    }
}