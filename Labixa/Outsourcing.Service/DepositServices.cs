using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Linq;

namespace Outsourcing.Service
{

    public interface IDepositService
    {

        IQueryable<Deposit> FindAll();
        Deposit FindById(int id);
        void Create(Deposit entity);
        void Edit(Deposit entity);
        void Delete(int id);
        void Delete(Deposit entity);
        IQueryable<Deposit> FindSelectList(int? id);
    }
    public class DepositService : IDepositService
    {
        #region Field
        private readonly IDepositRepository _depositRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public DepositService(IDepositRepository depositRepository, IUnitOfWork unitOfWork)
        {
            _depositRepository = depositRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod
        public IQueryable<Deposit> FindAll()
        {
            var listEntities = _depositRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public Deposit FindById(int id)
        {
            var entity = _depositRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Deposit entity)
        {
            _depositRepository.Add(entity);
            Commit();
        }

        public void Edit(Deposit entity)
        {
            _depositRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public IQueryable<Deposit> FindSelectList(int? id)
        {
            var list = _depositRepository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Deposit entity)
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
