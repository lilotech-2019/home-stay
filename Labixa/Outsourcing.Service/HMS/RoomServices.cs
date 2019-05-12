using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface IRoomService
    {
        IQueryable<Room> FindAll();
        Room FindById(int id);
        void Create(Room entity);
        void Edit(Room entity);
        void Delete(int id);
        void Delete(Room entity);
        IQueryable<Room> FindSelectList(int? id);
        IQueryable<Room> FindByType(RoomType type);
        Room FindByIdAndSlug(int id, string slug);
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
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region BaseMethod

        public IQueryable<Room> FindSelectList(int? id)
        {
            var list = _roomRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public IQueryable<Room> FindAll()
        {
            var listEntities = _roomRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public void Create(Room entity)
        {
            _roomRepository.Add(entity);
            Commit();
        }

        public void Edit(Room entity)
        {
            _roomRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public void Delete(Room entity)
        {
            if (entity != null)
            {
                entity.Deleted = true;
                Edit(entity);
            }
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public Room FindById(int id)
        {
            var entity = _roomRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public IQueryable<Room> FindByType(RoomType type)
        {
            var result = _roomRepository.FindBy(w => w.Deleted == false & w.Type == type & w.Status == true);
            return result;
        }

        public Room FindByIdAndSlug(int id, string slug)
        {
            return _roomRepository.FindBy(w => w.Deleted == false & w.Id == id & w.Slug == slug & w.Status == true).SingleOrDefault();
        }

        #endregion
    }
}