using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface ICustomerService : IServiceBase<Customer>
    {
    }

    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {
        public CustomerService(IRepository<Customer> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}