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
   
    public interface ICostOrderItemService
    {

        IEnumerable<CostOrderItem> GetCostOrderItems();
        CostOrderItem GetCostOrderItemById(int CostOrderItemId);
        void CreateCostOrderItem(CostOrderItem CostOrderItem);
        void EditCostOrderItem(CostOrderItem CostOrderItemToEdit);
        void DeleteCostOrderItem(int CostOrderItemId);
        void SaveCostOrderItem();
        IEnumerable<ValidationResult> CanAddCostOrderItem(CostOrderItem CostOrderItem);

    }
    public class CostOrderItemService : ICostOrderItemService
    {
        #region Field
        private readonly ICostOrderItemRepository CostOrderItemRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CostOrderItemService(ICostOrderItemRepository CostOrderItemRepository, IUnitOfWork unitOfWork)
        {
            this.CostOrderItemRepository = CostOrderItemRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CostOrderItem> GetCostOrderItems()
        {
            var CostOrderItems = CostOrderItemRepository.GetAll();
            return CostOrderItems;
        }

        public CostOrderItem GetCostOrderItemById(int CostOrderItemId)
        {
            var CostOrderItem = CostOrderItemRepository.GetById(CostOrderItemId);
            return CostOrderItem;
        }

        public void CreateCostOrderItem(CostOrderItem CostOrderItem)
        {
            CostOrderItemRepository.Add(CostOrderItem);
            SaveCostOrderItem();
        }

        public void EditCostOrderItem(CostOrderItem CostOrderItemToEdit)
        {
            CostOrderItemRepository.Update(CostOrderItemToEdit);
            SaveCostOrderItem();
        }

        public void DeleteCostOrderItem(int CostOrderItemId)
        {
            //Get CostOrderItem by id.
            var CostOrderItem = CostOrderItemRepository.GetById(CostOrderItemId);
            if (CostOrderItem != null)
            {
                CostOrderItemRepository.Delete(CostOrderItem);
                SaveCostOrderItem();
            }
        }

        public void SaveCostOrderItem()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCostOrderItem(CostOrderItem CostOrderItem)
        {

            //    yield return new ValidationResult("CostOrderItem", "ErrorString");
            return null;
        }

        #endregion
    }
}
