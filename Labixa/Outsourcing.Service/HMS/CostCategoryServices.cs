using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface ICostCategoryService
    {

        IEnumerable<CostCategory> GetProductCategories();
        CostCategory GetCostCategoryById(int costCategoryId);
        void CreateCostCategory(CostCategory costCategory);
        void EditCostCategory(CostCategory costCategoryToEdit);
        void DeleteProductCategories(int costCategoryId);
        void SaveCostCategory();
        IEnumerable<ValidationResult> CanAddCostCategory(CostCategory costCategory);

    }
    public class CostCategoryService : ICostCategoryService
    {
        #region Field
        private readonly ICostCategoryRepository _costCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CostCategoryService(ICostCategoryRepository costCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._costCategoryRepository = costCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CostCategory> GetProductCategories()
        {
            var costCategorys = _costCategoryRepository.GetAll().Where(p => p.Deleted == false);
            return costCategorys;
        }

        public CostCategory GetCostCategoryById(int costCategoryId)
        {
            var costCategory = _costCategoryRepository.GetById(costCategoryId);
            return costCategory;
        }

        public void CreateCostCategory(CostCategory costCategory)
        {
            _costCategoryRepository.Add(costCategory);
            SaveCostCategory();
        }

        public void EditCostCategory(CostCategory costCategoryToEdit)
        {
            _costCategoryRepository.Update(costCategoryToEdit);
            SaveCostCategory();
        }

        public void DeleteProductCategories(int costCategoryId)
        {
            //Get CostCategory by id.
            var costCategory = _costCategoryRepository.GetById(costCategoryId);
            if (costCategory != null)
            {
                _costCategoryRepository.Delete(costCategory);
                SaveCostCategory();
            }
        }

        public void SaveCostCategory()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCostCategory(CostCategory costCategory)
        {

            //    yield return new ValidationResult("CostCategory", "ErrorString");
            return null;
        }

        #endregion
    }
}
