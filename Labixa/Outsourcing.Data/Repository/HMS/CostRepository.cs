using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
  
    public class CostRepository : RepositoryBase<Costs>, ICostRepository
    {
        public CostRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICostRepository : IRepository<Costs>
    {

    }
}
