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
        RoomImages GetRoomImageById(int RoomImageId);
        void CreateRoomImage(RoomImages RoomImage);
        void EditRoomImage(RoomImages RoomImageToEdit);
        void DeleteRoomImage(int RoomImageId);
        void SaveRoomImage();
        IEnumerable<ValidationResult> CanAddRoomImage(RoomImages RoomImage);

    }
    public class RoomImageService : IRoomImageService
    {
        #region Field
        private readonly IRoomImageRepository RoomImageRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RoomImageService(IRoomImageRepository RoomImageRepository, IUnitOfWork unitOfWork)
        {
            this.RoomImageRepository = RoomImageRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomImages> GetRoomImages()
        {
            var RoomImages = RoomImageRepository.GetAll();
            return RoomImages;
        }

        public RoomImages GetRoomImageById(int RoomImageId)
        {
            var RoomImage = RoomImageRepository.GetById(RoomImageId);
            return RoomImage;
        }

        public void CreateRoomImage(RoomImages RoomImage)
        {
            RoomImageRepository.Add(RoomImage);
            SaveRoomImage();
        }

        public void EditRoomImage(RoomImages RoomImageToEdit)
        {
            RoomImageRepository.Update(RoomImageToEdit);
            SaveRoomImage();
        }

        public void DeleteRoomImage(int RoomImageId)
        {
            //Get RoomImage by id.
            var RoomImage = RoomImageRepository.GetById(RoomImageId);
            if (RoomImage != null)
            {
                RoomImageRepository.Delete(RoomImage);
                SaveRoomImage();
            }
        }

        public void SaveRoomImage()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomImage(RoomImages RoomImage)
        {

            //    yield return new ValidationResult("RoomImage", "ErrorString");
            return null;
        }

        #endregion
    }
}
