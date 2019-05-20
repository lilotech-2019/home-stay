using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface ICostsService : IServiceBase<Cost>
    {
    }

    public class CostsService : ServiceBase<Cost>, ICostsService
    {
        public CostsService(IRepository<Cost> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}
