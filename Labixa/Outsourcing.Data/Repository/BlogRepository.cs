﻿using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IBlogRepository : IRepository<Blog>
    {
    }
}