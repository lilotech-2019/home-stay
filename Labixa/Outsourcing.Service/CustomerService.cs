using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service
{
    public interface ICustomerService
    {
        void Create(string customerName, string customerEmail, string customerPhone);
        void Create(Customer entity);
        Customer FindByPhone(string phone);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(string customerName, string customerEmail, string customerPhone)
        {
            var customer = FindByPhone(customerPhone);
            if (customer != null) return;
            customer = new Customer
            {
                Name = customerName,
                Email = customerEmail,
                Phone = customerPhone
            };
            Create(customer);
        }

        public void Create(Customer entity)
        {
            _customerRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public Customer FindByPhone(string phone)
        {
            return _customerRepository.FindBy(c => c.Phone == phone).FirstOrDefault();

        }
    }
}