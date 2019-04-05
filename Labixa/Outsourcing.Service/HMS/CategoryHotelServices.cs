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

        IEnumerable<CategoryHotels> GetProductCategories();
        CategoryHotels GetCategoryHotelById(int CategoryHotelId);
        void CreateCategoryHotel(CategoryHotels CategoryHotel);
        void EditCategoryHotel(CategoryHotels CategoryHotelToEdit);
        void DeleteProductCategories(int CategoryHotelId);
        void SaveCategoryHotel();
        IEnumerable<ValidationResult> CanAddCategoryHotel(CategoryHotels CategoryHotel);

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

        public IEnumerable<CategoryHotels> GetProductCategories()
        {
            var CategoryHotels = CategoryHotelRepository.GetAll().Where(p => p.Status == true);
            return CategoryHotels;
        }

        public CategoryHotels GetCategoryHotelById(int CategoryHotelId)
        {
            var CategoryHotel = CategoryHotelRepository.GetById(CategoryHotelId);
            return CategoryHotel;
        }

        public void CreateCategoryHotel(CategoryHotels CategoryHotel)
        {
            CategoryHotelRepository.Add(CategoryHotel);
            SaveCategoryHotel();
        }

        public void EditCategoryHotel(CategoryHotels CategoryHotelToEdit)
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

        public IEnumerable<ValidationResult> CanAddCategoryHotel(CategoryHotels CategoryHotel)
        {

            //    yield return new ValidationResult("CategoryHotel", "ErrorString");
            return null;
        }

        #endregion
    }
}
