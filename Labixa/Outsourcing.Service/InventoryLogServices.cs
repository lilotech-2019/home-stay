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
        InventoryLog GetInventoryLogById(int InventoryLogId);
        void CreateInventoryLog(InventoryLog InventoryLog);
        void EditInventoryLog(InventoryLog InventoryLogToEdit);
        void DeleteInventoryLog(int InventoryLogId);
        void SaveInventoryLog();
        IEnumerable<ValidationResult> CanAddInventoryLog(InventoryLog InventoryLog);

    }
    public class InventoryLogService : IInventoryLogService
    {
        #region Field
        private readonly IInventoryLogRepository InventoryLogRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public InventoryLogService(IInventoryLogRepository InventoryLogRepository, IUnitOfWork unitOfWork)
        {
            this.InventoryLogRepository = InventoryLogRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<InventoryLog> GetInventoryLogs()
        {
            var InventoryLogs = InventoryLogRepository.GetAll();
            return InventoryLogs;
        }

        public InventoryLog GetInventoryLogById(int InventoryLogId)
        {
            var InventoryLog = InventoryLogRepository.GetById(InventoryLogId);
            return InventoryLog;
        }

        public void CreateInventoryLog(InventoryLog InventoryLog)
        {
            InventoryLogRepository.Add(InventoryLog);
            SaveInventoryLog();
        }

        public void EditInventoryLog(InventoryLog InventoryLogToEdit)
        {
            InventoryLogRepository.Update(InventoryLogToEdit);
            SaveInventoryLog();
        }

        public void DeleteInventoryLog(int InventoryLogId)
        {
            //Get InventoryLog by id.
            var InventoryLog = InventoryLogRepository.GetById(InventoryLogId);
            if (InventoryLog != null)
            {
                InventoryLogRepository.Delete(InventoryLog);
                SaveInventoryLog();
            }
        }

        public void SaveInventoryLog()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddInventoryLog(InventoryLog InventoryLog)
        {

            //    yield return new ValidationResult("InventoryLog", "ErrorString");
            return null;
        }

        #endregion
    }
}
