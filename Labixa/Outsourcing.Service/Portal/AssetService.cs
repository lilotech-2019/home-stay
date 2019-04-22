using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IAssetService : IServiceBase<Asset>
    {
    }

    public class AssetService : ServiceBase<Asset>, IAssetService
    {
        public AssetService(IRepository<Asset> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
