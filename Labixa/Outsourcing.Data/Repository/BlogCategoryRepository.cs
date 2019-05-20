using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class BlogCategoryRepository : RepositoryBase<BlogCategories>, IBlogTypeRepository
        {
        public BlogCategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }        
        }
    public interface IBlogTypeRepository : IRepository<BlogCategories>
    {
        
    }
}