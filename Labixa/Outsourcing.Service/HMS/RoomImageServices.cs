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
   
    public interface IRoomImageService
    {

        IEnumerable<RoomImages> GetRoomImages();
        RoomImages GetRoomImageById(int roomImageId);
        void CreateRoomImage(RoomImages roomImage);
        void EditRoomImage(RoomImages roomImageToEdit);
        void DeleteRoomImage(int roomImageId);
        void SaveRoomImage();
        IEnumerable<ValidationResult> CanAddRoomImage(RoomImages roomImage);

    }
    public class RoomImageService : IRoomImageService
    {
        #region Field
        private readonly IRoomImageRepository _roomImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public RoomImageService(IRoomImageRepository roomImageRepository, IUnitOfWork unitOfWork)
        {
            this._roomImageRepository = roomImageRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomImages> GetRoomImages()
        {
            var roomImages = _roomImageRepository.GetAll();
            return roomImages;
        }

        public RoomImages GetRoomImageById(int roomImageId)
        {
            var roomImage = _roomImageRepository.GetById(roomImageId);
            return roomImage;
        }

        public void CreateRoomImage(RoomImages roomImage)
        {
            _roomImageRepository.Add(roomImage);
            SaveRoomImage();
        }

        public void EditRoomImage(RoomImages roomImageToEdit)
        {
            _roomImageRepository.Update(roomImageToEdit);
            SaveRoomImage();
        }

        public void DeleteRoomImage(int roomImageId)
        {
            //Get RoomImage by id.
            var roomImage = _roomImageRepository.GetById(roomImageId);
            if (roomImage != null)
            {
                _roomImageRepository.Delete(roomImage);
                SaveRoomImage();
            }
        }

        public void SaveRoomImage()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomImage(RoomImages roomImage)
        {

            //    yield return new ValidationResult("RoomImage", "ErrorString");
            return null;
        }

        #endregion
    }
}
