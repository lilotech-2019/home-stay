using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository
{
   
    public class RoomImageRepository : RepositoryBase<RoomImages>, IRoomImageRepository
    {
        public RoomImageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRoomImageRepository : IRepository<RoomImages>
    {

    }
}
