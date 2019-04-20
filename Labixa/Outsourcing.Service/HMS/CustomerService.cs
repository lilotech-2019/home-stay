using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Repository.HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Service.HMS
{
    public interface ICustomerService
    {
        int CreateNewCustomerByPhone(String CustomerName, String CustomerEmail, String CustomerPhone);
        int FindIdByPhone(String Phone);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _iCustomerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _iCustomerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public int CreateNewCustomerByPhone(String CustomerName, String CustomerEmail, String CustomerPhone)
        {
            var customerTmp = _iCustomerRepository.FindBy(c => c.Phone == CustomerPhone).FirstOrDefault();
            if (customerTmp == null)
            {
                var newCustomer = new Customer()
                {
                    Name = CustomerName,
                    Email = CustomerEmail,
                    Phone = CustomerPhone
                };
                _iCustomerRepository.Add(newCustomer);
                _unitOfWork.Commit();
                return newCustomer.Id;
            }
            return 0;
        }
        public int FindIdByPhone(String Phone)
        {
            var customerTmp = _iCustomerRepository.FindBy(c => c.Phone == Phone).FirstOrDefault();
            if (customerTmp != null)
            {
                return customerTmp.Id;
            }
            return 0;
        }
    }
}
