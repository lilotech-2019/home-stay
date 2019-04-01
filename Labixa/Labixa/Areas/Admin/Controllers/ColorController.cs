using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class ColorController : Controller
    {
          #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly IColorService _ColorService;
        readonly IProductPictureMappingService _productPictureMappingService;



        #endregion

        #region Ctor
        public ColorController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService, IColorService _ColorService
           )
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._productAttributeService = productAttributeService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._pictureService = pictureService;
            this._productPictureMappingService = productPictureMappingService;
            this._ColorService = _ColorService;
        }
        #endregion
        //
        // GET: /Admin/Color/
        public ActionResult Index()
        {
            var list = _ColorService.GetColors();
            return View(list);
        }
        public ActionResult Create()
        {
            //Get the list category
            var Color = new Color();
            return View(Color);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Color newColor)
        {
            if (ModelState.IsValid)
            {
                newColor.isDelete = false;
                //Mapping to domain
                //Create Blog
                _ColorService.CreateColor(newColor);
                return RedirectToAction("Index", "Color");
            }
            else
            {
                return View("Create", newColor);
            }
        }

        [HttpGet]
        public ActionResult Edit(int ColorId)
        {

            var Color = _ColorService.GetColorById(ColorId);
            return View(model: Color);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Color Colortoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _ColorService.EditColor(Colortoedit);
                return RedirectToAction("Index", "Color");
            }
            else
            {
                return View("Edit", Colortoedit);
            }
        }
        [HttpPost]
        public ActionResult Delete(int ColorId)
        {
            _ColorService.DeleteColor(ColorId);
            return RedirectToAction("Index");
        }
	}
}