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
    public interface ICategoryProductService
    {

        IEnumerable<CategoryProduct> GetProductCategories();
        CategoryProduct GetCategoryProductById(int CategoryProductId);
        void CreateCategoryProduct(CategoryProduct CategoryProduct);
        void EditCategoryProduct(CategoryProduct CategoryProductToEdit);
        void DeleteProductCategories(int CategoryProductId);
        void SaveCategoryProduct();
        IEnumerable<ValidationResult> CanAddCategoryProduct(CategoryProduct CategoryProduct);

    }
    public class CategoryProductService : ICategoryProductService
    {
        #region Field
        private readonly ICategoryProductRepository CategoryProductRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CategoryProductService(ICategoryProductRepository CategoryProductRepository, IUnitOfWork unitOfWork)
        {
            this.CategoryProductRepository = CategoryProductRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CategoryProduct> GetProductCategories()
        {
            var CategoryProducts = CategoryProductRepository.GetAll().Where(p => p.Deleted == false);
            return CategoryProducts;
        }

        public CategoryProduct GetCategoryProductById(int CategoryProductId)
        {
            var CategoryProduct = CategoryProductRepository.GetById(CategoryProductId);
            return CategoryProduct;
        }

        public void CreateCategoryProduct(CategoryProduct CategoryProduct)
        {
            CategoryProductRepository.Add(CategoryProduct);
            SaveCategoryProduct();
        }

        public void EditCategoryProduct(CategoryProduct CategoryProductToEdit)
        {
            CategoryProductRepository.Update(CategoryProductToEdit);
            SaveCategoryProduct();
        }

        public void DeleteProductCategories(int CategoryProductId)
        {
            //Get CategoryProduct by id.
            var CategoryProduct = CategoryProductRepository.GetById(CategoryProductId);
            if (CategoryProduct != null)
            {
                CategoryProductRepository.Delete(CategoryProduct);
                SaveCategoryProduct();
            }
        }

        public void SaveCategoryProduct()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCategoryProduct(CategoryProduct CategoryProduct)
        {

            //    yield return new ValidationResult("CategoryProduct", "ErrorString");
            return null;
        }

        #endregion
    }
}
