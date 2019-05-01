using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface ICostCategoriesService : IServiceBase<CostCategory>
    {
    }

    public class CostCategoriesService : ServiceBase<CostCategory>, ICostCategoriesService
    {
        public CostCategoriesService(IRepository<CostCategory> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}
