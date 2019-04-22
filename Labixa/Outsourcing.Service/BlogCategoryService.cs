using System;
using System.Collections.Generic;
using System.Linq;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;

namespace Outsourcing.Service
{
    public interface IBlogCategoryService
    {
        IQueryable<BlogCategories> FindAll();
        BlogCategories FindById(int id);
        void Create(BlogCategories entity);
        void Edit(BlogCategories entity);
        void Delete(BlogCategories entity);
        void Delete(int id);
        IQueryable<BlogCategories> FindSelectList(int? id);

        [Obsolete]
        IEnumerable<BlogCategories> GetBlogCategories();
    }

    public class BlogCategoryService : IBlogCategoryService
    {
        #region Field

        private readonly IBlogTypeRepository _blogCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public BlogCategoryService(IBlogTypeRepository blogCategoryRepository, IUnitOfWork unitOfWork)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Implementation for IHotelService

        public IQueryable<BlogCategories> FindAll()
        {
            var listEntities = _blogCategoryRepository.FindBy(w => w.IsDelete == false);
            return listEntities;
        }

        public BlogCategories FindById(int id)
        {
            var entity = _blogCategoryRepository.FindBy(w => w.IsDelete == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(BlogCategories entity)
        {
            _blogCategoryRepository.Add(entity);
            Commit();
        }

        public void Edit(BlogCategories entity)
        {
            _blogCategoryRepository.Update(entity);
            Commit();
        }

        #endregion

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public IEnumerable<BlogCategories> GetBlogCategories()
        {
return            _blogCategoryRepository.GetAll();
        }

        public void Delete(BlogCategories entity)
        {
            if (entity != null)
            {
                entity.IsDelete = true;
                Edit(entity);
            }
        }

        public IQueryable<BlogCategories> FindSelectList(int? id)
        {
            var list = _blogCategoryRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }
    }
}