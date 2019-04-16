using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
  
    public class RoomImageMappingRepository : RepositoryBase<RoomImageMappings>, IRoomImageMappingRepository
    {
        public RoomImageMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRoomImageMappingRepository : IRepository<RoomImageMappings>
    {

    }
}
