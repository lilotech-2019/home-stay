using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
           #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly IVendorService _VendorService;
        readonly IProductPictureMappingService _productPictureMappingService;



        #endregion

        #region Ctor
        public VendorController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService, IVendorService _VendorService
           )
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._productAttributeService = productAttributeService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._pictureService = pictureService;
            this._productPictureMappingService = productPictureMappingService;
            this._VendorService = _VendorService;
        }
        #endregion
        //
        // GET: /Admin/Vendor/
        public ActionResult Index()
        {
            var list = _VendorService.GetVendors();
            return View(list);
        }
        public ActionResult Create()
        {
            //Get the list category
            var Vendor = new Vendor();
            return View(Vendor);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Vendor newVendor)
        {
            if (ModelState.IsValid)
            {
                newVendor.IsDelete = false;
                //Mapping to domain
                //Create Blog
                _VendorService.CreateVendor(newVendor);
                return RedirectToAction("Index", "Vendor");
            }
            else
            {
                return View("Create", newVendor);
            }
        }

        [HttpGet]
        public ActionResult Edit(int VendorId)
        {

            var Vendor = _VendorService.GetVendorById(VendorId);
            return View(model: Vendor);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Vendor Vendortoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _VendorService.EditVendor(Vendortoedit);
                return RedirectToAction("Index", "Vendor");
            }
            else
            {
                return View("Edit", Vendortoedit);
            }
        }
        [HttpPost]
        public ActionResult Delete(int VendorId)
        {
            _VendorService.DeleteVendor(VendorId);
            return RedirectToAction("Index");
        }
	}
}