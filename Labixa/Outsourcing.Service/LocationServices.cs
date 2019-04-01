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
        Location GetLocationById(int LocationId);
        void CreateLocation(Location Location);
        void EditLocation(Location LocationToEdit);
        void DeleteLocation(int LocationId);
        void SaveLocation();
        IEnumerable<ValidationResult> CanAddLocation(Location Location);

    }
    public class LocationService : ILocationService
    {
        #region Field
        private readonly ILocationRepository LocationRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public LocationService(ILocationRepository LocationRepository, IUnitOfWork unitOfWork)
        {
            this.LocationRepository = LocationRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Location> GetLocations()
        {
            var Locations = LocationRepository.GetAll();
            return Locations;
        }

        public Location GetLocationById(int LocationId)
        {
            var Location = LocationRepository.GetById(LocationId);
            return Location;
        }

        public void CreateLocation(Location Location)
        {
            LocationRepository.Add(Location);
            SaveLocation();
        }

        public void EditLocation(Location LocationToEdit)
        {
            LocationRepository.Update(LocationToEdit);
            SaveLocation();
        }

        public void DeleteLocation(int LocationId)
        {
            //Get Location by id.
            var Location = LocationRepository.GetById(LocationId);
            if (Location != null)
            {
                LocationRepository.Delete(Location);
                SaveLocation();
            }
        }

        public void SaveLocation()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddLocation(Location Location)
        {

            //    yield return new ValidationResult("Location", "ErrorString");
            return null;
        }

        #endregion
    }
}
