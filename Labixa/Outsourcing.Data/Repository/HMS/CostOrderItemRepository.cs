using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
    
    public class CostOrderItemRepository : RepositoryBase<CostOrderItem>, ICostOrderItemRepository
    {
        public CostOrderItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICostOrderItemRepository : IRepository<CostOrderItem>
    {

    }
}
