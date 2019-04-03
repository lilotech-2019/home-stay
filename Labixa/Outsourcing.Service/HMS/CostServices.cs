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

        IEnumerable<Costs> GetCosts();
        Costs GetCostContact();
        IEnumerable<Costs> GetHomePageCosts();
        IEnumerable<Costs> GetCostByCategorySlug(string slug);
        IEnumerable<Costs> GetCostByCategoryId(int id);
        IEnumerable<Costs> Get6CostService();
        IEnumerable<Costs> Get2CostNews();
        IEnumerable<Costs> Get3CostNewsNewest();
        Costs GetCostById(int CostId);
        void CreateCost(Costs Cost);
        void EditCost(Costs CostToEdit);
        void DeleteCost(int CostId);
        void SaveCost();
        IEnumerable<ValidationResult> CanAddCost(string CostUrl);

        Costs GetCostByUrlName(string urlName);

        IEnumerable<Costs> GetCostsByCategory(int CostTypeId);

        IEnumerable<Costs> GetStaticPage();
        IEnumerable<Costs> GetNewPost();
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
        public IEnumerable<Costs> GetCosts()
        {
            var Costs = CostRepository.GetAll().Where(p=>!p.IsDelete);
            return Costs;
        }
        public IEnumerable<Costs> Get3CostsPosition()
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Costs> GetHomePageCosts()
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Costs> GetCostByCategoryId(int id)
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Costs> GetCostByCategorySlug(string slug)
        {
            var Costs = CostRepository.GetAll();

            return Costs;
        }
        public IEnumerable<Costs> GetStaticPage()
        {
            var Costs = CostRepository.GetAll();
            return Costs;
        }

        public Costs GetCostById(int CostId)
        {
            var Cost = CostRepository.GetById(CostId);
            return Cost;
        }

        public void CreateCost(Costs Cost)
        {
            CostRepository.Add(Cost);
            SaveCost();
        }

        public void EditCost(Costs CostToEdit)
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

        public Costs GetCostByUrlName(string urlName)
        {
            var Costs = CostRepository.GetAll();

            return null;
        }

        public IEnumerable<Costs> GetCostsByCategory(int CostTypeId)
        {
            var Costs = this.GetCosts().Where(b => b.CostCategoryId == CostTypeId);
            return Costs;
        }

        public IEnumerable<Costs> Get6CostService()
        {
            var Costs = this.GetCosts().Where(p => p.CostCategoryId == 6).Take(6);
            return Costs;
        }
        public IEnumerable<Costs> Get2CostNews()
        {
            var Costs = CostRepository.GetAll();
            return Costs;
        }
        public IEnumerable<Costs> Get3CostNewsNewest()
        {
            var Costs = CostRepository.GetAll();
            return Costs;
        }
        #endregion


        public Costs GetCostContact()
        {
            var Costs = CostRepository.GetAll();
            return Costs.FirstOrDefault();
        }


        public IEnumerable<Costs> GetNewPost()
        {
            return CostRepository.GetAll().Where(p => p.CostCategoryId == 3).OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
