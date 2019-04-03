using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Linq.Expressions;

namespace Outsourcing.Data.Repository
{
    public class WebsiteAttributeRepository : RepositoryBase<WebsiteAttributes>, IWebsiteAttributeRepository
    {
        public WebsiteAttributeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IWebsiteAttributeRepository : IRepository<WebsiteAttributes>
    {

    }
}
