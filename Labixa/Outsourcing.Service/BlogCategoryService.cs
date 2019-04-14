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
        IEnumerable<BlogCategories> GetBlogCategories();
        BlogCategories GetBlogCategoryById(int blogId);
        BlogCategories GetBlogCategoryByUrl(string url);
        void CreateBlogCategory(BlogCategories obj);
        void EditBlogCategory(BlogCategories obj);
        void DeleteBlogCategory(string urlName);
        void DeleteBlogCategory(int id);

        void SaveChange();
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

        #region [base Method]

        public IEnumerable<BlogCategories> GetBlogCategories()
        {
            var list = _blogCategoryRepository.FindBy(p => p.Status && !p.IsStaticPage)
                .OrderByDescending(p => p.CategoryParentId).ToList();
            return list;
        }

        public BlogCategories GetBlogCategoryByUrl(string slug)
        {
            try
            {
                var item = _blogCategoryRepository.FindBy(p => p.Slug.Equals(slug)).SingleOrDefault();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BlogCategories GetBlogCategoryById(int blogId)
        {
            try
            {
                var item = _blogCategoryRepository.FindBy(w => w.Id == blogId);
                return item.SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void CreateBlogCategory(BlogCategories obj)
        {
            try
            {
                _blogCategoryRepository.Add(obj);
                SaveChange();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void EditBlogCategory(BlogCategories obj)
        {
            try
            {
                var item = _blogCategoryRepository.FindBy(p => p.Id == obj.Id).SingleOrDefault();
                if (item != null)
                {
                    item.Name = obj.Name;
                    item.Slug = obj.Slug;
                    item.Status = obj.Status;
                    item.DisplayOrder = obj.DisplayOrder;
                    item.CategoryParentId = obj.CategoryParentId;
                    _blogCategoryRepository.Update(item);

                    SaveChange();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void DeleteBlogCategory(string slugName)
        {
            try
            {
                var item = _blogCategoryRepository.FindBy(p => p.Slug.Equals(slugName)).SingleOrDefault();
                if (item != null)
                {
                    item.Status = false;
                    _blogCategoryRepository.Update(item);

                    SaveChange();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }


        public void SaveChange()
        {
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion


        public void DeleteBlogCategory(int id)
        {
            var item = _blogCategoryRepository.FindBy(p => p.Id == id).SingleOrDefault();
            if (item != null)
            {
                item.Status = false;
                _blogCategoryRepository.Update(item);

                SaveChange();
            }
        }
    }
}