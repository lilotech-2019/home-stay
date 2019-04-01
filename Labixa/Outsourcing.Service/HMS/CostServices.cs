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
    public interface ICostService
    {

        IEnumerable<Cost> GetCosts();
        Cost GetCostContact();
        IEnumerable<Cost> GetHomePageCosts();
        IEnumerable<Cost> GetCostByCategorySlug(string slug);
        IEnumerable<Cost> GetCostByCategoryId(int id);
        IEnumerable<Cost> Get6CostService();
        IEnumerable<Cost> Get2CostNews();
        IEnumerable<Cost> Get3CostNewsNewest();
        Cost GetCostById(int CostId);
        void CreateCost(Cost Cost);
        void EditCost(Cost CostToEdit);
        void DeleteCost(int CostId);
        void SaveCost();
        IEnumerable<ValidationResult> CanAddCost(string CostUrl);

        Cost GetCostByUrlName(string urlName);

        IEnumerable<Cost> GetCostsByCategory(int CostTypeId);

        IEnumerable<Cost> GetStaticPage();
        IEnumerable<Cost> GetNewPost();
    }
    public class CostService : ICostService
    {
        #region Field
        private readonly ICostRepository CostRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CostService(ICostRepository CostRepository, IUnitOfWork unitOfWork)
        {
            this.CostRepository = CostRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for ICostService
        public IEnumerable<Cost> GetCosts()
        {
            var Costs = CostRepository.GetAll().Where(p=>!p.IsDelete);
            return Costs;
        }
        public IEnumerable<Cost> Get3CostsPosition()
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Cost> GetHomePageCosts()
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Cost> GetCostByCategoryId(int id)
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Cost> GetCostByCategorySlug(string slug)
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Cost> GetStaticPage()
        {
            var Costs = CostRepository.GetAll();
            return Costs;
        }

        public Cost GetCostById(int CostId)
        {
            var Cost = CostRepository.GetById(CostId);
            return Cost;
        }

        public void CreateCost(Cost Cost)
        {
            CostRepository.Add(Cost);
            SaveCost();
        }

        public void EditCost(Cost CostToEdit)
        {
            //CostToEdit.LastEditedTime = DateTime.Now;
            CostRepository.Update(CostToEdit);
            SaveCost();
        }

        public void DeleteCost(int CostId)
        {
            //Get Cost by id.
            var Cost = CostRepository.GetById(CostId);
            if (Cost != null)
            {
                Cost.IsDelete = true;
                CostRepository.Update(Cost);
                SaveCost();
            }
        }

        public void SaveCost()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCost(string slug)
        {
            //Get Cost by url.
            var Costs = CostRepository.GetAll();
            return null;
        }

        public Cost GetCostByUrlName(string urlName)
        {
            var Costs = CostRepository.GetAll();

            return null;
        }

        public IEnumerable<Cost> GetCostsByCategory(int CostTypeId)
        {
            var Costs = this.GetCosts().Where(b => b.CostCategoryId == CostTypeId);
            return Costs;
        }

        public IEnumerable<Cost> Get6CostService()
        {
            var Costs = this.GetCosts().Where(p => p.CostCategoryId == 6).Take(6);
            return Costs;
        }
        public IEnumerable<Cost> Get2CostNews()
        {
            var Costs = CostRepository.GetAll();
            return Costs;
        }
        public IEnumerable<Cost> Get3CostNewsNewest()
        {
            var Costs = CostRepository.GetAll();
            return Costs;
        }
        #endregion


        public Cost GetCostContact()
        {
            var Costs = CostRepository.GetAll();
            return Costs.FirstOrDefault();
        }


        public IEnumerable<Cost> GetNewPost()
        {
            return CostRepository.GetAll().Where(p => p.CostCategoryId == 3).OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
