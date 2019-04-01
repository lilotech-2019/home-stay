using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
 
   public interface ICategoryHotelService
    {

        IEnumerable<CategoryHotel> GetProductCategories();
        CategoryHotel GetCategoryHotelById(int CategoryHotelId);
        void CreateCategoryHotel(CategoryHotel CategoryHotel);
        void EditCategoryHotel(CategoryHotel CategoryHotelToEdit);
        void DeleteProductCategories(int CategoryHotelId);
        void SaveCategoryHotel();
        IEnumerable<ValidationResult> CanAddCategoryHotel(CategoryHotel CategoryHotel);

    }
    public class CategoryHotelService : ICategoryHotelService
    {
        #region Field
        private readonly ICategoryHotelRepository CategoryHotelRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CategoryHotelService(ICategoryHotelRepository CategoryHotelRepository, IUnitOfWork unitOfWork)
        {
            this.CategoryHotelRepository = CategoryHotelRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CategoryHotel> GetProductCategories()
        {
            var CategoryHotels = CategoryHotelRepository.GetAll().Where(p => p.Status == true);
            return CategoryHotels;
        }

        public CategoryHotel GetCategoryHotelById(int CategoryHotelId)
        {
            var CategoryHotel = CategoryHotelRepository.GetById(CategoryHotelId);
            return CategoryHotel;
        }

        public void CreateCategoryHotel(CategoryHotel CategoryHotel)
        {
            CategoryHotelRepository.Add(CategoryHotel);
            SaveCategoryHotel();
        }

        public void EditCategoryHotel(CategoryHotel CategoryHotelToEdit)
        {
            CategoryHotelRepository.Update(CategoryHotelToEdit);
            SaveCategoryHotel();
        }

        public void DeleteProductCategories(int CategoryHotelId)
        {
            //Get CategoryHotel by id.
            var CategoryHotel = CategoryHotelRepository.GetById(CategoryHotelId);
            if (CategoryHotel != null)
            {
                CategoryHotelRepository.Delete(CategoryHotel);
                SaveCategoryHotel();
            }
        }

        public void SaveCategoryHotel()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCategoryHotel(CategoryHotel CategoryHotel)
        {

            //    yield return new ValidationResult("CategoryHotel", "ErrorString");
            return null;
        }

        #endregion
    }
}
