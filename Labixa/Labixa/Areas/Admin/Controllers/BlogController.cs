using System;
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
    //[Authorize]
    public class BlogController : BaseController
    {
        #region Field

        readonly IBlogService _blogService;
        readonly IBlogCategoryService _blogCategoryService;
        #endregion

        #region Ctor
        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }
        #endregion

        public ActionResult Index()
        {
            var blogs = _blogService.GetBlogs();
            return View(model: blogs);
        }
        public ActionResult ManageStaticPage()
        {
            var blogs = _blogService.GetStaticPage();
            return View(model: blogs);
        }

        public ActionResult Create()
        {
            //Get the list category
            var listCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            var blog = new BlogFormModel { ListCategory = listCategory };
            return View(blog);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(BlogFormModel newBlog, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Blogs blog = Mapper.Map<BlogFormModel, Blogs>(newBlog);
                if (String.IsNullOrEmpty(blog.Slug))
                {
                    blog.Slug = StringConvert.ConvertShortName(blog.Title);
                }
                //Create Blog
                _blogService.CreateBlog(blog);
                return continueEditing ? RedirectToAction("Edit", "Blog", new { blogId = blog.Id })
                                  : RedirectToAction("Index", "Blog");
            }
            newBlog.ListCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            return View("Create", newBlog);
        }

        [HttpGet]
        public ActionResult Edit(int blogId)
        {

            var blog = _blogService.GetBlogById(blogId);
            BlogFormModel blogFormModel = Mapper.Map<Blogs, BlogFormModel>(blog);
            blogFormModel.ListCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);

            return View(model: blogFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(BlogFormModel blogToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Blogs blog = Mapper.Map<BlogFormModel, Blogs>(blogToEdit);
                if (String.IsNullOrEmpty(blog.Slug))
                {
                    blog.Slug = StringConvert.ConvertShortName(blog.Title);
                }
                _blogService.EditBlog(blog);
                return continueEditing ? RedirectToAction("Edit", "Blog", new { blogId = blog.Id })
                                 : RedirectToAction("Index", "Blog");
            }
            blogToEdit.ListCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            return View("Edit", blogToEdit);
        }

        [HttpGet]
        public ActionResult EditStaticPage(int blogId)
        {
            var blog = _blogService.GetBlogById(blogId);
            BlogFormModel blogFormModel = Mapper.Map<Blogs, BlogFormModel>(blog);
            return View(model: blogFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult EditStaticPage(BlogFormModel blogToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Blogs blog = Mapper.Map<BlogFormModel, Blogs>(blogToEdit);
              
                blog.BlogCategoryId = 2;
                _blogService.EditBlog(blog);
                return continueEditing ? RedirectToAction("EditStaticPage", "Blog", new { id = blog.Id })
                               : RedirectToAction("ManageStaticPage", "Blog");
            }
            blogToEdit.ListCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
            return View("EditStaticPage", blogToEdit);
        }

        [HttpPost]
        public ActionResult Delete(int blogId)
        {
            _blogService.DeleteBlog(blogId);
            return RedirectToAction("Index");
        }


    }
}