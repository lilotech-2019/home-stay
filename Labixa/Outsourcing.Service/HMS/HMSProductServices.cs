using System;
using System.Collections.Generic;
using System.Linq;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;

namespace Outsourcing.Service.HMS
{
    class HmsProductServices
    {
    }
    public interface IHmsProductService
    {

        IEnumerable<HmsProduct> GetHmsProducts();
        HmsProduct GetHmsProductContact();
        IEnumerable<HmsProduct> GetHomePageHmsProducts();
        IEnumerable<HmsProduct> GetHmsProductByCategorySlug(string slug);
        IEnumerable<HmsProduct> GetHmsProductByCategoryId(int id);
        IEnumerable<HmsProduct> Get6HmsProductService();
        IEnumerable<HmsProduct> Get2HmsProductNews();
        IEnumerable<HmsProduct> Get3HmsProductNewsNewest();
        HmsProduct GetHmsProductById(int hmsProductId);
        void CreateHmsProduct(HmsProduct hmsProduct);
        void EditHmsProduct(HmsProduct hmsProductToEdit);
        void DeleteHmsProduct(int hmsProductId);
        void SaveHmsProduct();
        IEnumerable<ValidationResult> CanAddHmsProduct(string hmsProductUrl);

        HmsProduct GetHmsProductByUrlName(string urlName);

        IEnumerable<HmsProduct> GetHmsProductsByCategory(int hmsProductTypeId);

        IEnumerable<HmsProduct> GetStaticPage();
        IEnumerable<HmsProduct> GetNewPost();
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
        public IEnumerable<HmsProduct> GetHmsProducts()
        {
            var hmsProducts = _hmsProductRepository.GetMany(b => !b.Deleted).OrderBy(b => b.Position);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> Get3HmsProductsPosition()
        {
            var hmsProducts = _hmsProductRepository.GetMany(b => !b.Deleted).OrderBy(b => b.Position).Take(3);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> GetHomePageHmsProducts()
        {
            var hmsProducts = _hmsProductRepository.
                GetMany(b => !b.Deleted && b.IsHomePage).
                OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> GetHmsProductByCategoryId(int id)
        {
            var hmsProducts = _hmsProductRepository.GetMany(b => b.CategoryProductId.Equals(id)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> GetHmsProductByCategorySlug(string slug)
        {
            var hmsProducts = _hmsProductRepository.GetMany(b =>b.Slug.Equals(slug)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> GetStaticPage()
        {
            var hmsProducts = _hmsProductRepository.GetMany(b =>!b.Deleted).OrderByDescending(b => b.DateCreated);
            return hmsProducts;
        }

        public HmsProduct GetHmsProductById(int hmsProductId)
        {
            var hmsProduct = _hmsProductRepository.GetById(hmsProductId);
            return hmsProduct;
        }

        public void CreateHmsProduct(HmsProduct hmsProduct)
        {
            _hmsProductRepository.Add(hmsProduct);
            SaveHmsProduct();
        }

        public void EditHmsProduct(HmsProduct hmsProductToEdit)
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

        public HmsProduct GetHmsProductByUrlName(string urlName)
        {
            var hmsProduct = _hmsProductRepository.Get(b => b.Slug == urlName);
            return hmsProduct;
        }

        public IEnumerable<HmsProduct> GetHmsProductsByCategory(int hmsProductTypeId)
        {
            var hmsProducts = this.GetHmsProducts();
            return hmsProducts;
        }

        public IEnumerable<HmsProduct> Get6HmsProductService()
        {
            var hmsProducts = this.GetHmsProducts().Take(6);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> Get2HmsProductNews()
        {
            var hmsProducts = this.GetHmsProducts().OrderBy(p => p.Position).Take(2);
            return hmsProducts;
        }
        public IEnumerable<HmsProduct> Get3HmsProductNewsNewest()
        {
            var hmsProducts = this.GetHmsProducts().OrderBy(p => p.Position).Take(3);
            return hmsProducts;
        }
        #endregion


        public HmsProduct GetHmsProductContact()
        {
            var item = _hmsProductRepository.Get(p => p.Slug.Equals("lien-he"));
            return item;
        }


        public IEnumerable<HmsProduct> GetNewPost()
        {
            return _hmsProductRepository.GetAll().OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
