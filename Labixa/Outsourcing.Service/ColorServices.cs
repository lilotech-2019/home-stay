using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Outsourcing.Service
{

    public interface IColorService
    {

        IQueryable<Colors> FindAll();
        Colors FindById(int id);
        void Create(Colors entity);
        void Edit(Colors entity);
        void Delete(int id);
        void Delete(Colors entity);
        IQueryable<Colors> FindSelectList(int? id);
    }
    public class ColorService : IColorService
    {
        #region Field
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ColorService(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod
        public IQueryable<Colors> FindAll()
        {
            var listEntities = _colorRepository.FindBy(w => w.isDelete == false);
            return listEntities;
        }

        public Colors FindById(int id)
        {
            var entity = _colorRepository.FindBy(w => w.isDelete == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(Colors entity)
        {
            _colorRepository.Add(entity);
            commit();
        }

        public void Edit(Colors entity)
        {
            _colorRepository.Update(entity);
            commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public IQueryable<Colors> FindSelectList(int? id)
        {
            var list = _colorRepository.FindBy(r => r.isDelete == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        private void commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Colors entity)
        {
            if (entity != null)
            {
                entity.isDelete = true;
                Edit(entity);
            }
        }

        #endregion
    }
}
