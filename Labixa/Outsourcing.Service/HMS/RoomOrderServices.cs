using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{

    public interface IRoomOrderService
    {

        IEnumerable<RoomOrders> GetRoomOrders();
        RoomOrders GetRoomOrderById(int RoomOrderId);
        void CreateRoomOrder(RoomOrders RoomOrder);
        void EditRoomOrder(RoomOrders RoomOrderToEdit);
        void DeleteRoomOrder(int RoomOrderId);
        void SaveRoomOrder();
        IEnumerable<ValidationResult> CanAddRoomOrder(RoomOrders RoomOrder);
       
    }
    public class RoomOrderService : IRoomOrderService
    {
        #region Field
        private readonly IRoomOrderRepository RoomOrderRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RoomOrderService(IRoomOrderRepository RoomOrderRepository, IUnitOfWork unitOfWork)
        {
            this.RoomOrderRepository = RoomOrderRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomOrders> GetRoomOrders()
        {
            var RoomOrders = RoomOrderRepository.GetAll();
            return RoomOrders;
        }

        public RoomOrders GetRoomOrderById(int RoomOrderId)
        {
            var RoomOrder = RoomOrderRepository.GetById(RoomOrderId);
            return RoomOrder;
        }

        public void CreateRoomOrder(RoomOrders RoomOrder)
        {
            RoomOrderRepository.Add(RoomOrder);
            SaveRoomOrder();
        }

        public void EditRoomOrder(RoomOrders RoomOrderToEdit)
        {
            RoomOrderRepository.Update(RoomOrderToEdit);
            SaveRoomOrder();
        }

        public void DeleteRoomOrder(int RoomOrderId)
        {
            //Get RoomOrder by id.
            var RoomOrder = RoomOrderRepository.GetById(RoomOrderId);
            if (RoomOrder != null)
            {
                RoomOrderRepository.Delete(RoomOrder);
                SaveRoomOrder();
            }
        }

        public void SaveRoomOrder()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomOrder(RoomOrders RoomOrder)
        {

            //    yield return new ValidationResult("RoomOrder", "ErrorString");
            return null;
        }

        #endregion
    }
}
