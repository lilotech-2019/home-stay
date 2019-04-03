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
    public interface IRoomImageMappingService
    {

        IEnumerable<RoomImageMappings> GetRoomImageMappings();
        RoomImageMappings GetRoomImageMappingById(int RoomImageMappingId);
        void CreateRoomImageMapping(RoomImageMappings RoomImageMapping);
        void EditRoomImageMapping(RoomImageMappings RoomImageMappingToEdit);
        void DeleteRoomImageMapping(int RoomImageMappingId);
        void SaveRoomImageMapping();
        IEnumerable<ValidationResult> CanAddRoomImageMapping(RoomImageMappings RoomImageMapping);

    }
    public class RoomImageMappingService : IRoomImageMappingService
    {
        #region Field
        private readonly IRoomImageMappingRepository RoomImageMappingRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RoomImageMappingService(IRoomImageMappingRepository RoomImageMappingRepository, IUnitOfWork unitOfWork)
        {
            this.RoomImageMappingRepository = RoomImageMappingRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RoomImageMappings> GetRoomImageMappings()
        {
            var RoomImageMappings = RoomImageMappingRepository.GetAll();
            return RoomImageMappings;
        }

        public RoomImageMappings GetRoomImageMappingById(int RoomImageMappingId)
        {
            var RoomImageMapping = RoomImageMappingRepository.GetById(RoomImageMappingId);
            return RoomImageMapping;
        }

        public void CreateRoomImageMapping(RoomImageMappings RoomImageMapping)
        {
            RoomImageMappingRepository.Add(RoomImageMapping);
            SaveRoomImageMapping();
        }

        public void EditRoomImageMapping(RoomImageMappings RoomImageMappingToEdit)
        {
            RoomImageMappingRepository.Update(RoomImageMappingToEdit);
            SaveRoomImageMapping();
        }

        public void DeleteRoomImageMapping(int RoomImageMappingId)
        {
            //Get RoomImageMapping by id.
            var RoomImageMapping = RoomImageMappingRepository.GetById(RoomImageMappingId);
            if (RoomImageMapping != null)
            {
                RoomImageMappingRepository.Delete(RoomImageMapping);
                SaveRoomImageMapping();
            }
        }

        public void SaveRoomImageMapping()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoomImageMapping(RoomImageMappings RoomImageMapping)
        {

            //    yield return new ValidationResult("RoomImageMapping", "ErrorString");
            return null;
        }

        #endregion
    }
}
