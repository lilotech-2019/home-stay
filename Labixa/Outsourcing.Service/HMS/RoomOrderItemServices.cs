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
    public interface IRoomOrderItemService
    {

        IEnumerable<RoomOrderItems> GetRoomOrderItems();
        RoomOrderItems GetRoomOrderItemById(int RoomOrderItemId);
        void CreateRoomOrderItem(RoomOrderItems RoomOrderItem);
        void EditRoomOrderItem(RoomOrderItems RoomOrderItemToEdit);
        void DeleteRoomOrderItem(int RoomOrderItemId);
        void SaveRoomOrderItem();
        IEnumerable<ValidationResult> CanAddRoomOrderItem(RoomOrderItems RoomOrderItem);

    }
    public class RoomOrderItemService : IRoomOrderItemService
    {
        #region Field
        private readonly IRoomOrderItemRepository RoomOrderItemRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RoomOrderItemService(IRoomOrderItemRepository RoomOrderItemRepository, IUnitOfWork unitOfWork)
        {
            this.RoomOrderItemRepository = RoomOrderItemRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomOrderItems> GetRoomOrderItems()
        {
            var RoomOrderItems = RoomOrderItemRepository.GetAll();
            return RoomOrderItems;
        }

        public RoomOrderItems GetRoomOrderItemById(int RoomOrderItemId)
        {
            var RoomOrderItem = RoomOrderItemRepository.GetById(RoomOrderItemId);
            return RoomOrderItem;
        }

        public void CreateRoomOrderItem(RoomOrderItems RoomOrderItem)
        {
            RoomOrderItemRepository.Add(RoomOrderItem);
            SaveRoomOrderItem();
        }

        public void EditRoomOrderItem(RoomOrderItems RoomOrderItemToEdit)
        {
            RoomOrderItemRepository.Update(RoomOrderItemToEdit);
            SaveRoomOrderItem();
        }

        public void DeleteRoomOrderItem(int RoomOrderItemId)
        {
            //Get RoomOrderItem by id.
            var RoomOrderItem = RoomOrderItemRepository.GetById(RoomOrderItemId);
            if (RoomOrderItem != null)
            {
                RoomOrderItemRepository.Delete(RoomOrderItem);
                SaveRoomOrderItem();
            }
        }

        public void SaveRoomOrderItem()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomOrderItem(RoomOrderItems RoomOrderItem)
        {

            //    yield return new ValidationResult("RoomOrderItem", "ErrorString");
            return null;
        }

        #endregion
    }
}
