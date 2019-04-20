using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Repository.HMS;
using System.Linq;

namespace Outsourcing.Service.HMS
{

    public interface ICategoryHotelService
    {

        IQueryable<CategoryHotels> FindAll();
        CategoryHotels FindById(int id);
        void Create(CategoryHotels entity);
        void Edit(CategoryHotels entity);
        void Delete(int id);
        void Delete(CategoryHotels entity);
        //object GetProductCategories();
    }
    public class CategoryHotelService : ICategoryHotelService
    {
        #region Field
        private readonly ICategoryHotelRepository _categoryHotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CategoryHotelService(ICategoryHotelRepository categoryHotelRepository, IUnitOfWork unitOfWork)
        {
            _categoryHotelRepository = categoryHotelRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IQueryable<CategoryHotels> FindAll()
        {
            var listEntities = _categoryHotelRepository.FindBy(w => w.IsDelete == false);
            return listEntities;
        }

        public CategoryHotels FindById(int id)
        {
            var entity = _categoryHotelRepository.FindBy(w => w.IsDelete == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(CategoryHotels entity)
        {
            _categoryHotelRepository.Add(entity);
            commit();
        }

        public void Edit(CategoryHotels entity)
        {
            _categoryHotelRepository.Update(entity);
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

        public void Delete(CategoryHotels entity)
        {
            if (entity != null)
            {
                entity.IsDelete = true;
                Edit(entity);
            }
        }
        #endregion
    }
}
