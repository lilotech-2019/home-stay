using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

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
