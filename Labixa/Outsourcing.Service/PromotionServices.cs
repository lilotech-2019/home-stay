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
        Promotion GetPromotionById(int promotionId);
        void CreatePromotion(Promotion promotion);
        void EditPromotion(Promotion promotionToEdit);
        void DeletePromotion(int promotionId);
        void SavePromotion();
        IEnumerable<ValidationResult> CanAddPromotion(Promotion promotion);

    }
    public class PromotionService : IPromotionService
    {
        #region Field
        private readonly IPromotionRepository _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public PromotionService(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
        {
            this._promotionRepository = promotionRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Promotion> GetPromotions()
        {
            var promotions = _promotionRepository.GetAll();
            return promotions;
        }

        public Promotion GetPromotionById(int promotionId)
        {
            var promotion = _promotionRepository.GetById(promotionId);
            return promotion;
        }

        public void CreatePromotion(Promotion promotion)
        {
            _promotionRepository.Add(promotion);
            SavePromotion();
        }

        public void EditPromotion(Promotion promotionToEdit)
        {
            _promotionRepository.Update(promotionToEdit);
            SavePromotion();
        }

        public void DeletePromotion(int promotionId)
        {
            //Get Promotion by id.
            var promotion = _promotionRepository.GetById(promotionId);
            if (promotion != null)
            {
                _promotionRepository.Delete(promotion);
                SavePromotion();
            }
        }

        public void SavePromotion()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddPromotion(Promotion promotion)
        {

            //    yield return new ValidationResult("Promotion", "ErrorString");
            return null;
        }

        #endregion
    }
}
