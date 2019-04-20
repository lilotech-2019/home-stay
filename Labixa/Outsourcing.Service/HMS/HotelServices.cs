using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Repository.HMS;
using System.Linq;

namespace Outsourcing.Service.HMS
{
    public interface IHotelService
    {
        IQueryable<Hotel> FindAll();
        Hotel FindById(int id);
        void Create(Hotel entity);
        void Edit(Hotel entity);
        void Delete(int id);
        void Delete(Hotel entity);
        IQueryable<Hotel> FindSelectList(int? id);
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
        public IQueryable<Hotel> FindSelectList(int? id)
        {
            var list = _hotelRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public IQueryable<Hotel> FindAll()
        {
            var listEntities = _hotelRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public Hotel FindById(int id)
        {
            var entity = _hotelRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Hotel entity)
        {
            _hotelRepository.Add(entity);
            Commit();
        }

        public void Edit(Hotel entity)
        {
            _hotelRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Hotel entity)
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