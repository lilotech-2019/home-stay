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

    public interface IRoomService
    {

        IEnumerable<Rooms> GetRooms();
        Rooms GetRoomById(int RoomId);
        void CreateRoom(Rooms Room);
        void EditRoom(Rooms RoomToEdit);
        void DeleteRoom(int RoomId);
        void SaveRoom();
        Rooms GetRoomByUrlName(string urlName);
        IEnumerable<Rooms> Get3RoomShortNews();
        IEnumerable<Rooms> Get3RoomLongNews();

        //IEnumerable<Room> Get4RoomShortHome();
   
        IEnumerable<ValidationResult> CanAddRoom(Rooms Room);

    }
    public class RoomService : IRoomService
    {
        #region Field
        private readonly IRoomRepository RoomRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RoomService(IRoomRepository RoomRepository, IUnitOfWork unitOfWork)
        {
            this.RoomRepository = RoomRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Rooms> GetRooms()
        {
            var Rooms = RoomRepository.GetAll();
            return Rooms;
        }

        public Rooms GetRoomById(int RoomId)
        {
            var Room = RoomRepository.GetById(RoomId);
            return Room;
        }

        public Rooms GetRoomByUrlName( string urlName)
        {
            var Room = RoomRepository.Get(b => b.Slug == urlName);
            return Room;
        }


        public void CreateRoom(Rooms Room)
        {
            RoomRepository.Add(Room);
            SaveRoom();
        }

        public void EditRoom(Rooms RoomToEdit)
        {
            RoomRepository.Update(RoomToEdit);
            SaveRoom();
        }

        public void DeleteRoom(int RoomId)
        {
            //Get Room by id.
            var Room = RoomRepository.GetById(RoomId);
            if (Room != null)
            {
                RoomRepository.Delete(Room);
                SaveRoom();
            }
        }

        public void SaveRoom()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddRoom(Rooms Room)
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
