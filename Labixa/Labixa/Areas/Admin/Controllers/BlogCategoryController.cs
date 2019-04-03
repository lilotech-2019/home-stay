using System.Web.Mvc;
using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    public class BlogCategoryController : Controller
    {
        #region Field
        IBlogCategoryService _blogCategoryService;
        #endregion

        #region Ctor
        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }
        #endregion
        // GET: Admin/BlogCategory
        public ActionResult Index()
        {
            var list = _blogCategoryService.GetBlogCategories();
            return View(model: list);
        }
        public ActionResult Create()
        {
            var listCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            var list = new BlogCategoryFormModel { ListCategory = listCategory };
            return View(list);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(BlogCategoryFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                BlogCategories blog = Mapper.Map<BlogCategoryFormModel, BlogCategories>(obj);
                var slug = StringConvert.ConvertShortName(blog.Name);
                blog.Slug = slug;
                _blogCategoryService.CreateBlogCategory(blog);
                return continueEditing ? RedirectToAction("Edit", "BlogCategory", new {blog.Id })
                                : RedirectToAction("Index", "BlogCategory");
            }
            var listCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            obj.ListCategory = listCategory;
            return View("Create", obj);
        }

        public ActionResult Edit(int Id)
        {
            var item = _blogCategoryService.GetBlogCategoryById(Id);

            var list = _blogCategoryService.GetBlogCategories().ToSelectListItems(int.Parse(item.CategoryParentId.ToString() == "" ? "0" : item.CategoryParentId.ToString()));

            //BlogCategoryFormModel model1 = Mapper.Map<BlogCategory,BlogCategoryFormModel>(item);
            var blogCategory = Mapper.Map<BlogCategories, BlogCategoryFormModel>(item);
            blogCategory.ListCategory = list;

            return View(blogCategory);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(BlogCategoryFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                BlogCategories item = Mapper.Map<BlogCategoryFormModel, BlogCategories>(obj);
                item.Slug = StringConvert.ConvertShortName(item.Name);
                _blogCategoryService.EditBlogCategory(item);
                return continueEditing ? RedirectToAction("Edit", "BlogCategory", new {item.Id })
                    : RedirectToAction("Index", "BlogCategory");
            }
            var listCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            obj.ListCategory = listCategory;
            return View("Edit", obj);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _blogCategoryService.DeleteBlogCategory(id);
            return RedirectToAction("Index", "BlogCategory");
        }
    }
}