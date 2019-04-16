using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
   
    public class RoomOrderItemRepository : RepositoryBase<RoomOrderItem>, IRoomOrderItemRepository
    {
        public RoomOrderItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRoomOrderItemRepository : IRepository<RoomOrderItem>
    {

    }
}
