using System.Collections.Generic;
using System.Linq;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface IRoomService
    {
        IEnumerable<Rooms> GetRooms();
        Rooms FindById(int id);
        void CreateRoom(Rooms room);
        void EditRoom(Rooms roomToEdit);
        void DeleteRoom(int roomId);
        void SaveRoom();
        Rooms GetRoomByUrlName(string urlName);
        IEnumerable<Rooms> Get3RoomShortNews();
        IEnumerable<Rooms> Get3RoomLongNews();

        //IEnumerable<Room> Get4RoomShortHome();

        IEnumerable<ValidationResult> CanAddRoom(Rooms room);
    }

    public class RoomService : IRoomService
    {
        #region Field

        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            this._roomRepository = roomRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region BaseMethod

        public IEnumerable<Rooms> GetRooms()
        {
            return _roomRepository.FindBy();
        }

        public Rooms FindById(int id)
        {
            return _roomRepository.FindBy(w => w.Id == id).SingleOrDefault();
        }

        public Rooms GetRoomByUrlName(string urlName)
        {
            var room = _roomRepository.FindBy(b => b.Slug == urlName).SingleOrDefault();
            return room;
        }


        public void CreateRoom(Rooms room)
        {
            _roomRepository.Add(room);
            SaveRoom();
        }

        public void EditRoom(Rooms roomToEdit)
        {
            _roomRepository.Update(roomToEdit);
            SaveRoom();
        }

        public void DeleteRoom(int roomId)
        {
            //Get Room by id.
            var room = _roomRepository.FindBy(w => w.Id == roomId).SingleOrDefault();
            if (room != null)
            {
                _roomRepository.Delete(room);
                SaveRoom();
            }
        }

        public void SaveRoom()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoom(Rooms room)
        {
            //    yield return new ValidationResult("Room", "ErrorString");
            return null;
        }

        public IEnumerable<Rooms> Get3RoomShortNews()
        {
            var blogs = this.GetRooms().Where(p => p.Hotel.Layout == 0).OrderBy(p => p.Layout == 0).Take(3);
            return blogs;
        }

        public IEnumerable<Rooms> Get3RoomLongNews()
        {
            var blogs = this.GetRooms().Where(p => p.Hotel.Layout == 2).OrderBy(p => p.Layout == 1).Take(3);
            return blogs;
        }

        #endregion
    }
}