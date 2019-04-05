using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Service;

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
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productAttributeService = productAttributeService;
            _productAttributeMappingService = productAttributeMappingService;
            _pictureService = pictureService;
            _productPictureMappingService = productPictureMappingService;
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
            var Vendor = new Vendors();
            return View(Vendor);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Vendors newVendor)
        {
            if (ModelState.IsValid)
            {
                newVendor.IsDelete = false;
                //Mapping to domain
                //Create Blog
                _VendorService.CreateVendor(newVendor);
                return RedirectToAction("Index", "Vendor");
            }
            return View("Create", newVendor);
        }

        [HttpGet]
        public ActionResult Edit(int VendorId)
        {

            var Vendor = _VendorService.GetVendorById(VendorId);
            return View(model: Vendor);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Vendors Vendortoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _VendorService.EditVendor(Vendortoedit);
                return RedirectToAction("Index", "Vendor");
            }
            return View("Edit", Vendortoedit);
        }
        [HttpPost]
        public ActionResult Delete(int VendorId)
        {
            _VendorService.DeleteVendor(VendorId);
            return RedirectToAction("Index");
        }
	}
}