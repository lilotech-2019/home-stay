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
        Inventory GetInventoryById(int InventoryId);
        Inventory CreateInventory(Inventory Inventory);
        void EditInventory(Inventory InventoryToEdit);
        void DeleteInventory(int InventoryId);
        void SaveInventory();
        IEnumerable<ValidationResult> CanAddInventory(Inventory Inventory);

    }
    public class InventoryService : IInventoryService
    {
        #region Field
        private readonly IInventoryRepository InventoryRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public InventoryService(IInventoryRepository InventoryRepository, IUnitOfWork unitOfWork)
        {
            this.InventoryRepository = InventoryRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Inventory> GetInventorys()
        {
            var Inventorys = InventoryRepository.GetAll();
            return Inventorys;
        }

        public Inventory GetInventoryById(int InventoryId)
        {
            var Inventory = InventoryRepository.GetById(InventoryId);
            return Inventory;
        }

        public Inventory CreateInventory(Inventory Inventory)
        {
            InventoryRepository.Add(Inventory);
            SaveInventory();
            return Inventory;
        }

        public void EditInventory(Inventory InventoryToEdit)
        {
            InventoryRepository.Update(InventoryToEdit);
            SaveInventory();
        }

        public void DeleteInventory(int InventoryId)
        {
            //Get Inventory by id.
            var Inventory = InventoryRepository.GetById(InventoryId);
            if (Inventory != null)
            {
                InventoryRepository.Delete(Inventory);
                SaveInventory();
            }
        }

        public void SaveInventory()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddInventory(Inventory Inventory)
        {

            //    yield return new ValidationResult("Inventory", "ErrorString");
            return null;
        }

        #endregion
    }
}
