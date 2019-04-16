using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{

    public class RoomRepository : RepositoryBase<Rooms>, IRoomRepository
    {
        public RoomRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRoomRepository : IRepository<Rooms>
    {

    }
}
