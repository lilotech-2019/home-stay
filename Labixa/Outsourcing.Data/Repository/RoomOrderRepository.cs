using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
  
    public class RoomOrderRepository : RepositoryBase<RoomOrder>, IRoomOrderRepository
    {
        public RoomOrderRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRoomOrderRepository : IRepository<RoomOrder>
    {

    }
}
