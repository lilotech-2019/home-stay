using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class ColorRepository : RepositoryBase<Colors>, IColorRepository
    {
        public ColorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IColorRepository : IRepository<Colors>
    {

    }
}
