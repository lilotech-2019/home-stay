using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    class HmsProductServices
    {
    }
    public interface IHmsProductService
    {

        IEnumerable<HMSProduct> GetHmsProducts();
        HMSProduct GetHmsProductContact();
        IEnumerable<HMSProduct> GetHomePageHmsProducts();
        IEnumerable<HMSProduct> GetHmsProductByCategorySlug(string slug);
        IEnumerable<HMSProduct> GetHmsProductByCategoryId(int id);
        IEnumerable<HMSProduct> Get6HmsProductService();
        IEnumerable<HMSProduct> Get2HmsProductNews();
        IEnumerable<HMSProduct> Get3HmsProductNewsNewest();
        HMSProduct GetHmsProductById(int hmsProductId);
        void CreateHmsProduct(HMSProduct hmsProduct);
        void EditHmsProduct(HMSProduct hmsProductToEdit);
        void DeleteHmsProduct(int hmsProductId);
        void SaveHmsProduct();
        IEnumerable<ValidationResult> CanAddHmsProduct(string hmsProductUrl);

        HMSProduct GetHmsProductByUrlName(string urlName);

        IEnumerable<HMSProduct> GetHmsProductsByCategory(int hmsProductTypeId);

        IEnumerable<HMSProduct> GetStaticPage();
        IEnumerable<HMSProduct> GetNewPost();
    }
    public class HmsProductService : IHmsProductService
    {
        #region Field
        private readonly Data.Repository.HMS.IProductRepository _hmsProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public HmsProductService(Data.Repository.HMS.IProductRepository hmsProductRepository, IUnitOfWork unitOfWork)
        {
            this._hmsProductRepository = hmsProductRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for IHMSProductService
        public IEnumerable<HMSProduct> GetHmsProducts()
        {
            var hmsProducts = _hmsProductRepository.GetMany(b => !b.Deleted).OrderBy(b => b.Position);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> Get3HmsProductsPosition()
        {
            var hmsProducts = _hmsProductRepository.GetMany(b => !b.Deleted).OrderBy(b => b.Position).Take(3);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> GetHomePageHmsProducts()
        {
            var hmsProducts = _hmsProductRepository.
                GetMany(b => !b.Deleted && b.IsHomePage).
                OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> GetHmsProductByCategoryId(int id)
        {
            var hmsProducts = _hmsProductRepository.GetMany(b => b.CategoryProductId.Equals(id)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> GetHmsProductByCategorySlug(string slug)
        {
            var hmsProducts = _hmsProductRepository.GetMany(b =>b.Slug.Equals(slug)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> GetStaticPage()
        {
            var hmsProducts = _hmsProductRepository.GetMany(b =>!b.Deleted).OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }

        public HMSProduct GetHmsProductById(int hmsProductId)
        {
            var hmsProduct = _hmsProductRepository.GetById(hmsProductId);
            return hmsProduct;
        }

        public void CreateHmsProduct(HMSProduct hmsProduct)
        {
            _hmsProductRepository.Add(hmsProduct);
            SaveHmsProduct();
        }

        public void EditHmsProduct(HMSProduct hmsProductToEdit)
        {
            hmsProductToEdit.LastEditedTime = DateTime.Now;
            _hmsProductRepository.Update(hmsProductToEdit);
            SaveHmsProduct();
        }

        public void DeleteHmsProduct(int hmsProductId)
        {
            //Get HMSProduct by id.
            var hmsProduct = _hmsProductRepository.GetById(hmsProductId);
            if (hmsProduct != null)
            {
                hmsProduct.Deleted = true;
                _hmsProductRepository.Update(hmsProduct);
                SaveHmsProduct();
            }
        }

        public void SaveHmsProduct()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddHmsProduct(string slug)
        {
            //Get HMSProduct by url.
            var hmsProduct = _hmsProductRepository.Get(b => b.Slug.Equals(slug));
            //Check if slug is exist
            //if (HMSProduct != null)
            //{
            //    yield return new ValidationResult("HMSProduct", Resources.HMSProductExist);
            //}
            return null;
        }

        public HMSProduct GetHmsProductByUrlName(string urlName)
        {
            var hmsProduct = _hmsProductRepository.Get(b => b.Slug == urlName);
            return hmsProduct;
        }

        public IEnumerable<HMSProduct> GetHmsProductsByCategory(int hmsProductTypeId)
        {
            var hmsProducts = this.GetHmsProducts();
            return hmsProducts;
        }

        public IEnumerable<HMSProduct> Get6HmsProductService()
        {
            var hmsProducts = this.GetHmsProducts().Take(6);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> Get2HmsProductNews()
        {
            var hmsProducts = this.GetHmsProducts().OrderBy(p => p.Position).Take(2);
            return hmsProducts;
        }
        public IEnumerable<HMSProduct> Get3HmsProductNewsNewest()
        {
            var hmsProducts = this.GetHmsProducts().OrderBy(p => p.Position).Take(3);
            return hmsProducts;
        }
        #endregion


        public HMSProduct GetHmsProductContact()
        {
            var item = _hmsProductRepository.Get(p => p.Slug.Equals("lien-he"));
            return item;
        }


        public IEnumerable<HMSProduct> GetNewPost()
        {
            return _hmsProductRepository.GetAll().OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
