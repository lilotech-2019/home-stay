using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IRoomAssetService : IServiceBase<RoomAsset>
    {
    }

    public class RoomRoomAssetService : ServiceBase<RoomAsset>, IRoomAssetService
    {
        public RoomRoomAssetService(IRepository<RoomAsset> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}