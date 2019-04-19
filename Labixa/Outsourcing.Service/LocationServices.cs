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

    public interface ILocationService
    {

        IEnumerable<Location> GetLocations();
        Location GetLocationById(int locationId);
        void CreateLocation(Location location);
        void EditLocation(Location locationToEdit);
        void DeleteLocation(int locationId);
        void SaveLocation();
        IEnumerable<ValidationResult> CanAddLocation(Location location);

    }
    public class LocationService : ILocationService
    {
        #region Field
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork)
        {
            this._locationRepository = locationRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Location> GetLocations()
        {
            var locations = _locationRepository.GetAll();
            return locations;
        }

        public Location GetLocationById(int locationId)
        {
            var location = _locationRepository.GetById(locationId);
            return location;
        }

        public void CreateLocation(Location location)
        {
            _locationRepository.Add(location);
            SaveLocation();
        }

        public void EditLocation(Location locationToEdit)
        {
            _locationRepository.Update(locationToEdit);
            SaveLocation();
        }

        public void DeleteLocation(int locationId)
        {
            //Get Location by id.
            var location = _locationRepository.GetById(locationId);
            if (location != null)
            {
                _locationRepository.Delete(location);
                SaveLocation();
            }
        }

        public void SaveLocation()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddLocation(Location location)
        {

            //    yield return new ValidationResult("Location", "ErrorString");
            return null;
        }

        #endregion
    }
}
