using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
  
    public interface ICostOrderService
    {

        IEnumerable<CostOrder> GetCostOrders();
        CostOrder GetCostOrderById(int costOrderId);
        void CreateCostOrder(CostOrder costOrder);
        void EditCostOrder(CostOrder costOrderToEdit);
        void DeleteCostOrder(int costOrderId);
        void SaveCostOrder();
        IEnumerable<ValidationResult> CanAddCostOrder(CostOrder costOrder);

    }
    public class CostOrderService : ICostOrderService
    {
        #region Field
        private readonly ICostOrderRepository _costOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CostOrderService(ICostOrderRepository costOrderRepository, IUnitOfWork unitOfWork)
        {
            this._costOrderRepository = costOrderRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CostOrder> GetCostOrders()
        {
            var costOrders = _costOrderRepository.GetAll().OrderByDescending(b => b.DateCreated);
            return costOrders;
        }

        public CostOrder GetCostOrderById(int costOrderId)
        {
            var costOrder = _costOrderRepository.GetById(costOrderId);
            return costOrder;
        }

        public void CreateCostOrder(CostOrder costOrder)
        {
            _costOrderRepository.Add(costOrder);
            SaveCostOrder();
        }

        public void EditCostOrder(CostOrder costOrderToEdit)
        {
            _costOrderRepository.Update(costOrderToEdit);
            SaveCostOrder();
        }

        public void DeleteCostOrder(int costOrderId)
        {
            //Get CostOrder by id.
            var costOrder = _costOrderRepository.GetById(costOrderId);
            if (costOrder != null)
            {
                _costOrderRepository.Delete(costOrder);
                SaveCostOrder();
            }
        }

        public void SaveCostOrder()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCostOrder(CostOrder costOrder)
        {

            //    yield return new ValidationResult("CostOrder", "ErrorString");
            return null;
        }

        #endregion
    }
}
