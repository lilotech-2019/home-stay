using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository
{
  
  
    public class CostCategoryRepository : RepositoryBase<CostCategory>, ICostCategoryRepository
    {
        public CostCategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICostCategoryRepository : IRepository<CostCategory>
    {

    }
}
