using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IPictureService _pictureService;
        readonly IVendorService _vendorService;
        readonly ILocationService _locationService;
        readonly IPromotionService _promotionService;
        readonly IColorService _colorService;

        readonly IProductPictureMappingService _productPictureMappingService;
        readonly IProductCategoryMappingService _productCategoryMappingService;

        #endregion

        #region Ctor

        public ProductController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService,
            IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService,
            IVendorService vendorService,
            ILocationService locationService,
            IPromotionService promotionService,
            IProductCategoryMappingService productCategoryMappingService,
            IColorService colorService
        )
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _pictureService = pictureService;
            _productPictureMappingService = productPictureMappingService;
            _vendorService = vendorService;
            _promotionService = promotionService;
            _locationService = locationService;
            _productCategoryMappingService = productCategoryMappingService;
            _colorService = colorService;
        }

        #endregion

        public ActionResult Index()
        {
            var products = _productService.GetProducts().ToList();

            //foreach (var item in products)
            //{
            //    ProductAttributeMapping obj = new ProductAttributeMapping();
            //    obj.IsRequired = false;
            //    obj.Value = "true";
            //    obj.DisplayOrder = 0;
            //    obj.ProductId = item.Id;
            //    obj.ProductAttributeId = 13;
            //    _productAttributeMappingService.CreateProductAttributeMapping(obj);
            ////adddata(item.Id);
            //}
            return View(model: products);
        }

        public ActionResult Create()
        {
            //Get the list category
            var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
            var listVendor = _vendorService.GetVendors().ToSelectListItems(-1);
            var listPromotion = _promotionService.GetPromotions().ToSelectListItems(-1);
            var listLocation = _locationService.GetLocations().ToSelectListItems(-1);
            var listColor = _colorService.GetColors().ToSelectListItems(-1);
            Product product = new Product();
            ProductFormModel model = new ProductFormModel
            {
                ListColors = listColor,
                ListProductCategory = listProductCategory,
                Location = listLocation,
                Promotion = listPromotion,
                Vendor = listVendor,
                product = product
            };

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(ProductFormModel newProduct, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                //Product product = Mapper.Map<ProductFormModel, Product>(newProduct.pro);
                Product product = newProduct.product;
                if (String.IsNullOrEmpty(product.Slug))
                {
                    product.Slug = StringConvert.ConvertShortName(product.Name);
                }

                //Create Product
                _productService.CreateProduct(product);
                if (newProduct.CategoryId != 0)
                {
                    ProductCategoryMapping obj = new ProductCategoryMapping();
                    obj.ProductId = product.Id;
                    obj.ProductCategoryId = newProduct.CategoryId;
                    _productCategoryMappingService.CreateProductCategoryMapping(obj);
                }
                if (newProduct.CategoryId2 != 0)
                {
                    ProductCategoryMapping obj = new ProductCategoryMapping();
                    obj.ProductId = product.Id;
                    obj.ProductCategoryId = newProduct.CategoryId2;
                    _productCategoryMappingService.CreateProductCategoryMapping(obj);
                }
                //Add ProductAttribute after product created
                //product.ProductAttributeMappings = new Collection<ProductAttributeMapping>();
                //var listAttributeId = _productAttributeService.GetProductAttributes().Select(p => p.Id);
                //foreach (var id in listAttributeId)
                //{
                //    product.ProductAttributeMappings.Add(
                //        new ProductAttributeMapping() { ProductAttributeId = id, ProductId = product.Id });

                //}
                //Add Picture default for Labixa
                product.ProductPictureMappings = new Collection<ProductPictureMapping>();
                for (int i = 0; i < 8; i++)
                {
                    var newPic = new Picture();
                    bool ismain = i == 0;
                    _pictureService.CreatePicture(newPic);
                    product.ProductPictureMappings.Add(
                        new ProductPictureMapping
                        {
                            PictureId = newPic.Id,
                            ProductId = product.Id,
                            IsMainPicture = ismain,
                            DisplayOrder = 0
                        });
                }
                _productService.EditProduct(product);


                //create product relation

                //Save all after edit
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
                var listVendor = _vendorService.GetVendors().ToSelectListItems(-1);
                var ListPromotion = _promotionService.GetPromotions().ToSelectListItems(-1);
                var ListLocation = _locationService.GetLocations().ToSelectListItems(-1);
                var ListColor = _colorService.GetColors().ToSelectListItems(-1);
                Product product = new Product();
                ProductFormModel model = new ProductFormModel();

                model.ListColors = ListColor;
                model.ListProductCategory = listProductCategory;
                model.Location = ListLocation;
                model.Promotion = ListPromotion;
                model.Vendor = listVendor;
                model.product = product;
                return View("Create", model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int productId)
        {
            Product product = _productService.GetProductById(productId);
            var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
            //var vendorid = product.VendorId != null ? -1 : product.VendorId;
            //var listVendor = _VendorService.GetVendors().ToSelectListItems(product.VendorId);
            //var promotionid = product.PromotionId != null ? -1 : product.VendorId;
            //var ListPromotion = _PromotionService.GetPromotions().ToSelectListItems(product.PromotionId);
            //var locationid = product.LocationId != null ? -1 : product.VendorId;
            //var ListLocation = _LocationService.GetLocations().ToSelectListItems(product.LocationId);
            // var ListColor = _ColorService.GetColors().ToSelectListItems(product.ColorId);

            ProductFormModel model = new ProductFormModel();
            model.CategoryId = product.ProductCategoryMappings.FirstOrDefault().ProductCategoryId;
            model.CategoryId2 = product.ProductCategoryMappings.LastOrDefault().ProductCategoryId;
            model.ListProductCategory = listProductCategory;
            //model.Location = ListLocation;
            //model.Promotion = ListPromotion;
            //model.Vendor = listVendor;
            //model.ListColors = ListColor;
            model.product = product;
            return View(model: model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(ProductFormModel productToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Product product = productToEdit.product;
                if (productToEdit.CategoryId != 0)
                {
                    var obj = _productCategoryMappingService.GetProductCategoryMappings()
                        .Where(p => p.ProductId == productToEdit.product.Id);
                    obj.FirstOrDefault().ProductCategoryId = productToEdit.CategoryId;
                    _productCategoryMappingService.EditProductCategoryMapping(obj.FirstOrDefault());
                }
                if (productToEdit.CategoryId2 != 0)
                {
                    var obj = _productCategoryMappingService.GetProductCategoryMappings()
                        .Where(p => p.ProductId == productToEdit.product.Id);
                    obj.LastOrDefault().ProductCategoryId = productToEdit.CategoryId;
                    _productCategoryMappingService.EditProductCategoryMapping(obj.LastOrDefault());
                }
                //Product product = Mapper.Map<ProductFormModel, Product>(productToEdit.product);
                if (String.IsNullOrEmpty(product.Slug))
                {
                    product.Slug = StringConvert.ConvertShortName(product.Name);
                }
                //this funcion not update all relationship value.
                _productService.EditProduct(product);
                //update attribute
                //foreach (var mapping in product.ProductAttributeMappings)
                //{
                //    _productAttributeMappingService.EditProductAttributeMapping(mapping);
                //}
                //update productpicture URL
                foreach (var picture in product.ProductPictureMappings)
                {
                    _productPictureMappingService.EditProductPictureMapping(picture);
                    _pictureService.EditPicture(picture.Picture);
                }
                //add tour relation
                return continueEditing
                    ? RedirectToAction("Edit", "Product", new {productId = product.Id})
                    : RedirectToAction("Index", "Product");
            }
            var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
            productToEdit.ListProductCategory = listProductCategory;
            return RedirectToAction("Edit", new {productId = productToEdit.product.Id});
        }


        public ActionResult Delete(int productId)
        {
            _productService.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}