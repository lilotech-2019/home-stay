using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Linq;

namespace Outsourcing.Service
{
    public interface IBlogService
    {

        IQueryable<Blogs> FindAll();
        Blogs FindById(int id);
        void Create(Blogs entity);
        void Edit(Blogs entity);
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
            this._blogsRepository = blogRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for IHotelService

        public IQueryable<Blogs> FindAll()
        {
            var listEntities = _blogsRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public Blogs FindById(int id)
        {
            var entity = _blogsRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Blogs entity)
        {
            _blogsRepository.Add(entity);
            commit();
        }

        public void Edit(Blogs entity)
        {
            _blogsRepository.Update(entity);
            commit();
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
