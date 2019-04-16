using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
   
    public class CategoryProductRepository : RepositoryBase<CategoryProducts>, ICategoryProductRepository
    {
        public CategoryProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICategoryProductRepository : IRepository<CategoryProducts>
    {

    }
}
