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

    public interface IInventoryLogService
    {

        IEnumerable<InventoryLog> GetInventoryLogs();
        InventoryLog GetInventoryLogById(int inventoryLogId);
        void CreateInventoryLog(InventoryLog inventoryLog);
        void EditInventoryLog(InventoryLog inventoryLogToEdit);
        void DeleteInventoryLog(int inventoryLogId);
        void SaveInventoryLog();
        IEnumerable<ValidationResult> CanAddInventoryLog(InventoryLog inventoryLog);

    }
    public class InventoryLogService : IInventoryLogService
    {
        #region Field
        private readonly IInventoryLogRepository _inventoryLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public InventoryLogService(IInventoryLogRepository inventoryLogRepository, IUnitOfWork unitOfWork)
        {
            this._inventoryLogRepository = inventoryLogRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<InventoryLog> GetInventoryLogs()
        {
            var inventoryLogs = _inventoryLogRepository.GetAll();
            return inventoryLogs;
        }

        public InventoryLog GetInventoryLogById(int inventoryLogId)
        {
            var inventoryLog = _inventoryLogRepository.GetById(inventoryLogId);
            return inventoryLog;
        }

        public void CreateInventoryLog(InventoryLog inventoryLog)
        {
            _inventoryLogRepository.Add(inventoryLog);
            SaveInventoryLog();
        }

        public void EditInventoryLog(InventoryLog inventoryLogToEdit)
        {
            _inventoryLogRepository.Update(inventoryLogToEdit);
            SaveInventoryLog();
        }

        public void DeleteInventoryLog(int inventoryLogId)
        {
            //Get InventoryLog by id.
            var inventoryLog = _inventoryLogRepository.GetById(inventoryLogId);
            if (inventoryLog != null)
            {
                _inventoryLogRepository.Delete(inventoryLog);
                SaveInventoryLog();
            }
        }

        public void SaveInventoryLog()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddInventoryLog(InventoryLog inventoryLog)
        {

            //    yield return new ValidationResult("InventoryLog", "ErrorString");
            return null;
        }

        #endregion
    }
}
