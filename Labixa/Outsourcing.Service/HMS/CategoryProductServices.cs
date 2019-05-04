using System.Collections.Generic;
using System.Linq;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface ICategoryProductService
    {

        IEnumerable<CategoryProducts> GetProductCategories();
        CategoryProducts GetCategoryProductById(int categoryProductId);
        void CreateCategoryProduct(CategoryProducts categoryProduct);
        void EditCategoryProduct(CategoryProducts categoryProductToEdit);
        void DeleteProductCategories(int categoryProductId);
        void SaveCategoryProduct();
        IEnumerable<ValidationResult> CanAddCategoryProduct(CategoryProducts categoryProduct);

    }
    public class CategoryProductService : ICategoryProductService
    {
        #region Field
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CategoryProductService(ICategoryProductRepository categoryProductRepository, IUnitOfWork unitOfWork)
        {
            _categoryProductRepository = categoryProductRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CategoryProducts> GetProductCategories()
        {
            var categoryProducts = _categoryProductRepository.FindBy();
            return categoryProducts;
        }

        public CategoryProducts GetCategoryProductById(int categoryProductId)
        {
            var categoryProduct = _categoryProductRepository.FindBy(w => w.Id == categoryProductId);
            return categoryProduct.FirstOrDefault();
        }

        public void CreateCategoryProduct(CategoryProducts categoryProduct)
        {
            _categoryProductRepository.Add(categoryProduct);
            SaveCategoryProduct();
        }

        public void EditCategoryProduct(CategoryProducts categoryProductToEdit)
        {
            _categoryProductRepository.Update(categoryProductToEdit);
            SaveCategoryProduct();
        }

        public void DeleteProductCategories(int categoryProductId)
        {
            //Get CategoryProduct by id.
            var categoryProduct = GetCategoryProductById(categoryProductId);
            if (categoryProduct != null)
            {
                _categoryProductRepository.Delete(categoryProduct);
                SaveCategoryProduct();
            }
        }

        public void SaveCategoryProduct()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCategoryProduct(CategoryProducts categoryProduct)
        {

            //    yield return new ValidationResult("CategoryProduct", "ErrorString");
            return null;
        }

        #endregion
    }
}
