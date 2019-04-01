using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Helpers;
using Labixa.Areas.HMSAdmin.ViewModels;

namespace Labixa.Areas.HMSAdmin.Controllers
{

    #region Field
    public partial class HotelController : BaseController
    {
        readonly IHotelService _HotelService;
        readonly ICategoryHotelService _HotelCategoryService;
        #endregion

        #region Ctor
        public HotelController(IHotelService HotelService, ICategoryHotelService HotelCategoryService)
        {
            _HotelService = HotelService;
            _HotelCategoryService = HotelCategoryService;
        }
        #endregion

        public ActionResult Index(int? page = 1)
        {
            var Hotels = _HotelService.GetHotels();
            
          
                             
            return View(model:Hotels);
        }
        public ActionResult ManageStaticPage()
        {
            var Hotels = _HotelService.GetStaticPage();
            return View(model: Hotels);
        }

        public ActionResult Create()
        {
            //Get the list category
            var listCategory = _HotelCategoryService.GetProductCategories().ToSelectListItems(-1);
            var Hotel = new HotelModel { ListCategoryHotel = listCategory };
            return View(Hotel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(HotelModel newHotel, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Hotel = new Hotel
                {
                    //Mapping to domain
                    //Hotel = (Hotel)Mapper.Map<HotelModel, Hotel>(newHotel);
                    Address = newHotel.Address,
                    ContractDate = newHotel.ContractDate,
                    CategoryHotelId = newHotel.CategoryHotelId,
                    ContractExpire = newHotel.ContractExpire,
                    ContractNumber = newHotel.ContractNumber,
                    DateCreated = newHotel.DateCreated,
                    Description = newHotel.Description,
                    HostAddress = newHotel.HostAddress,
                    HostEmail = newHotel.HostEmail,
                    HostName = newHotel.HostName,
                    HostPhone = newHotel.HostPhone,
                    LastEditedTime = newHotel.LastEditedTime,
                    MetaDescription = newHotel.MetaDescription,
                    MetaKeywords = newHotel.MetaKeywords,
                    MetaTitle = newHotel.MetaTitle,
                    Name = newHotel.Name,
                    SharePercent = newHotel.SharePercent,
                    UrlImage1 = newHotel.UrlImage1,
                    UrlImage2 = newHotel.UrlImage2,
                    UrlImage3 = newHotel.UrlImage3,
                    Deleted = false
                };
                if (String.IsNullOrEmpty(Hotel.Slug))
                {
                    Hotel.Slug = StringConvert.ConvertShortName(Hotel.Name);
                }
                //Create Hotel
                _HotelService.CreateHotel(Hotel);
                return continueEditing ? RedirectToAction("Edit", "Hotel", new { HotelId = Hotel.Id })
                                  : RedirectToAction("Index", "Hotel");
            }
            else
            {
                newHotel.ListCategoryHotel = _HotelCategoryService.GetProductCategories().ToSelectListItems(newHotel.CategoryHotelId);
                return View("Create", newHotel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int HotelId)
        {

            var Hotel = _HotelService.GetHotelById(HotelId);
            //HotelModel HotelFormModel = Mapper.Map<Hotel, HotelModel>(Hotel);
            HotelModel HotelFormModel = new HotelModel
            {
                //Mapping to domain
                //Hotel = (Hotel)Mapper.Map<HotelModel, Hotel>(newHotel);
                Slug = Hotel.Slug,
                Id = Hotel.Id,
                Address = Hotel.Address,
                ContractDate = Hotel.ContractDate,
                CategoryHotelId = Hotel.CategoryHotelId,
                ContractExpire = Hotel.ContractExpire,
                ContractNumber = Hotel.ContractNumber,
                DateCreated = Hotel.DateCreated,
                Description = Hotel.Description,
                HostAddress = Hotel.HostAddress,
                HostEmail = Hotel.HostEmail,
                HostName = Hotel.HostName,
                HostPhone = Hotel.HostPhone,
                LastEditedTime = Hotel.LastEditedTime,
                MetaDescription = Hotel.MetaDescription,
                MetaKeywords = Hotel.MetaKeywords,
                MetaTitle = Hotel.MetaTitle,
                Name = Hotel.Name,
                SharePercent = Hotel.SharePercent,
                UrlImage1 = Hotel.UrlImage1,
                UrlImage2 = Hotel.UrlImage2,
                UrlImage3 = Hotel.UrlImage3
            };
            HotelFormModel.ListCategoryHotel = _HotelCategoryService.GetProductCategories().ToSelectListItems(HotelFormModel.CategoryHotelId);

            return View(model: HotelFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(HotelModel HotelToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Hotel = _HotelService.GetHotelById(HotelToEdit.Id);
                //Mapping to domain
                //Mapping to domain
                //Hotel = (Hotel)Mapper.Map<HotelModel, Hotel>(newHotel);
                Hotel.Address = HotelToEdit.Address;
                Hotel.ContractDate = HotelToEdit.ContractDate;
                Hotel.CategoryHotelId = HotelToEdit.CategoryHotelId;
                Hotel.ContractExpire = HotelToEdit.ContractExpire;
                Hotel.ContractNumber = HotelToEdit.ContractNumber;
                Hotel.DateCreated = HotelToEdit.DateCreated;
                Hotel.Description = HotelToEdit.Description;
                Hotel.HostAddress = HotelToEdit.HostAddress;
                Hotel.HostEmail = HotelToEdit.HostEmail;
                Hotel.HostName = HotelToEdit.HostName;
                Hotel.HostPhone = HotelToEdit.HostPhone;
                Hotel.LastEditedTime = HotelToEdit.LastEditedTime;
                Hotel.MetaDescription = HotelToEdit.MetaDescription;
                Hotel.MetaKeywords = HotelToEdit.MetaKeywords;
                Hotel.MetaTitle = HotelToEdit.MetaTitle;
                Hotel.Name = HotelToEdit.Name;
                Hotel.SharePercent = HotelToEdit.SharePercent;
                Hotel.UrlImage1 = HotelToEdit.UrlImage1;
                Hotel.UrlImage2 = HotelToEdit.UrlImage2;
                Hotel.UrlImage3 = HotelToEdit.UrlImage3;
                Hotel.Slug = StringConvert.ConvertShortName(Hotel.Name);
                _HotelService.EditHotel(Hotel);
                return continueEditing ? RedirectToAction("Edit", "Hotel", new { HotelId = Hotel.Id })
                                 : RedirectToAction("Index", "Hotel");
            }
            else
            {
                HotelToEdit.ListCategoryHotel = _HotelCategoryService.GetProductCategories().ToSelectListItems(HotelToEdit.CategoryHotelId);
                return View("Edit", HotelToEdit);
            }
        }




     
        public ActionResult Delete(int HotelId)
        {
            _HotelService.DeleteHotel(HotelId);
            return RedirectToAction("Index");
        }


    }
}