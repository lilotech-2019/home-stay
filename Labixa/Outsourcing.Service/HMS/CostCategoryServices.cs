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
        CostCategory GetCostCategoryById(int CostCategoryId);
        void CreateCostCategory(CostCategory CostCategory);
        void EditCostCategory(CostCategory CostCategoryToEdit);
        void DeleteProductCategories(int CostCategoryId);
        void SaveCostCategory();
        IEnumerable<ValidationResult> CanAddCostCategory(CostCategory CostCategory);

    }
    public class CostCategoryService : ICostCategoryService
    {
        #region Field
        private readonly ICostCategoryRepository CostCategoryRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CostCategoryService(ICostCategoryRepository CostCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.CostCategoryRepository = CostCategoryRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CostCategory> GetProductCategories()
        {
            var CostCategorys = CostCategoryRepository.GetAll().Where(p => p.Deleted == false);
            return CostCategorys;
        }

        public CostCategory GetCostCategoryById(int CostCategoryId)
        {
            var CostCategory = CostCategoryRepository.GetById(CostCategoryId);
            return CostCategory;
        }

        public void CreateCostCategory(CostCategory CostCategory)
        {
            CostCategoryRepository.Add(CostCategory);
            SaveCostCategory();
        }

        public void EditCostCategory(CostCategory CostCategoryToEdit)
        {
            CostCategoryRepository.Update(CostCategoryToEdit);
            SaveCostCategory();
        }

        public void DeleteProductCategories(int CostCategoryId)
        {
            //Get CostCategory by id.
            var CostCategory = CostCategoryRepository.GetById(CostCategoryId);
            if (CostCategory != null)
            {
                CostCategoryRepository.Delete(CostCategory);
                SaveCostCategory();
            }
        }

        public void SaveCostCategory()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCostCategory(CostCategory CostCategory)
        {

            //    yield return new ValidationResult("CostCategory", "ErrorString");
            return null;
        }

        #endregion
    }
}
