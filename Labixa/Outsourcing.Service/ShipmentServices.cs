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
 
    public interface IShipmentService
    {

        IEnumerable<Shipment> GetShipments();
        Shipment GetShipmentById(int ShipmentId);
        void CreateShipment(Shipment Shipment);
        void EditShipment(Shipment ShipmentToEdit);
        void DeleteShipment(int ShipmentId);
        void SaveShipment();
        IEnumerable<ValidationResult> CanAddShipment(Shipment Shipment);

    }
    public class ShipmentService : IShipmentService
    {
        #region Field
        private readonly IShipmentRepository ShipmentRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ShipmentService(IShipmentRepository ShipmentRepository, IUnitOfWork unitOfWork)
        {
            this.ShipmentRepository = ShipmentRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Shipment> GetShipments()
        {
            var Shipments = ShipmentRepository.GetAll();
            return Shipments;
        }

        public Shipment GetShipmentById(int ShipmentId)
        {
            var Shipment = ShipmentRepository.GetById(ShipmentId);
            return Shipment;
        }

        public void CreateShipment(Shipment Shipment)
        {
            ShipmentRepository.Add(Shipment);
            SaveShipment();
        }

        public void EditShipment(Shipment ShipmentToEdit)
        {
            ShipmentRepository.Update(ShipmentToEdit);
            SaveShipment();
        }

        public void DeleteShipment(int ShipmentId)
        {
            //Get Shipment by id.
            var Shipment = ShipmentRepository.GetById(ShipmentId);
            if (Shipment != null)
            {
                ShipmentRepository.Delete(Shipment);
                SaveShipment();
            }
        }

        public void SaveShipment()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddShipment(Shipment Shipment)
        {

            //    yield return new ValidationResult("Shipment", "ErrorString");
            return null;
        }

        #endregion
    }
}
