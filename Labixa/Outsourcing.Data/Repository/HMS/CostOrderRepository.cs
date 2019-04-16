using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
  
    public class CostOrderRepository : RepositoryBase<CostOrder>, ICostOrderRepository
    {
        public CostOrderRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICostOrderRepository : IRepository<CostOrder>
    {

    }
}
