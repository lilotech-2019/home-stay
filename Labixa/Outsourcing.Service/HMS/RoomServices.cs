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
        IQueryable<Rooms> FindAll();
        Rooms FindById(int id);
        void Create(Rooms entity);
        void Edit(Rooms entity);
        void Delete(int id);
        void Delete(Rooms entity);
        IQueryable<Rooms> FindSelectList(int? id);
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

        public IQueryable<Rooms> FindSelectList(int? id) {
            var list = _roomRepository.FindBy(r => r.IsDelete == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public IQueryable<Rooms> FindAll()
        {
            var listEntities = _roomRepository.FindBy(w => w.IsDelete == false);
            return listEntities;
        }

        public void Create(Rooms entity)
        {
            _roomRepository.Add(entity);
            commit();
        }

        public void Edit(Rooms entity)
        {
            _roomRepository.Update(entity);
            commit();
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
                entity.IsDelete = true;
                Edit(entity);
            }
        }

        private void commit() {
            _unitOfWork.Commit();
        }

        public Rooms FindById(int id)
        {
            var entity = _roomRepository.FindBy(w => w.IsDelete == false & w.Id == id).SingleOrDefault();
            return entity;
        }
        #endregion
    }
}