using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IBlogService
    {

        IEnumerable<Blogs> GetBlogs();
        Blogs GetBlogContact();
        IEnumerable<Blogs> GetHomePageBlogs();
        IEnumerable<Blogs> GetBlogByCategorySlug(string slug);
        IEnumerable<Blogs> GetBlogByCategoryId(int id);
        IEnumerable<Blogs> Get6BlogService();
        IEnumerable<Blogs> Get2BlogNews();
        IEnumerable<Blogs> Get3BlogNewsNewest();
        Blogs GetBlogById(int blogId);
        void CreateBlog(Blogs blog);
        void EditBlog(Blogs blogToEdit);
        void DeleteBlog(int blogId);
        void SaveBlog();
        IEnumerable<ValidationResult> CanAddBlog(string BlogUrl);

        Blogs GetBlogByUrlName(string urlName);

        IEnumerable<Blogs> GetBlogsByCategory(int blogTypeId);

        IEnumerable<Blogs> GetStaticPage();
        IEnumerable<Blogs> GetNewPost();
        IEnumerable<Blogs> Get3BlogsPosition();
    }
    public class BlogService : IBlogService
    {
        #region Field
        private readonly IBlogRepository blogRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public BlogService(IBlogRepository blogRepository, IUnitOfWork unitOfWork) 
        {
            this.blogRepository = blogRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for IBlogService
        public IEnumerable<Blogs> GetBlogs()
        {
            var blogs = blogRepository.GetMany(b => !b.BlogCategory.IsStaticPage && !b.Deleted).OrderBy(b => b.Position);
            return blogs;
        }
        public IEnumerable<Blogs> Get3BlogsPosition()
        {
            var blogs = blogRepository.GetMany(b => !b.BlogCategory.IsStaticPage && !b.Deleted).OrderBy(b => b.Position).Take(3);
            return blogs;
        }
          public IEnumerable<Blogs> GetHomePageBlogs()
        {
            var blogs = blogRepository.
                GetMany(b => !b.BlogCategory.IsStaticPage && !b.Deleted && b.IsHomePage).
                OrderByDescending(b => b.DateCreated);
            return blogs;
        }
          public IEnumerable<Blogs> GetBlogByCategoryId(int id)
          {
              var blogs = blogRepository.GetMany(b => !b.BlogCategory.IsStaticPage
                  && b.BlogCategory.Id.Equals(id)
                  && !b.Deleted).
                  OrderByDescending(b => b.DateCreated);
              return blogs;
          }
        public IEnumerable<Blogs> GetBlogByCategorySlug(string slug)
        {
            var blogs = blogRepository.GetMany(b => !b.BlogCategory.IsStaticPage 
                && b.BlogCategory.Slug.Equals(slug)
                && !b.Deleted).
                OrderByDescending(b => b.DateCreated);
            return blogs;
        }
        public IEnumerable<Blogs> GetStaticPage()
        {
            var blogs = blogRepository.GetMany(b => b.BlogCategory.IsStaticPage && !b.Deleted).OrderByDescending(b => b.DateCreated);
            return blogs;
        }

        public Blogs GetBlogById(int blogId)
        {
            var blog = blogRepository.GetById(blogId);
            return blog;
        }

        public void CreateBlog(Blogs blog)
        {
            blogRepository.Add(blog);
            SaveBlog();
        }

        public void EditBlog(Blogs blogToEdit)
        {
            blogToEdit.LastEditedTime = DateTime.Now;
            blogRepository.Update(blogToEdit);
            SaveBlog();
        }

        public void DeleteBlog(int blogId)
        {
            //Get blog by id.
            var blog = blogRepository.GetById(blogId);
            if (blog != null)
            {
                blog.Deleted = true;
                blogRepository.Update(blog);
                SaveBlog();
            }
        }

        public void SaveBlog()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddBlog(string slug)
        {
            //Get blog by url.
            var blog = blogRepository.Get(b => b.Slug.Equals(slug));
            //Check if slug is exist
            if (blog != null)
            {
                yield return new ValidationResult("Blog", Resources.BlogExist);
            }
        }

        public Blogs GetBlogByUrlName(string urlName)
        {
            var blog = blogRepository.Get(b => b.Slug == urlName);
            return blog;
        }

        public IEnumerable<Blogs> GetBlogsByCategory(int blogTypeId)
        {
            var blogs = this.GetBlogs().Where(b => b.BlogCategoryId == blogTypeId);
            return blogs;
        }

        public IEnumerable<Blogs> Get6BlogService()
        {
            var blogs = this.GetBlogs().Where(p => p.BlogCategoryId == 6).Take(6);
            return blogs;
        }
        public IEnumerable<Blogs> Get2BlogNews()
        {
            var blogs = this.GetBlogs().Where(p => p.BlogCategoryId == 3).OrderBy(p => p.Position).Take(2);
            return blogs;
        }
        public IEnumerable<Blogs> Get3BlogNewsNewest()
        {
            var blogs = this.GetBlogs().Where(p => p.BlogCategoryId == 3).OrderBy(p=>p.Position).Take(3);
            return blogs;
        }
        #endregion


        public Blogs GetBlogContact()
        {
            var item = blogRepository.Get(p => p.Slug.Equals("lien-he"));
            return item;
        }


        public IEnumerable<Blogs> GetNewPost()
        {
            return blogRepository.GetAll().Where(p => p.BlogCategoryId == 3).OrderByDescending(p => p.DateCreated).Take(5);
        }
        
    }
}
