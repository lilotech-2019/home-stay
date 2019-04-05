using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
