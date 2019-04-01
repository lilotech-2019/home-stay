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

        IEnumerable<RoomImage> GetRoomImages();
        RoomImage GetRoomImageById(int RoomImageId);
        void CreateRoomImage(RoomImage RoomImage);
        void EditRoomImage(RoomImage RoomImageToEdit);
        void DeleteRoomImage(int RoomImageId);
        void SaveRoomImage();
        IEnumerable<ValidationResult> CanAddRoomImage(RoomImage RoomImage);

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

        public IEnumerable<RoomImage> GetRoomImages()
        {
            var RoomImages = RoomImageRepository.GetAll();
            return RoomImages;
        }

        public RoomImage GetRoomImageById(int RoomImageId)
        {
            var RoomImage = RoomImageRepository.GetById(RoomImageId);
            return RoomImage;
        }

        public void CreateRoomImage(RoomImage RoomImage)
        {
            RoomImageRepository.Add(RoomImage);
            SaveRoomImage();
        }

        public void EditRoomImage(RoomImage RoomImageToEdit)
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

        public IEnumerable<ValidationResult> CanAddRoomImage(RoomImage RoomImage)
        {

            //    yield return new ValidationResult("RoomImage", "ErrorString");
            return null;
        }

        #endregion
    }
}
