using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

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
