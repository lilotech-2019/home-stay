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
    class HMSProductServices
    {
    }
    public interface IHMSProductService
    {

        IEnumerable<HMSProduct> GetHMSProducts();
        HMSProduct GetHMSProductContact();
        IEnumerable<HMSProduct> GetHomePageHMSProducts();
        IEnumerable<HMSProduct> GetHMSProductByCategorySlug(string slug);
        IEnumerable<HMSProduct> GetHMSProductByCategoryId(int id);
        IEnumerable<HMSProduct> Get6HMSProductService();
        IEnumerable<HMSProduct> Get2HMSProductNews();
        IEnumerable<HMSProduct> Get3HMSProductNewsNewest();
        HMSProduct GetHMSProductById(int HMSProductId);
        void CreateHMSProduct(HMSProduct HMSProduct);
        void EditHMSProduct(HMSProduct HMSProductToEdit);
        void DeleteHMSProduct(int HMSProductId);
        void SaveHMSProduct();
        IEnumerable<ValidationResult> CanAddHMSProduct(string HMSProductUrl);

        HMSProduct GetHMSProductByUrlName(string urlName);

        IEnumerable<HMSProduct> GetHMSProductsByCategory(int HMSProductTypeId);

        IEnumerable<HMSProduct> GetStaticPage();
        IEnumerable<HMSProduct> GetNewPost();
    }
    public class HMSProductService : IHMSProductService
    {
        #region Field
        private readonly Data.Repository.HMS.IProductRepository HMSProductRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public HMSProductService(Data.Repository.HMS.IProductRepository HMSProductRepository, IUnitOfWork unitOfWork)
        {
            this.HMSProductRepository = HMSProductRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for IHMSProductService
        public IEnumerable<HMSProduct> GetHMSProducts()
        {
            var HMSProducts = HMSProductRepository.GetMany(b => !b.Deleted).OrderBy(b => b.Position);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> Get3HMSProductsPosition()
        {
            var HMSProducts = HMSProductRepository.GetMany(b => !b.Deleted).OrderBy(b => b.Position).Take(3);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> GetHomePageHMSProducts()
        {
            var HMSProducts = HMSProductRepository.
                GetMany(b => !b.Deleted && b.IsHomePage).
                OrderByDescending(b => b.DateCreated);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> GetHMSProductByCategoryId(int id)
        {
            var HMSProducts = HMSProductRepository.GetMany(b => b.CategoryProductId.Equals(id)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> GetHMSProductByCategorySlug(string slug)
        {
            var HMSProducts = HMSProductRepository.GetMany(b =>b.Slug.Equals(slug)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> GetStaticPage()
        {
            var HMSProducts = HMSProductRepository.GetMany(b =>!b.Deleted).OrderByDescending(b => b.DateCreated);
            return HMSProducts;
        }

        public HMSProduct GetHMSProductById(int HMSProductId)
        {
            var HMSProduct = HMSProductRepository.GetById(HMSProductId);
            return HMSProduct;
        }

        public void CreateHMSProduct(HMSProduct HMSProduct)
        {
            HMSProductRepository.Add(HMSProduct);
            SaveHMSProduct();
        }

        public void EditHMSProduct(HMSProduct HMSProductToEdit)
        {
            HMSProductToEdit.LastEditedTime = DateTime.Now;
            HMSProductRepository.Update(HMSProductToEdit);
            SaveHMSProduct();
        }

        public void DeleteHMSProduct(int HMSProductId)
        {
            //Get HMSProduct by id.
            var HMSProduct = HMSProductRepository.GetById(HMSProductId);
            if (HMSProduct != null)
            {
                HMSProduct.Deleted = true;
                HMSProductRepository.Update(HMSProduct);
                SaveHMSProduct();
            }
        }

        public void SaveHMSProduct()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddHMSProduct(string slug)
        {
            //Get HMSProduct by url.
            var HMSProduct = HMSProductRepository.Get(b => b.Slug.Equals(slug));
            //Check if slug is exist
            //if (HMSProduct != null)
            //{
            //    yield return new ValidationResult("HMSProduct", Resources.HMSProductExist);
            //}
            return null;
        }

        public HMSProduct GetHMSProductByUrlName(string urlName)
        {
            var HMSProduct = HMSProductRepository.Get(b => b.Slug == urlName);
            return HMSProduct;
        }

        public IEnumerable<HMSProduct> GetHMSProductsByCategory(int HMSProductTypeId)
        {
            var HMSProducts = this.GetHMSProducts();
            return HMSProducts;
        }

        public IEnumerable<HMSProduct> Get6HMSProductService()
        {
            var HMSProducts = this.GetHMSProducts().Take(6);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> Get2HMSProductNews()
        {
            var HMSProducts = this.GetHMSProducts().OrderBy(p => p.Position).Take(2);
            return HMSProducts;
        }
        public IEnumerable<HMSProduct> Get3HMSProductNewsNewest()
        {
            var HMSProducts = this.GetHMSProducts().OrderBy(p => p.Position).Take(3);
            return HMSProducts;
        }
        #endregion


        public HMSProduct GetHMSProductContact()
        {
            var item = HMSProductRepository.Get(p => p.Slug.Equals("lien-he"));
            return item;
        }


        public IEnumerable<HMSProduct> GetNewPost()
        {
            return HMSProductRepository.GetAll().OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
