using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Service
{
    public interface ICustomerService : IServiceBase<Customer>
    {
        Customer FindByPhone(string phone);
    }

    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {
        public CustomerService(IRepository<Customer> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public Customer FindByPhone(string phone)
        {
            return Repository.FindBy(w => w.Deleted == false & w.Phone.Equals(phone)).FirstOrDefault();
        }
    }
}