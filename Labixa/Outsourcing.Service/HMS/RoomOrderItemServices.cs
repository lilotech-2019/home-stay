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

        IEnumerable<RoomOrderItem> GetRoomOrderItems();
        RoomOrderItem GetRoomOrderItemById(int roomOrderItemId);
        void CreateRoomOrderItem(RoomOrderItem roomOrderItem);
        void EditRoomOrderItem(RoomOrderItem roomOrderItemToEdit);
        void DeleteRoomOrderItem(int roomOrderItemId);
        void SaveRoomOrderItem();
        IEnumerable<ValidationResult> CanAddRoomOrderItem(RoomOrderItem roomOrderItem);

    }
    public class RoomOrderItemService : IRoomOrderItemService
    {
        #region Field
        private readonly IRoomOrderItemRepository _roomOrderItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public RoomOrderItemService(IRoomOrderItemRepository roomOrderItemRepository, IUnitOfWork unitOfWork)
        {
            this._roomOrderItemRepository = roomOrderItemRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomOrderItem> GetRoomOrderItems()
        {
            var roomOrderItems = _roomOrderItemRepository.GetAll();
            return roomOrderItems;
        }

        public RoomOrderItem GetRoomOrderItemById(int roomOrderItemId)
        {
            var roomOrderItem = _roomOrderItemRepository.GetById(roomOrderItemId);
            return roomOrderItem;
        }

        public void CreateRoomOrderItem(RoomOrderItem roomOrderItem)
        {
            _roomOrderItemRepository.Add(roomOrderItem);
            SaveRoomOrderItem();
        }

        public void EditRoomOrderItem(RoomOrderItem roomOrderItemToEdit)
        {
            _roomOrderItemRepository.Update(roomOrderItemToEdit);
            SaveRoomOrderItem();
        }

        public void DeleteRoomOrderItem(int roomOrderItemId)
        {
            //Get RoomOrderItem by id.
            var roomOrderItem = _roomOrderItemRepository.GetById(roomOrderItemId);
            if (roomOrderItem != null)
            {
                _roomOrderItemRepository.Delete(roomOrderItem);
                SaveRoomOrderItem();
            }
        }

        public void SaveRoomOrderItem()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomOrderItem(RoomOrderItem roomOrderItem)
        {

            //    yield return new ValidationResult("RoomOrderItem", "ErrorString");
            return null;
        }

        #endregion
    }
}
