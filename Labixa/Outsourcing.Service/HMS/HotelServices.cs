using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Repository.HMS;
using System.Linq;

namespace Outsourcing.Service.HMS
{
    public interface IHotelService
    {
        IQueryable<Hotels> FindAll();
        Hotels FindById(int id);
        void Create(Hotels entity);
        void Edit(Hotels entity);
        void Delete(int id);
        void Delete(Hotels entity);
        IQueryable<Hotels> FindSelectList(int? id);
    }

    public class HotelService : IHotelService
    {
        #region Field

        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public HotelService(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
        {
            this._hotelRepository = hotelRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Implementation for IHotelService
        public IQueryable<Hotels> FindSelectList(int? id)
        {
            var list = _hotelRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public IQueryable<Hotels> FindAll()
        {
            var listEntities = _hotelRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public Hotels FindById(int id)
        {
            var entity = _hotelRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Hotels entity)
        {
            _hotelRepository.Add(entity);
            commit();
        }

        public void Edit(Hotels entity)
        {
            _hotelRepository.Update(entity);
            commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        private void commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Hotels entity)
        {
            if (entity != null)
            {
                entity.Deleted = true;
                Edit(entity);
            }
        }
        #endregion
    }
}