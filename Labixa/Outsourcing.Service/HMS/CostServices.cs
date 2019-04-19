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
        Costs GetCostById(int costId);
        void CreateCost(Costs cost);
        void EditCost(Costs costToEdit);
        void DeleteCost(int costId);
        void SaveCost();
        IEnumerable<ValidationResult> CanAddCost(string costUrl);

        Costs GetCostByUrlName(string urlName);

        IEnumerable<Costs> GetCostsByCategory(int costTypeId);

        IEnumerable<Costs> GetStaticPage();
        IEnumerable<Costs> GetNewPost();
    }
    public class CostService : ICostService
    {
        #region Field
        private readonly ICostRepository _costRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CostService(ICostRepository costRepository, IUnitOfWork unitOfWork)
        {
            this._costRepository = costRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for ICostService
        public IEnumerable<Costs> GetCosts()
        {
            var costs = _costRepository.GetAll().Where(p=>!p.IsDelete);
            return costs;
        }
        public IEnumerable<Costs> Get3CostsPosition()
        {
            var costs = _costRepository.GetAll();

            return costs;
        }
        public IEnumerable<Costs> GetHomePageCosts()
        {
            var costs = _costRepository.GetAll();

            return costs;
        }
        public IEnumerable<Costs> GetCostByCategoryId(int id)
        {
            var costs = _costRepository.GetAll();

            return costs;
        }
        public IEnumerable<Costs> GetCostByCategorySlug(string slug)
        {
            var costs = _costRepository.GetAll();

            return costs;
        }
        public IEnumerable<Costs> GetStaticPage()
        {
            var costs = _costRepository.GetAll();
            return costs;
        }

        public Costs GetCostById(int costId)
        {
            var cost = _costRepository.GetById(costId);
            return cost;
        }

        public void CreateCost(Costs cost)
        {
            _costRepository.Add(cost);
            SaveCost();
        }

        public void EditCost(Costs costToEdit)
        {
            //CostToEdit.LastEditedTime = DateTime.Now;
            _costRepository.Update(costToEdit);
            SaveCost();
        }

        public void DeleteCost(int costId)
        {
            //Get Cost by id.
            var cost = _costRepository.GetById(costId);
            if (cost != null)
            {
                cost.IsDelete = true;
                _costRepository.Update(cost);
                SaveCost();
            }
        }

        public void SaveCost()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddCost(string slug)
        {
            //Get Cost by url.
            var costs = _costRepository.GetAll();
            return null;
        }

        public Costs GetCostByUrlName(string urlName)
        {
            var costs = _costRepository.GetAll();

            return null;
        }

        public IEnumerable<Costs> GetCostsByCategory(int costTypeId)
        {
            var costs = this.GetCosts().Where(b => b.CostCategoryId == costTypeId);
            return costs;
        }

        public IEnumerable<Costs> Get6CostService()
        {
            var costs = this.GetCosts().Where(p => p.CostCategoryId == 6).Take(6);
            return costs;
        }
        public IEnumerable<Costs> Get2CostNews()
        {
            var costs = _costRepository.GetAll();
            return costs;
        }
        public IEnumerable<Costs> Get3CostNewsNewest()
        {
            var costs = _costRepository.GetAll();
            return costs;
        }
        #endregion


        public Costs GetCostContact()
        {
            var costs = _costRepository.GetAll();
            return costs.FirstOrDefault();
        }


        public IEnumerable<Costs> GetNewPost()
        {
            return _costRepository.GetAll().Where(p => p.CostCategoryId == 3).OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
