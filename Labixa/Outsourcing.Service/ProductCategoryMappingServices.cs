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
        ProductCategoryMapping GetProductCategoryMappingById(int ProductCategoryMappingId);
        void CreateProductCategoryMapping(ProductCategoryMapping ProductCategoryMapping);
        void EditProductCategoryMapping(ProductCategoryMapping ProductCategoryMappingToEdit);
        void DeleteProductCategoryMapping(int ProductCategoryMappingId);
        void SaveProductCategoryMapping();
        IEnumerable<ValidationResult> CanAddProductCategoryMapping(ProductCategoryMapping ProductCategoryMapping);

    }
    public class ProductCategoryMappingService : IProductCategoryMappingService
    {
        #region Field
        private readonly IProductCategoryMappingRepository ProductCategoryMappingRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ProductCategoryMappingService(IProductCategoryMappingRepository ProductCategoryMappingRepository, IUnitOfWork unitOfWork)
        {
            this.ProductCategoryMappingRepository = ProductCategoryMappingRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<ProductCategoryMapping> GetProductCategoryMappings()
        {
            var ProductCategoryMappings = ProductCategoryMappingRepository.GetAll();
            return ProductCategoryMappings;
        }

        public ProductCategoryMapping GetProductCategoryMappingById(int ProductCategoryMappingId)
        {
            var ProductCategoryMapping = ProductCategoryMappingRepository.GetById(ProductCategoryMappingId);
            return ProductCategoryMapping;
        }

        public void CreateProductCategoryMapping(ProductCategoryMapping ProductCategoryMapping)
        {
            ProductCategoryMappingRepository.Add(ProductCategoryMapping);
            SaveProductCategoryMapping();
        }

        public void EditProductCategoryMapping(ProductCategoryMapping ProductCategoryMappingToEdit)
        {
            ProductCategoryMappingRepository.Update(ProductCategoryMappingToEdit);
            SaveProductCategoryMapping();
        }

        public void DeleteProductCategoryMapping(int ProductCategoryMappingId)
        {
            //Get ProductCategoryMapping by id.
            var ProductCategoryMapping = ProductCategoryMappingRepository.GetById(ProductCategoryMappingId);
            if (ProductCategoryMapping != null)
            {
                ProductCategoryMappingRepository.Delete(ProductCategoryMapping);
                SaveProductCategoryMapping();
            }
        }

        public void SaveProductCategoryMapping()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddProductCategoryMapping(ProductCategoryMapping ProductCategoryMapping)
        {

            //    yield return new ValidationResult("ProductCategoryMapping", "ErrorString");
            return null;
        }

        #endregion
    }
}
