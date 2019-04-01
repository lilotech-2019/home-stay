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
        CostOrder GetCostOrderById(int CostOrderId);
        void CreateCostOrder(CostOrder CostOrder);
        void EditCostOrder(CostOrder CostOrderToEdit);
        void DeleteCostOrder(int CostOrderId);
        void SaveCostOrder();
        IEnumerable<ValidationResult> CanAddCostOrder(CostOrder CostOrder);

    }
    public class CostOrderService : ICostOrderService
    {
        #region Field
        private readonly ICostOrderRepository CostOrderRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CostOrderService(ICostOrderRepository CostOrderRepository, IUnitOfWork unitOfWork)
        {
            this.CostOrderRepository = CostOrderRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CostOrder> GetCostOrders()
        {
            var CostOrders = CostOrderRepository.GetAll().OrderByDescending(b => b.DateCreated);
            return CostOrders;
        }

        public CostOrder GetCostOrderById(int CostOrderId)
        {
            var CostOrder = CostOrderRepository.GetById(CostOrderId);
            return CostOrder;
        }

        public void CreateCostOrder(CostOrder CostOrder)
        {
            CostOrderRepository.Add(CostOrder);
            SaveCostOrder();
        }

        public void EditCostOrder(CostOrder CostOrderToEdit)
        {
            CostOrderRepository.Update(CostOrderToEdit);
            SaveCostOrder();
        }

        public void DeleteCostOrder(int CostOrderId)
        {
            //Get CostOrder by id.
            var CostOrder = CostOrderRepository.GetById(CostOrderId);
            if (CostOrder != null)
            {
                CostOrderRepository.Delete(CostOrder);
                SaveCostOrder();
            }
        }

        public void SaveCostOrder()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCostOrder(CostOrder CostOrder)
        {

            //    yield return new ValidationResult("CostOrder", "ErrorString");
            return null;
        }

        #endregion
    }
}
