using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IOrderService
    {

        IEnumerable<Order> GetOrders();
        Order GetOrderById(int orderId);
        void CreateOrder(Order order);
        void EditOrder(Order orderToEdit);
        void DeleteOrder(int orderId);
        void SaveOrder();
        IEnumerable<ValidationResult> CanAddOrder(Order order);

    }
    public class OrderService : IOrderService
    {
        #region Field
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Order> GetOrders()
        {
            var orders = _orderRepository.GetAll().OrderByDescending(b => b.DateCreated);
            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            return order;
        }

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            SaveOrder();
        }

        public void EditOrder(Order orderToEdit)
        {
            _orderRepository.Update(orderToEdit);
            SaveOrder();
        }

        public void DeleteOrder(int orderId)
        {
            //Get order by id.
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                _orderRepository.Delete(order);
                SaveOrder();
            }
        }

        public void SaveOrder()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddOrder(Order order)
        {
        
            //    yield return new ValidationResult("Order", "ErrorString");
            return null;
        }

        #endregion
    }
}
