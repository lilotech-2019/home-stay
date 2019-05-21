using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
  
    public class CostRepository : RepositoryBase<Cost>, ICostRepository
    {
        public CostRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICostRepository : IRepository<Cost>
    {

    }
}
