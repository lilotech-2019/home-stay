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
        IQueryable<Blog> FindAll();

        [Obsolete]
        IEnumerable<Blog> GetBlogs();

        Blog FindById(int id);
        Blog FindBySlug(string slug);
        Blog GetBlogById(int id);
        Blog GetStaticPage();
        void Create(Blog entity);
        void Edit(Blog entity);
        void EditBlog(Blog entity);
        void Delete(int id);
        void Delete(Blog entity);
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

        public IQueryable<Blog> FindAll()
        {
            var listEntities = _blogsRepository.FindBy(w => w.Deleted == false & w.Status == true);
            return listEntities;
        }

        public IEnumerable<Blog> GetBlogs()
        {
            return FindAll();
        }

        public IQueryable<Blog> GetBlogCategories()
        {
            throw new NotImplementedException();
        }

        public Blog FindById(int id)
        {
            return _blogsRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
        }

        public Blog FindBySlug(string slug)
        {
            return _blogsRepository.FindBy(w => w.Deleted == false & w.Slug == slug & w.Status == true).SingleOrDefault();
        }

        public Blog GetBlogById(int id)
        {
            return FindById(id);
        }

        public Blog GetStaticPage()
        {
            throw new NotImplementedException();
        }

        public void Create(Blog entity)
        {
            _blogsRepository.Add(entity);
            commit();
        }

        public void Edit(Blog entity)
        {
            _blogsRepository.Update(entity);
            commit();
        }

        public void EditBlog(Blog entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        private void commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Blog entity)
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