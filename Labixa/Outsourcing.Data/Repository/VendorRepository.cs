using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class VendorRepository : RepositoryBase<ContactUs>, IVendorRepository
    {
        public VendorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IVendorRepository : IRepository<ContactUs>
    {
    }
}