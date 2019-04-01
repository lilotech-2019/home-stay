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
    public interface IPromotionService
    {

        IEnumerable<Promotion> GetPromotions();
        Promotion GetPromotionById(int PromotionId);
        void CreatePromotion(Promotion Promotion);
        void EditPromotion(Promotion PromotionToEdit);
        void DeletePromotion(int PromotionId);
        void SavePromotion();
        IEnumerable<ValidationResult> CanAddPromotion(Promotion Promotion);

    }
    public class PromotionService : IPromotionService
    {
        #region Field
        private readonly IPromotionRepository PromotionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public PromotionService(IPromotionRepository PromotionRepository, IUnitOfWork unitOfWork)
        {
            this.PromotionRepository = PromotionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Promotion> GetPromotions()
        {
            var Promotions = PromotionRepository.GetAll();
            return Promotions;
        }

        public Promotion GetPromotionById(int PromotionId)
        {
            var Promotion = PromotionRepository.GetById(PromotionId);
            return Promotion;
        }

        public void CreatePromotion(Promotion Promotion)
        {
            PromotionRepository.Add(Promotion);
            SavePromotion();
        }

        public void EditPromotion(Promotion PromotionToEdit)
        {
            PromotionRepository.Update(PromotionToEdit);
            SavePromotion();
        }

        public void DeletePromotion(int PromotionId)
        {
            //Get Promotion by id.
            var Promotion = PromotionRepository.GetById(PromotionId);
            if (Promotion != null)
            {
                PromotionRepository.Delete(Promotion);
                SavePromotion();
            }
        }

        public void SavePromotion()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddPromotion(Promotion Promotion)
        {

            //    yield return new ValidationResult("Promotion", "ErrorString");
            return null;
        }

        #endregion
    }
}
