﻿using Outsourcing.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Outsourcing.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : BaseEntity
    {
        private ApplicationDbContext _dataContext;
        private readonly DbSet<T> _dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected ApplicationDbContext DataContext => _dataContext ?? (_dataContext = DatabaseFactory.Get());

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            // _dbset.Attach(entity);
            // _dataContext.Entry(entity).State = EntityState.Modified;
            var item = _dbset.Find(entity.Id);
            if (item == null)
            {
                return;
            }            
            _dataContext.Entry(item).CurrentValues.SetValues(entity);

        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbset.Where(where).AsEnumerable();
            foreach (T obj in objects)
            {
                _dbset.Remove(obj);
            }
        }
        public virtual IQueryable<T> FindBy()
        {
            return _dbset;
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where);
        }

        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }



        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }


        /// <summary>
        /// Return a paged list of entities
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page">Which page to retrieve</param>
        /// <param name="where">Where clause to apply</param>
        /// <param name="order">Order by to apply</param>
        /// <returns></returns>
        public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where,
            Expression<Func<T, TOrder>> order)
        {
            var results = _dbset.OrderBy(order).Where(where).GetPage(page).ToList();
            var total = _dbset.Count(where);
            return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }
    }
}