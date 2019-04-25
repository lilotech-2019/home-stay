using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class RoomAssetRepository : RepositoryBase<RoomAsset>, IRoomAssetRepository
    {
        public RoomAssetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IRoomAssetRepository : IRepository<RoomAsset>
    {
    }
}
