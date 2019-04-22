using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Linq;

namespace Outsourcing.Service
{
    public interface IVendorService
    {
        IQueryable<ContactUs> FindAll();
        ContactUs FindById(int id);
        void Create(ContactUs entity);
        void Edit(ContactUs entity);
        void Delete(int id);
        void Delete(ContactUs entity);
    }

    public class VendorService : IVendorService
    {
        #region Field

        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public VendorService(IVendorRepository vendorRepository, IUnitOfWork unitOfWork)
        {
            _vendorRepository = vendorRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region BaseMethod

        public IQueryable<ContactUs> FindAll()
        {
            var listEntities = _vendorRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public ContactUs FindById(int id)
        {
            var entity = _vendorRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(ContactUs entity)
        {
            _vendorRepository.Add(entity);
            Commit();
        }

        public void Edit(ContactUs entity)
        {
            _vendorRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public void Delete(ContactUs entity)
        {
            if (entity == null) return;
            entity.Deleted = true;
            Edit(entity);
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        #endregion
    }
}