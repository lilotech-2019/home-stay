using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;

namespace Outsourcing.Service
{

    public interface IInventoryService
    {

        IEnumerable<Inventory> GetInventorys();
        Inventory GetInventoryById(int inventoryId);
        Inventory CreateInventory(Inventory inventory);
        void EditInventory(Inventory inventoryToEdit);
        void DeleteInventory(int inventoryId);
        void SaveInventory();
        IEnumerable<ValidationResult> CanAddInventory(Inventory inventory);

    }
    public class InventoryService : IInventoryService
    {
        #region Field
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public InventoryService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
        {
            this._inventoryRepository = inventoryRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Inventory> GetInventorys()
        {
            var inventorys = _inventoryRepository.GetAll();
            return inventorys;
        }

        public Inventory GetInventoryById(int inventoryId)
        {
            var inventory = _inventoryRepository.GetById(inventoryId);
            return inventory;
        }

        public Inventory CreateInventory(Inventory inventory)
        {
            _inventoryRepository.Add(inventory);
            SaveInventory();
            return inventory;
        }

        public void EditInventory(Inventory inventoryToEdit)
        {
            _inventoryRepository.Update(inventoryToEdit);
            SaveInventory();
        }

        public void DeleteInventory(int inventoryId)
        {
            //Get Inventory by id.
            var inventory = _inventoryRepository.GetById(inventoryId);
            if (inventory != null)
            {
                _inventoryRepository.Delete(inventory);
                SaveInventory();
            }
        }

        public void SaveInventory()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddInventory(Inventory inventory)
        {

            //    yield return new ValidationResult("Inventory", "ErrorString");
            return null;
        }

        #endregion
    }
}
