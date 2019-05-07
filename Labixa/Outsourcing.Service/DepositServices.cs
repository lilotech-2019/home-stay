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
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public DepositService(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod
        public IQueryable<Deposit> FindAll()
        {
            var listEntities = _colorRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public Deposit FindById(int id)
        {
            var entity = _colorRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Deposit entity)
        {
            _colorRepository.Add(entity);
            Commit();
        }

        public void Edit(Deposit entity)
        {
            _colorRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public IQueryable<Deposit> FindSelectList(int? id)
        {
            var list = _colorRepository.FindBy(r => r.Deleted == false);
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
