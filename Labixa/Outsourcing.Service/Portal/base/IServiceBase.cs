using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Service.Portal.@base
{
    public interface IServiceBase<T> where T : BaseEntity
    {
        IQueryable<T> FindAll();
        T FindById(int id);
        void Create(T entity);
        void Edit(T entity);
        void Delete(int id);
        void Delete(T entity);
    }

    public abstract class ServiceBase<T> where T : BaseEntity
    {
        #region Field

        protected readonly IRepository<T> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        #region Ctor

        #endregion

        protected ServiceBase(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        #endregion

        #region

        public void Create(T entity)
        {
            Repository.Add(entity);
            UnitOfWork.Commit();
        }

        #endregion


        public void Delete(int id)
        {
            var entity = FindById(id);
            if (entity == null) return;
            Delete(entity);
        }

        public void Delete(T entity)
        {
            entity.Deleted = true;
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public void Edit(T entity)
        {
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public IQueryable<T> FindAll()
        {
            return Repository.FindBy(w => w.Deleted == false);
        }

        public T FindById(int id)
        {
            return Repository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
        }
    }
}