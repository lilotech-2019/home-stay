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
using WebGrease.Css.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Helpers;


namespace Labixa.Areas.HMSAdmin.Controllers
{
    #region Field
    public partial class CategoryHotelController : BaseController
    {
        readonly ICategoryHotelService _CategoryHotelService;
        #endregion

        #region Ctor
        public CategoryHotelController(ICategoryHotelService CategoryHotelService)
        {
            _CategoryHotelService = CategoryHotelService;
        }
        #endregion

        public ActionResult Index()
        {
            var CategoryHotels = _CategoryHotelService.GetProductCategories();
            return View(model: CategoryHotels);
        }
        //public ActionResult ManageStaticPage()
        //{
        //    var CategoryHotels = _CategoryHotelService.GetStaticPage();
        //    return View(model: CategoryHotels);
        //}

        public ActionResult Create()
        {
            CategoryHotel model = new CategoryHotel();
            //Get the list category
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(CategoryHotel newCategoryHotel, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                //CategoryHotel CategoryHotel = Mapper.Map<CategoryHotelFormModel, CategoryHotel>(newCategoryHotel);
                if (String.IsNullOrEmpty(newCategoryHotel.Slug))
                {
                    newCategoryHotel.Slug = StringConvert.ConvertShortName(newCategoryHotel.Name);
                }
                //Create CategoryHotel
                _CategoryHotelService.CreateCategoryHotel(newCategoryHotel);
                return continueEditing ? RedirectToAction("Edit", "CategoryHotel", new { CategoryHotelId = newCategoryHotel.Id })
                                  : RedirectToAction("Index", "CategoryHotel");
            }
            else
            {
                return View("Create", newCategoryHotel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int CategoryHotelId)
        {

            var CategoryHotel = _CategoryHotelService.GetCategoryHotelById(CategoryHotelId);
            //CategoryHotelFormModel CategoryHotelFormModel = Mapper.Map<CategoryHotel, CategoryHotelFormModel>(CategoryHotel);
            //CategoryHotelFormModel.ListCategory = _CategoryHotelCategoryService.GetCategoryHotelCategories().ToSelectListItems(-1);

            return View(model: CategoryHotel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryHotel editCategoryHotel, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                //CategoryHotel CategoryHotel = Mapper.Map<CategoryHotelFormModel, CategoryHotel>(CategoryHotelToEdit);
                if (String.IsNullOrEmpty(editCategoryHotel.Slug))
                {
                    editCategoryHotel.Slug = StringConvert.ConvertShortName(editCategoryHotel.Name);
                }
                _CategoryHotelService.EditCategoryHotel(editCategoryHotel);
                return continueEditing ? RedirectToAction("Edit", "CategoryHotel", new { CategoryHotelId = editCategoryHotel.Id })
                                 : RedirectToAction("Index", "CategoryHotel");
            }
            else
            {
                return View("Edit", editCategoryHotel);
            }
        }


        [HttpPost]
        public ActionResult Delete(int CategoryHotelId)
        {
            _CategoryHotelService.DeleteProductCategories(CategoryHotelId);
            return RedirectToAction("Index");
        }


    }
}