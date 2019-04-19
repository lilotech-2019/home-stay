using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IProductCategoryService
    {

        IEnumerable<ProductCategory> GetProductCategories();
        ProductCategory GetProductCategoryById(int productCategoryId);
        void CreateProductCategory(ProductCategory productCategory);
        void EditProductCategory(ProductCategory productCategoryToEdit);
        void DeleteProductCategories(int productCategoryId);
        void SaveProductCategory();
        IEnumerable<ValidationResult> CanAddProductCategory(ProductCategory productCategory);

    }
    public class ProductCategoryService : IProductCategoryService
    {
        #region Field
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            var productCategorys = _productCategoryRepository.GetAll().Where(p=>p.Deleted==false);
            return productCategorys;
        }

        public ProductCategory GetProductCategoryById(int productCategoryId)
        {
            var productCategory = _productCategoryRepository.GetById(productCategoryId);
            return productCategory;
        }

        public void CreateProductCategory(ProductCategory productCategory)
        {
            _productCategoryRepository.Add(productCategory);
            SaveProductCategory();
        }

        public void EditProductCategory(ProductCategory productCategoryToEdit)
        {
            _productCategoryRepository.Update(productCategoryToEdit);
            SaveProductCategory();
        }

        public void DeleteProductCategories(int productCategoryId)
        {
            //Get productCategory by id.
            var productCategory = _productCategoryRepository.GetById(productCategoryId);
            if (productCategory != null)
            {
                _productCategoryRepository.Delete(productCategory);
                SaveProductCategory();
            }
        }

        public void SaveProductCategory()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddProductCategory(ProductCategory productCategory)
        {
        
            //    yield return new ValidationResult("ProductCategory", "ErrorString");
            return null;
        }

        #endregion
    }
}
