using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Service
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
