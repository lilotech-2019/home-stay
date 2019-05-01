using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Repository.HMS;
using System.Linq;

namespace Outsourcing.Service.Portal
{
    public interface ICostService
    {
        IQueryable<Cost> FindAll();
        Cost FindById(int id);
        void Create(Cost entity);
        void Edit(Cost entity);
        void Delete(int id);
        void Delete(Cost entity);
        IQueryable<Cost> FindSelectList(int? id);
    }

    public class CostNewService : ICostService
    {
        #region Field
        private readonly ICostRepository _costRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public CostNewService(ICostRepository costRepository, IUnitOfWork unitOfWork)
        {
            this._costRepository = costRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Implementation for ICostService
        public IQueryable<Cost> FindSelectList(int? id)
        {
            var list = _costRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public IQueryable<Cost> FindAll()
        {
            var listEntities = _costRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public Cost FindById(int id)
        {
            var entity = _costRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Cost entity)
        {
            _costRepository.Add(entity);
            Commit();
        }

        public void Edit(Cost entity)
        {
            _costRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Cost entity)
        {
            if (entity != null)
            {
                entity.Deleted = true;
                Edit(entity);
            }
        }
        #endregion
    }
}