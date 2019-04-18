//using System;
//using System.Web.Mvc;
//using Outsourcing.Service.HMS;
//using Outsourcing.Data.Models.HMS;
//using Outsourcing.Core.Common;
//using Outsourcing.Core.Framework.Controllers;


//namespace Labixa.Areas.HMSAdmin.Controllers
//{
//    public class CategoryHotelController : BaseController
//    {
//        #region Field

//        readonly ICategoryHotelService _categoryHotelService;

//        #endregion

//        #region Ctor

//        public CategoryHotelController(ICategoryHotelService categoryHotelService)
//        {
//            _categoryHotelService = categoryHotelService;
//        }

//        #endregion

//        public ActionResult Index()
//        {
//            var categoryHotels = _categoryHotelService.GetProductCategories();
//            return View(categoryHotels);
//        }
//        //public ActionResult ManageStaticPage()
//        //{
//        //    var CategoryHotels = _CategoryHotelService.GetStaticPage();
//        //    return View(model: CategoryHotels);
//        //}

//        public ActionResult Create()
//        {
//            CategoryHotels model = new CategoryHotels();
//            //Get the list category
//            return View(model);
//        }

//        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
//        [ValidateInput(false)]
//        public ActionResult Create(CategoryHotels newCategoryHotel, bool continueEditing)
//        {
//            if (ModelState.IsValid)
//            {
//                //Mapping to domain
//                //CategoryHotel CategoryHotel = Mapper.Map<CategoryHotelFormModel, CategoryHotel>(newCategoryHotel);
//                if (String.IsNullOrEmpty(newCategoryHotel.Slug))
//                {
//                    newCategoryHotel.Slug = StringConvert.ConvertShortName(newCategoryHotel.Name);
//                }
//                //Create CategoryHotel
//                _categoryHotelService.CreateCategoryHotel(newCategoryHotel);
//                return continueEditing
//                    ? RedirectToAction("Edit", "CategoryHotel", new {CategoryHotelId = newCategoryHotel.Id})
//                    : RedirectToAction("Index", "CategoryHotel");
//            }
//            else
//            {
//                return View("Create", newCategoryHotel);
//            }
//        }

//        [HttpGet]
//        public ActionResult Edit(int categoryHotelId)
//        {
//            var categoryHotel = _categoryHotelService.GetCategoryHotelById(categoryHotelId);
//            //CategoryHotelFormModel CategoryHotelFormModel = Mapper.Map<CategoryHotel, CategoryHotelFormModel>(CategoryHotel);
//            //CategoryHotelFormModel.ListCategory = _CategoryHotelCategoryService.GetCategoryHotelCategories().ToSelectListItems(-1);

//            return View(categoryHotel);
//        }

//        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
//        [ValidateInput(false)]
//        public ActionResult Edit(CategoryHotels editCategoryHotel, bool continueEditing)
//        {
//            if (ModelState.IsValid)
//            {
//                //Mapping to domain
//                //CategoryHotel CategoryHotel = Mapper.Map<CategoryHotelFormModel, CategoryHotel>(CategoryHotelToEdit);
//                if (String.IsNullOrEmpty(editCategoryHotel.Slug))
//                {
//                    editCategoryHotel.Slug = StringConvert.ConvertShortName(editCategoryHotel.Name);
//                }
//                _categoryHotelService.EditCategoryHotel(editCategoryHotel);
//                return continueEditing
//                    ? RedirectToAction("Edit", "CategoryHotel", new {CategoryHotelId = editCategoryHotel.Id})
//                    : RedirectToAction("Index", "CategoryHotel");
//            }
//            else
//            {
//                return View("Edit", editCategoryHotel);
//            }
//        }


//        [HttpPost]
//        public ActionResult Delete(int categoryHotelId)
//        {
//            _categoryHotelService.DeleteProductCategories(categoryHotelId);
//            return RedirectToAction("Index");
//        }
//    }
//}