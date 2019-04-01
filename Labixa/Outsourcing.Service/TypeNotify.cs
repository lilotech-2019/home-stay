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

    public interface ITypeNotifyService
    {

        IEnumerable<TypeNotify> GetTypeNotifys();
        TypeNotify GetTypeNotifyById(int TypeNotifyId);
        void CreateTypeNotify(TypeNotify TypeNotify);
        void EditTypeNotify(TypeNotify TypeNotifyToEdit);
        void DeleteTypeNotify(int TypeNotifyId);
        void SaveTypeNotify();
        IEnumerable<ValidationResult> CanAddTypeNotify(TypeNotify TypeNotify);

    }
    public class TypeNotifyService : ITypeNotifyService
    {
        #region Field
        private readonly ITypeNotifyRepository TypeNotifyRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public TypeNotifyService(ITypeNotifyRepository TypeNotifyRepository, IUnitOfWork unitOfWork)
        {
            this.TypeNotifyRepository = TypeNotifyRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<TypeNotify> GetTypeNotifys()
        {
            var TypeNotifys = TypeNotifyRepository.GetAll();
            return TypeNotifys;
        }

        public TypeNotify GetTypeNotifyById(int TypeNotifyId)
        {
            var TypeNotify = TypeNotifyRepository.GetById(TypeNotifyId);
            return TypeNotify;
        }

        public void CreateTypeNotify(TypeNotify TypeNotify)
        {
            TypeNotifyRepository.Add(TypeNotify);
            SaveTypeNotify();
        }

        public void EditTypeNotify(TypeNotify TypeNotifyToEdit)
        {
            TypeNotifyRepository.Update(TypeNotifyToEdit);
            SaveTypeNotify();
        }

        public void DeleteTypeNotify(int TypeNotifyId)
        {
            //Get TypeNotify by id.
            var TypeNotify = TypeNotifyRepository.GetById(TypeNotifyId);
            if (TypeNotify != null)
            {
                TypeNotifyRepository.Delete(TypeNotify);
                SaveTypeNotify();
            }
        }

        public void SaveTypeNotify()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddTypeNotify(TypeNotify TypeNotify)
        {

            //    yield return new ValidationResult("TypeNotify", "ErrorString");
            return null;
        }

        #endregion
    }
}
