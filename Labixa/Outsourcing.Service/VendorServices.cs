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
    public interface IVendorService
    {

        IEnumerable<Vendors> GetVendors();
        Vendors GetVendorById(int VendorId);
        void CreateVendor(Vendors Vendor);
        void EditVendor(Vendors VendorToEdit);
        void DeleteVendor(int VendorId);
        void SaveVendor();
        IEnumerable<ValidationResult> CanAddVendor(Vendors Vendor);

    }
    public class VendorService : IVendorService
    {
        #region Field
        private readonly IVendorRepository VendorRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public VendorService(IVendorRepository VendorRepository, IUnitOfWork unitOfWork)
        {
            this.VendorRepository = VendorRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Vendors> GetVendors()
        {
            var Vendors = VendorRepository.GetAll();
            return Vendors;
        }

        public Vendors GetVendorById(int VendorId)
        {
            var Vendor = VendorRepository.GetById(VendorId);
            return Vendor;
        }

        public void CreateVendor(Vendors Vendor)
        {
            VendorRepository.Add(Vendor);
            SaveVendor();
        }

        public void EditVendor(Vendors VendorToEdit)
        {
            VendorRepository.Update(VendorToEdit);
            SaveVendor();
        }

        public void DeleteVendor(int VendorId)
        {
            //Get Vendor by id.
            var Vendor = VendorRepository.GetById(VendorId);
            if (Vendor != null)
            {
                VendorRepository.Delete(Vendor);
                SaveVendor();
            }
        }

        public void SaveVendor()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddVendor(Vendors Vendor)
        {

            //    yield return new ValidationResult("Vendor", "ErrorString");
            return null;
        }

        #endregion
    }
}
