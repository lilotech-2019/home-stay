using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PagedList;

namespace Outsourcing.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        IQueryable<T> FindBy();
        IQueryable<T> FindBy(Expression<Func<T, bool>> where);

        #region Obsolete

        [Obsolete]
        T GetById(long id);

        [Obsolete]
        T GetById(string id);

        [Obsolete]
        T Get(Expression<Func<T, bool>> where);


        [Obsolete]
        IEnumerable<T> GetAll();

        [Obsolete]
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        #endregion

        IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);
    }
}