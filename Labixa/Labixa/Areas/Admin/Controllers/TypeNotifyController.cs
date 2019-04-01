using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class TypeNotifyController : Controller
    {
          #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly ITypeNotifyService _TypeNotifyService;
        readonly IProductPictureMappingService _productPictureMappingService;



        #endregion

        #region Ctor
        public TypeNotifyController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService, ITypeNotifyService _TypeNotifyService
           )
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._productAttributeService = productAttributeService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._pictureService = pictureService;
            this._productPictureMappingService = productPictureMappingService;
            this._TypeNotifyService = _TypeNotifyService;
        }
        #endregion
        //
        // GET: /Admin/TypeNotify/
        public ActionResult Index()
        {
            var list = _TypeNotifyService.GetTypeNotifys();
            return View(list);
        }
        public ActionResult Create()
        {
            //Get the list category
            var TypeNotify = new TypeNotify();
            return View(TypeNotify);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TypeNotify newTypeNotify)
        {
            if (ModelState.IsValid)
            {
                newTypeNotify.IsDelete = false;
                //Mapping to domain
                //Create Blog
                _TypeNotifyService.CreateTypeNotify(newTypeNotify);
                return RedirectToAction("Index", "TypeNotify");
            }
            else
            {
                return View("Create", newTypeNotify);
            }
        }

        [HttpGet]
        public ActionResult Edit(int TypeNotifyId)
        {

            var TypeNotify = _TypeNotifyService.GetTypeNotifyById(TypeNotifyId);
            return View(model: TypeNotify);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TypeNotify TypeNotifytoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _TypeNotifyService.EditTypeNotify(TypeNotifytoedit);
                return RedirectToAction("Index", "TypeNotify");
            }
            else
            {
                return View("Edit", TypeNotifytoedit);
            }
        }
        [HttpPost]
        public ActionResult Delete(int TypeNotifyId)
        {
            _TypeNotifyService.DeleteTypeNotify(TypeNotifyId);
            return RedirectToAction("Index");
        }
	}
}