using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Linq;
namespace Outsourcing.Service
{
    public interface IVendorService
    {

        IQueryable<Vendors> FindAll();
        Vendors FindById(int id);
        void Create(Vendors entity);
        void Edit(Vendors entity);
        void Delete(int id);
        void Delete(Vendors entity);
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
            this._vendorRepository = vendorRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod
    
        public IQueryable<Vendors> FindAll()
        {
            var listEntities = _vendorRepository.FindBy(w => w.IsDelete == false);
            return listEntities;
        }

        public Vendors FindById(int id)
        {
            var entity = _vendorRepository.FindBy(w => w.IsDelete == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Vendors entity)
        {
            _vendorRepository.Add(entity);
            commit();
        }

        public void Edit(Vendors entity)
        {
            _vendorRepository.Update(entity);
            commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public void Delete(Vendors entity)
        {
            if (entity != null)
            {
                entity.IsDelete = true;
                Edit(entity);
            }
        }

        private void commit() {
            _unitOfWork.Commit();
        }
        #endregion
    }
}
