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
    public interface IProductAttributeService
    {

        IEnumerable<ProductAttribute> GetProductAttributes();
        ProductAttribute GetProductAttributeById(int productAttributeId);
        void CreateProductAttribute(ProductAttribute productAttribute);
        void EditProductAttribute(ProductAttribute productAttributeToEdit);
        void DeleteProductAttribute(int productAttributeId);
        void SaveProductAttribute();
        IEnumerable<ValidationResult> CanAddProductAttribute(ProductAttribute productAttribute);

    }
    public class ProductAttributeService : IProductAttributeService
    {
        #region Field
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ProductAttributeService(IProductAttributeRepository productAttributeRepository, IUnitOfWork unitOfWork)
        {
            this._productAttributeRepository = productAttributeRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<ProductAttribute> GetProductAttributes()
        {
            var productAttributes = _productAttributeRepository.GetAll();
            return productAttributes;
        }

        public ProductAttribute GetProductAttributeById(int productAttributeId)
        {
            var productAttribute = _productAttributeRepository.GetById(productAttributeId);
            return productAttribute;
        }

        public void CreateProductAttribute(ProductAttribute productAttribute)
        {
            _productAttributeRepository.Add(productAttribute);
            SaveProductAttribute();
        }

        public void EditProductAttribute(ProductAttribute productAttributeToEdit)
        {
            _productAttributeRepository.Update(productAttributeToEdit);
            SaveProductAttribute();
        }

        public void DeleteProductAttribute(int productAttributeId)
        {
            //Get productAttribute by id.
            var productAttribute = _productAttributeRepository.GetById(productAttributeId);
            if (productAttribute != null)
            {
                _productAttributeRepository.Delete(productAttribute);
                SaveProductAttribute();
            }
        }

        public void SaveProductAttribute()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddProductAttribute(ProductAttribute productAttribute)
        {
        
            //    yield return new ValidationResult("ProductAttribute", "ErrorString");
            return null;
        }

        #endregion
    }
}
