using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class ColorRepository : RepositoryBase<Deposit>, IColorRepository
    {
        public ColorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IColorRepository : IRepository<Deposit>
    {

    }
}
