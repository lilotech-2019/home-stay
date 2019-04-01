using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    public class LocationController : Controller
    {
           #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly ILocationService _LocationService;
        readonly IProductPictureMappingService _productPictureMappingService;



        #endregion

        #region Ctor
        public LocationController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService, ILocationService _LocationService
           )
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productAttributeService = productAttributeService;
            _productAttributeMappingService = productAttributeMappingService;
            _pictureService = pictureService;
            _productPictureMappingService = productPictureMappingService;
            this._LocationService = _LocationService;
        }
        #endregion
        //
        // GET: /Admin/Location/
        public ActionResult Index()
        {
            var list = _LocationService.GetLocations();
            return View(list);
        }
        public ActionResult Create()
        {
            //Get the list category
            var Location = new Location();
            return View(Location);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Location newLocation)
        {
            if (ModelState.IsValid)
            {
                newLocation.isDelete = false;
                //Mapping to domain
                //Create Blog
                _LocationService.CreateLocation(newLocation);
                return RedirectToAction("Index", "Location");
            }
            return View("Create", newLocation);
        }

        [HttpGet]
        public ActionResult Edit(int LocationId)
        {

            var Location = _LocationService.GetLocationById(LocationId);
            return View(model: Location);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Location Locationtoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _LocationService.EditLocation(Locationtoedit);
                return RedirectToAction("Index", "Location");
            }
            return View("Edit", Locationtoedit);
        }
        [HttpPost]
        public ActionResult Delete(int LocationId)
        {
            _LocationService.DeleteLocation(LocationId);
            return RedirectToAction("Index");
        }
	}
}