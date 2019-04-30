using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Linq.Expressions;

namespace Outsourcing.Data.Repository
{
    public class WebsiteAttributeRepository : RepositoryBase<WebsiteAtribute>, IWebsiteAttributeRepository
    {
        public WebsiteAttributeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IWebsiteAttributeRepository : IRepository<WebsiteAtribute>
    {

    }
}
