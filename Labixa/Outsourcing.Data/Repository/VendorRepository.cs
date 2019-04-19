using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class VendorRepository : RepositoryBase<Vendors>, IVendorRepository
    {
        public VendorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IVendorRepository : IRepository<Vendors>
    {
    }
}