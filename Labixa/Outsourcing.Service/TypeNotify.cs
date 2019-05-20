using System.Collections.Generic;
using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
namespace Outsourcing.Service
{

    public interface ITypeNotifyService
    {

        IEnumerable<TypeNotify> GetTypeNotifys();
        TypeNotify GetTypeNotifyById(int typeNotifyId);
        void CreateTypeNotify(TypeNotify typeNotify);
        void EditTypeNotify(TypeNotify typeNotifyToEdit);
        void DeleteTypeNotify(int typeNotifyId);
        void SaveTypeNotify();
        IEnumerable<ValidationResult> CanAddTypeNotify(TypeNotify typeNotify);

    }
    public class TypeNotifyService : ITypeNotifyService
    {
        #region Field
        private readonly ITypeNotifyRepository _typeNotifyRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public TypeNotifyService(ITypeNotifyRepository typeNotifyRepository, IUnitOfWork unitOfWork)
        {
            this._typeNotifyRepository = typeNotifyRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<TypeNotify> GetTypeNotifys()
        {
            var typeNotifys = _typeNotifyRepository.GetAll();
            return typeNotifys;
        }

        public TypeNotify GetTypeNotifyById(int typeNotifyId)
        {
            var typeNotify = _typeNotifyRepository.GetById(typeNotifyId);
            return typeNotify;
        }

        public void CreateTypeNotify(TypeNotify typeNotify)
        {
            _typeNotifyRepository.Add(typeNotify);
            SaveTypeNotify();
        }

        public void EditTypeNotify(TypeNotify typeNotifyToEdit)
        {
            _typeNotifyRepository.Update(typeNotifyToEdit);
            SaveTypeNotify();
        }

        public void DeleteTypeNotify(int typeNotifyId)
        {
            //Get TypeNotify by id.
            var typeNotify = _typeNotifyRepository.GetById(typeNotifyId);
            if (typeNotify != null)
            {
                _typeNotifyRepository.Delete(typeNotify);
                SaveTypeNotify();
            }
        }

        public void SaveTypeNotify()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddTypeNotify(TypeNotify typeNotify)
        {

            //    yield return new ValidationResult("TypeNotify", "ErrorString");
            return null;
        }

        #endregion
    }
}
