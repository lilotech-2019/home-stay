using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface ICostCategoryService : IServiceBase<CostCategory>
    {
    }

    public class CostCategoryService : ServiceBase<CostCategory>, ICostCategoryService
    {
        public CostCategoryService(IRepository<CostCategory> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}
