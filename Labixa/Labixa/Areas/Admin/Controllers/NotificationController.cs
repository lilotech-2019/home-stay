﻿using System;
using System.Web.Mvc;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Core.Extensions;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Users = "longt")]
    public class NotificationController : Controller
    {
         #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly INotificationService _NotificationService;
        readonly ITypeNotifyService _TypeNotifyService;
        readonly IProductPictureMappingService _productPictureMappingService;



        #endregion

        #region Ctor
        public NotificationController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService, INotificationService _NotificationService
           , ITypeNotifyService _TypeNotifyService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productAttributeService = productAttributeService;
            _productAttributeMappingService = productAttributeMappingService;
            _pictureService = pictureService;
            _productPictureMappingService = productPictureMappingService;
            this._NotificationService = _NotificationService;
            this._TypeNotifyService = _TypeNotifyService;
        }
        #endregion
        //
        // GET: /Admin/Notification/
        public ActionResult Index()
        {
            var list = _NotificationService.GetNotifications();
            return View(list);
        }
        public ActionResult Create()
        {
            //Get the list category
            var Notification = new Notification();
            var model = new NotificationModels();
            model.notification = Notification;
            var listype = _TypeNotifyService.GetTypeNotifys().ToSelectListItems(-1);
            model.ListType = listype;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NotificationModels newNotification)
        {
            if (ModelState.IsValid)
            {
                newNotification.notification.IsDelete = false;
                newNotification.notification.DateCreated = DateTime.Now;
                //Mapping to domain
                //Create Blog
                _NotificationService.CreateNotification(newNotification.notification);
                return RedirectToAction("Index", "Notification");
            }
            return View("Create", newNotification);
        }

        [HttpGet]
        public ActionResult Edit(int NotificationId)
        {

            var Notification = _NotificationService.GetNotificationById(NotificationId);
            return View(model: Notification);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Notification Notificationtoedit)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                _NotificationService.EditNotification(Notificationtoedit);
                return RedirectToAction("Index", "Notification");
            }
            return View("Edit", Notificationtoedit);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _NotificationService.DeleteNotification(id);
            return RedirectToAction("Index");
        }
	}
}