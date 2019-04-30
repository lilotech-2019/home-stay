using System.Collections.Generic;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
   
    public interface ICostOrderItemService
    {

        IEnumerable<CostOrderItem> GetCostOrderItems();
        CostOrderItem GetCostOrderItemById(int costOrderItemId);
        void CreateCostOrderItem(CostOrderItem costOrderItem);
        void EditCostOrderItem(CostOrderItem costOrderItemToEdit);
        void DeleteCostOrderItem(int costOrderItemId);
        void SaveCostOrderItem();
        IEnumerable<ValidationResult> CanAddCostOrderItem(CostOrderItem costOrderItem);

    }
    public class CostOrderItemService : ICostOrderItemService
    {
        #region Field
        private readonly ICostOrderItemRepository _costOrderItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CostOrderItemService(ICostOrderItemRepository costOrderItemRepository, IUnitOfWork unitOfWork)
        {
            this._costOrderItemRepository = costOrderItemRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CostOrderItem> GetCostOrderItems()
        {
            var costOrderItems = _costOrderItemRepository.GetAll();
            return costOrderItems;
        }

        public CostOrderItem GetCostOrderItemById(int costOrderItemId)
        {
            var costOrderItem = _costOrderItemRepository.GetById(costOrderItemId);
            return costOrderItem;
        }

        public void CreateCostOrderItem(CostOrderItem costOrderItem)
        {
            _costOrderItemRepository.Add(costOrderItem);
            SaveCostOrderItem();
        }

        public void EditCostOrderItem(CostOrderItem costOrderItemToEdit)
        {
            _costOrderItemRepository.Update(costOrderItemToEdit);
            SaveCostOrderItem();
        }

        public void DeleteCostOrderItem(int costOrderItemId)
        {
            //Get CostOrderItem by id.
            var costOrderItem = _costOrderItemRepository.GetById(costOrderItemId);
            if (costOrderItem != null)
            {
                _costOrderItemRepository.Delete(costOrderItem);
                SaveCostOrderItem();
            }
        }

        public void SaveCostOrderItem()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCostOrderItem(CostOrderItem costOrderItem)
        {

            //    yield return new ValidationResult("CostOrderItem", "ErrorString");
            return null;
        }

        #endregion
    }
}
