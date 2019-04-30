using System.Collections.Generic;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface IRoomImageMappingService
    {

        IEnumerable<RoomImageMappings> GetRoomImageMappings();
        RoomImageMappings GetRoomImageMappingById(int roomImageMappingId);
        void CreateRoomImageMapping(RoomImageMappings roomImageMapping);
        void EditRoomImageMapping(RoomImageMappings roomImageMappingToEdit);
        void DeleteRoomImageMapping(int roomImageMappingId);
        void SaveRoomImageMapping();
        IEnumerable<ValidationResult> CanAddRoomImageMapping(RoomImageMappings roomImageMapping);

    }
    public class RoomImageMappingService : IRoomImageMappingService
    {
        #region Field
        private readonly IRoomImageMappingRepository _roomImageMappingRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public RoomImageMappingService(IRoomImageMappingRepository roomImageMappingRepository, IUnitOfWork unitOfWork)
        {
            this._roomImageMappingRepository = roomImageMappingRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomImageMappings> GetRoomImageMappings()
        {
            var roomImageMappings = _roomImageMappingRepository.GetAll();
            return roomImageMappings;
        }

        public RoomImageMappings GetRoomImageMappingById(int roomImageMappingId)
        {
            var roomImageMapping = _roomImageMappingRepository.GetById(roomImageMappingId);
            return roomImageMapping;
        }

        public void CreateRoomImageMapping(RoomImageMappings roomImageMapping)
        {
            _roomImageMappingRepository.Add(roomImageMapping);
            SaveRoomImageMapping();
        }

        public void EditRoomImageMapping(RoomImageMappings roomImageMappingToEdit)
        {
            _roomImageMappingRepository.Update(roomImageMappingToEdit);
            SaveRoomImageMapping();
        }

        public void DeleteRoomImageMapping(int roomImageMappingId)
        {
            //Get RoomImageMapping by id.
            var roomImageMapping = _roomImageMappingRepository.GetById(roomImageMappingId);
            if (roomImageMapping != null)
            {
                _roomImageMappingRepository.Delete(roomImageMapping);
                SaveRoomImageMapping();
            }
        }

        public void SaveRoomImageMapping()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomImageMapping(RoomImageMappings roomImageMapping)
        {

            //    yield return new ValidationResult("RoomImageMapping", "ErrorString");
            return null;
        }

        #endregion
    }
}
