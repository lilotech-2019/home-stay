using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Service
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
