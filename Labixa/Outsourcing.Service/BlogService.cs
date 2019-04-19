using System;
using System.Collections.Generic;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Linq;

namespace Outsourcing.Service
{
    public interface IBlogService
    {
        IQueryable<Blogs> FindAll();

        [Obsolete]
        IEnumerable<Blogs> GetBlogs();

        Blogs FindById(int id);
        Blogs FindBySlug(string slug);
        Blogs GetBlogById(int id);
        Blogs GetStaticPage();
        void Create(Blogs entity);
        void Edit(Blogs entity);
        void EditBlog(Blogs entity);
        void Delete(int id);
        void Delete(Blogs entity);
    }

    public class BlogService : IBlogService
    {
        #region Field

        private readonly IBlogRepository _blogsRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public BlogService(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _blogsRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Implementation for IHotelService

        public IQueryable<Blogs> FindAll()
        {
            var listEntities = _blogsRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public IEnumerable<Blogs> GetBlogs()
        {
            return FindAll();
        }

        public IQueryable<Blogs> GetBlogCategories()
        {
            throw new NotImplementedException();
        }

        public Blogs FindById(int id)
        {
            return _blogsRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
        }

        public Blogs FindBySlug(string slug)
        {
            return _blogsRepository.FindBy(w => w.Deleted == false & w.Slug == slug).SingleOrDefault();
        }

        public Blogs GetBlogById(int id)
        {
            return FindById(id);
        }

        public Blogs GetStaticPage()
        {
            throw new NotImplementedException();
        }

        public void Create(Blogs entity)
        {
            _blogsRepository.Add(entity);
            Commit();
        }

        public void Edit(Blogs entity)
        {
            _blogsRepository.Update(entity);
            Commit();
        }

        public void EditBlog(Blogs entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Blogs entity)
        {
            if (entity != null)
            {
                entity.Deleted = true;
                Edit(entity);
            }
        }

        #endregion
    }
}