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
        Shipment GetShipmentById(int shipmentId);
        void CreateShipment(Shipment shipment);
        void EditShipment(Shipment shipmentToEdit);
        void DeleteShipment(int shipmentId);
        void SaveShipment();
        IEnumerable<ValidationResult> CanAddShipment(Shipment shipment);

    }
    public class ShipmentService : IShipmentService
    {
        #region Field
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ShipmentService(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
        {
            this._shipmentRepository = shipmentRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Shipment> GetShipments()
        {
            var shipments = _shipmentRepository.GetAll();
            return shipments;
        }

        public Shipment GetShipmentById(int shipmentId)
        {
            var shipment = _shipmentRepository.GetById(shipmentId);
            return shipment;
        }

        public void CreateShipment(Shipment shipment)
        {
            _shipmentRepository.Add(shipment);
            SaveShipment();
        }

        public void EditShipment(Shipment shipmentToEdit)
        {
            _shipmentRepository.Update(shipmentToEdit);
            SaveShipment();
        }

        public void DeleteShipment(int shipmentId)
        {
            //Get Shipment by id.
            var shipment = _shipmentRepository.GetById(shipmentId);
            if (shipment != null)
            {
                _shipmentRepository.Delete(shipment);
                SaveShipment();
            }
        }

        public void SaveShipment()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddShipment(Shipment shipment)
        {

            //    yield return new ValidationResult("Shipment", "ErrorString");
            return null;
        }

        #endregion
    }
}
