using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
             #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly IPromotionService _promotionService;
        readonly IProductPictureMappingService _productPictureMappingService;



        #endregion

        #region Ctor
        public PromotionController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService, IPromotionService _promotionService
           )
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productAttributeService = productAttributeService;
            _productAttributeMappingService = productAttributeMappingService;
            _pictureService = pictureService;
            _productPictureMappingService = productPictureMappingService;
            this._promotionService = _promotionService;
        }
        #endregion
        //
        // GET: /Admin/Promotion/
        public ActionResult Index()
        {
            var list = _promotionService.GetPromotions();
            return View(list);
        }
        public ActionResult Create()
        {
            //Get the list category
            var promotion = new Promotion();
            return View(promotion);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Promotion newPromotion)
        {
            if (ModelState.IsValid)
            {
                newPromotion.isDelete = false;
                //Mapping to domain
                //Create Blog
                _promotionService.CreatePromotion(newPromotion);
                return RedirectToAction("Index", "Promotion");
            }
            return View("Create", newPromotion);
        }

        [HttpGet]
        public ActionResult Edit(int promotionId)
        {

            var promotion = _promotionService.GetPromotionById(promotionId);
            return View(model: promotion);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Promotion promotiontoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _promotionService.EditPromotion(promotiontoedit);
                return RedirectToAction("Index", "Promotion");
            }
            return View("Edit", promotiontoedit);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _promotionService.DeletePromotion(id);
            return RedirectToAction("Index");
        }
	}
}