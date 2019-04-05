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

        IEnumerable<CategoryProducts> GetProductCategories();
        CategoryProducts GetCategoryProductById(int CategoryProductId);
        void CreateCategoryProduct(CategoryProducts CategoryProduct);
        void EditCategoryProduct(CategoryProducts CategoryProductToEdit);
        void DeleteProductCategories(int CategoryProductId);
        void SaveCategoryProduct();
        IEnumerable<ValidationResult> CanAddCategoryProduct(CategoryProducts CategoryProduct);

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

        public IEnumerable<CategoryProducts> GetProductCategories()
        {
            var CategoryProducts = CategoryProductRepository.GetAll().Where(p => p.Deleted == false);
            return CategoryProducts;
        }

        public CategoryProducts GetCategoryProductById(int CategoryProductId)
        {
            var CategoryProduct = CategoryProductRepository.GetById(CategoryProductId);
            return CategoryProduct;
        }

        public void CreateCategoryProduct(CategoryProducts CategoryProduct)
        {
            CategoryProductRepository.Add(CategoryProduct);
            SaveCategoryProduct();
        }

        public void EditCategoryProduct(CategoryProducts CategoryProductToEdit)
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

        public IEnumerable<ValidationResult> CanAddCategoryProduct(CategoryProducts CategoryProduct)
        {

            //    yield return new ValidationResult("CategoryProduct", "ErrorString");
            return null;
        }

        #endregion
    }
}
