using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
namespace Outsourcing.Service
{

    public interface IProductCategoryMappingService
    {

        IEnumerable<ProductCategoryMapping> GetProductCategoryMappings();
        ProductCategoryMapping GetProductCategoryMappingById(int productCategoryMappingId);
        void CreateProductCategoryMapping(ProductCategoryMapping productCategoryMapping);
        void EditProductCategoryMapping(ProductCategoryMapping productCategoryMappingToEdit);
        void DeleteProductCategoryMapping(int productCategoryMappingId);
        void SaveProductCategoryMapping();
        IEnumerable<ValidationResult> CanAddProductCategoryMapping(ProductCategoryMapping productCategoryMapping);

    }
    public class ProductCategoryMappingService : IProductCategoryMappingService
    {
        #region Field
        private readonly IProductCategoryMappingRepository _productCategoryMappingRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ProductCategoryMappingService(IProductCategoryMappingRepository productCategoryMappingRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryMappingRepository = productCategoryMappingRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<ProductCategoryMapping> GetProductCategoryMappings()
        {
            var productCategoryMappings = _productCategoryMappingRepository.GetAll();
            return productCategoryMappings;
        }

        public ProductCategoryMapping GetProductCategoryMappingById(int productCategoryMappingId)
        {
            var productCategoryMapping = _productCategoryMappingRepository.GetById(productCategoryMappingId);
            return productCategoryMapping;
        }

        public void CreateProductCategoryMapping(ProductCategoryMapping productCategoryMapping)
        {
            _productCategoryMappingRepository.Add(productCategoryMapping);
            SaveProductCategoryMapping();
        }

        public void EditProductCategoryMapping(ProductCategoryMapping productCategoryMappingToEdit)
        {
            _productCategoryMappingRepository.Update(productCategoryMappingToEdit);
            SaveProductCategoryMapping();
        }

        public void DeleteProductCategoryMapping(int productCategoryMappingId)
        {
            //Get ProductCategoryMapping by id.
            var productCategoryMapping = _productCategoryMappingRepository.GetById(productCategoryMappingId);
            if (productCategoryMapping != null)
            {
                _productCategoryMappingRepository.Delete(productCategoryMapping);
                SaveProductCategoryMapping();
            }
        }

        public void SaveProductCategoryMapping()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddProductCategoryMapping(ProductCategoryMapping productCategoryMapping)
        {

            //    yield return new ValidationResult("ProductCategoryMapping", "ErrorString");
            return null;
        }

        #endregion
    }
}
