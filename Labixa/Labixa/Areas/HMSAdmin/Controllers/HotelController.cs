using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Areas.HMSAdmin.ViewModels;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    #region Field

    public partial class HotelController : BaseController
    {
        readonly IHotelService _hotelService;
        readonly ICategoryHotelService _hotelCategoryService;

        #endregion

        #region Ctor

        public HotelController(IHotelService hotelService, ICategoryHotelService hotelCategoryService)
        {
            _hotelService = hotelService;
            _hotelCategoryService = hotelCategoryService;
        }

        #endregion

        public async Task<ActionResult> Index(int? id, int? page = 1)
        {
            var hotels = _hotelService.GetHotels();
            if (id != null)
            {
                hotels = hotels.Where(w => w.Id == id);
            }
            return View(await hotels.ToListAsync());
        }

        public ActionResult ManageStaticPage()
        {
            var hotels = _hotelService.GetStaticPage();
            return View(hotels);
        }

        public ActionResult Create()
        {
            //Get the list category
            var listCategory = _hotelCategoryService.FindAll().ToSelectListItems(-1);
            var hotel = new HotelModel {ListCategoryHotel = listCategory};
            return View(hotel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(HotelModel newHotel, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var hotel = new Hotels
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
                if (String.IsNullOrEmpty(hotel.Slug))
                {
                    hotel.Slug = StringConvert.ConvertShortName(hotel.Name);
                }
                //Create Hotel
                _hotelService.CreateHotel(hotel);
                return continueEditing
                    ? RedirectToAction("Edit", "Hotel", new {HotelId = hotel.Id})
                    : RedirectToAction("Index", "Hotel");
            }
            else
            {
                //newHotel.ListCategoryHotel = _hotelCategoryService.GetProductCategories()
                //    .ToSelectListItems(newHotel.CategoryHotelId);
                //return View("Create", newHotel);
            }
            return null;
        }

        [HttpGet]
        public ActionResult Edit(int hotelId)
        {
            var hotel = _hotelService.GetHotelById(hotelId);
            //HotelModel HotelFormModel = Mapper.Map<Hotel, HotelModel>(Hotel);
            HotelModel hotelFormModel = new HotelModel
            {
                //Mapping to domain
                //Hotel = (Hotel)Mapper.Map<HotelModel, Hotel>(newHotel);
                Slug = hotel.Slug,
                Id = hotel.Id,
                Address = hotel.Address,
                ContractDate = hotel.ContractDate,
                CategoryHotelId = hotel.CategoryHotelId,
                ContractExpire = hotel.ContractExpire,
                ContractNumber = hotel.ContractNumber,
                DateCreated = hotel.DateCreated,
                Description = hotel.Description,
                HostAddress = hotel.HostAddress,
                HostEmail = hotel.HostEmail,
                HostName = hotel.HostName,
                HostPhone = hotel.HostPhone,
                LastEditedTime = hotel.LastEditedTime,
                MetaDescription = hotel.MetaDescription,
                MetaKeywords = hotel.MetaKeywords,
                MetaTitle = hotel.MetaTitle,
                Name = hotel.Name,
                SharePercent = hotel.SharePercent,
                UrlImage1 = hotel.UrlImage1,
                UrlImage2 = hotel.UrlImage2,
                UrlImage3 = hotel.UrlImage3
            };
            hotelFormModel.ListCategoryHotel = _hotelCategoryService.FindAll()
                .ToSelectListItems(hotelFormModel.CategoryHotelId);

            return View(hotelFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(HotelModel hotelToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var hotel = _hotelService.GetHotelById(hotelToEdit.Id);
                //Mapping to domain
                //Mapping to domain
                //Hotel = (Hotel)Mapper.Map<HotelModel, Hotel>(newHotel);
                hotel.Address = hotelToEdit.Address;
                hotel.ContractDate = hotelToEdit.ContractDate;
                hotel.CategoryHotelId = hotelToEdit.CategoryHotelId;
                hotel.ContractExpire = hotelToEdit.ContractExpire;
                hotel.ContractNumber = hotelToEdit.ContractNumber;
                hotel.DateCreated = hotelToEdit.DateCreated;
                hotel.Description = hotelToEdit.Description;
                hotel.HostAddress = hotelToEdit.HostAddress;
                hotel.HostEmail = hotelToEdit.HostEmail;
                hotel.HostName = hotelToEdit.HostName;
                hotel.HostPhone = hotelToEdit.HostPhone;
                hotel.LastEditedTime = hotelToEdit.LastEditedTime;
                hotel.MetaDescription = hotelToEdit.MetaDescription;
                hotel.MetaKeywords = hotelToEdit.MetaKeywords;
                hotel.MetaTitle = hotelToEdit.MetaTitle;
                hotel.Name = hotelToEdit.Name;
                hotel.SharePercent = hotelToEdit.SharePercent;
                hotel.UrlImage1 = hotelToEdit.UrlImage1;
                hotel.UrlImage2 = hotelToEdit.UrlImage2;
                hotel.UrlImage3 = hotelToEdit.UrlImage3;
                hotel.Slug = StringConvert.ConvertShortName(hotel.Name);
                _hotelService.EditHotel(hotel);
                return continueEditing
                    ? RedirectToAction("Edit", "Hotel", new {HotelId = hotel.Id})
                    : RedirectToAction("Index", "Hotel");
            }
            else
            {
                hotelToEdit.ListCategoryHotel = _hotelCategoryService.FindAll()
                    .ToSelectListItems(hotelToEdit.CategoryHotelId);
                return View("Edit", hotelToEdit);
            }
        }


        public ActionResult Delete(int hotelId)
        {
            _hotelService.DeleteHotel(hotelId);
            return RedirectToAction("Index");
        }
    }
}