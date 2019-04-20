using System.Linq;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface IRoomService
    {
        IQueryable<Rooms> FindAll();
        Rooms FindById(int id);
        void Create(Rooms entity);
        void Edit(Rooms entity);
        void Delete(int id);
        void Delete(Rooms entity);
        IQueryable<Rooms> FindSelectList(int? id);
        IQueryable<Rooms> FindByType(RoomType type);
        Rooms FindByIdAndSlug(int id, string slug);
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

        public IQueryable<Rooms> FindSelectList(int? id)
        {
            var list = _roomRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public IQueryable<Rooms> FindAll()
        {
            var listEntities = _roomRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public void Create(Rooms entity)
        {
            _roomRepository.Add(entity);
            Commit();
        }

        public void Edit(Rooms entity)
        {
            _roomRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public void Delete(Rooms entity)
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

        public Rooms FindById(int id)
        {
            var entity = _roomRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public IQueryable<Rooms> FindByType(RoomType type)
        {
            var result = _roomRepository.FindBy(w => w.Deleted == false & w.Type == type);
            return result;
        }

        public Rooms FindByIdAndSlug(int id, string slug)
        {
            return _roomRepository.FindBy(w => w.Deleted == false & w.Id == id & w.Slug == slug).SingleOrDefault();
        }

        #endregion
    }
}