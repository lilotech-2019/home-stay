//using System.Collections.Generic;
//using System.Linq;
//using Outsourcing.Core.Common;
//using Outsourcing.Data.Models.HMS;
//using Outsourcing.Data.Infrastructure;
//using Outsourcing.Data.Repository.HMS;
//using Outsourcing.Service.Portal.Base;

//namespace Outsourcing.Service.HMS
//{
//    public interface ICostService 
//    {

//        IEnumerable<Cost> GetCosts();
//        Cost GetCostContact();
//        IEnumerable<Cost> GetHomePageCosts();
//        IEnumerable<Cost> GetCostByCategorySlug(string slug);
//        IEnumerable<Cost> GetCostByCategoryId(int id);
//        IEnumerable<Cost> Get6CostService();
//        IEnumerable<Cost> Get2CostNews();
//        IEnumerable<Cost> Get3CostNewsNewest();
//        Cost GetCostById(int costId);
//        void CreateCost(Cost cost);
//        void EditCost(Cost costToEdit);
//        void DeleteCost(int costId);
//        void SaveCost();
//        IEnumerable<ValidationResult> CanAddCost(string costUrl);

//        Cost GetCostByUrlName(string urlName);

//        IEnumerable<Cost> GetCostsByCategory(int costTypeId);

//        IEnumerable<Cost> GetStaticPage();
//        IEnumerable<Cost> GetNewPost();
//    }
//    public class CostService : ICostService
//    {
//        #region Field
//        private readonly ICostRepository _costRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        #endregion

//        #region Ctor
//        public CostService(ICostRepository costRepository, IUnitOfWork unitOfWork)
//        {
//            this._costRepository = costRepository;
//            this._unitOfWork = unitOfWork;
//        }
//        #endregion

//        #region Implementation for ICostService
//        public IEnumerable<Cost> GetCosts()
//        {
//            var costs = _costRepository.GetAll().Where(p => !p.Deleted);
//            return costs;
//        }
//        public IEnumerable<Cost> Get3CostsPosition()
//        {
//            var costs = _costRepository.GetAll();

//            return costs;
//        }
//        public IEnumerable<Cost> GetHomePageCosts()
//        {
//            var costs = _costRepository.GetAll();

//            return costs;
//        }
//        public IEnumerable<Cost> GetCostByCategoryId(int id)
//        {
//            var costs = _costRepository.GetAll();

//            return costs;
//        }
//        public IEnumerable<Cost> GetCostByCategorySlug(string slug)
//        {
//            var costs = _costRepository.FindBy();

//            return costs;
//        }
//        public IEnumerable<Cost> GetStaticPage()
//        {
//            var costs = _costRepository.GetAll();
//            return costs;
//        }

//        public Cost GetCostById(int costId)
//        {
//            var cost = _costRepository.GetById(costId);
//            return cost;
//        }

//        public void CreateCost(Cost cost)
//        {
//            _costRepository.Add(cost);
//            SaveCost();
//        }

//        public void EditCost(Cost costToEdit)
//        {
//            //CostToEdit.LastEditedTime = DateTime.Now;
//            _costRepository.Update(costToEdit);
//            SaveCost();
//        }

//        public void DeleteCost(int costId)
//        {
//            //Get Cost by id.
//            var cost = _costRepository.GetById(costId);
//            if (cost != null)
//            {
//                cost.Deleted = true;
//                _costRepository.Update(cost);
//                SaveCost();
//            }
//        }

//        public void SaveCost()
//        {
//            _unitOfWork.Commit();
//        }

//        public IEnumerable<ValidationResult> CanAddCost(string slug)
//        {
//            //Get Cost by url.
//            var costs = _costRepository.GetAll();
//            return null;
//        }

//        public Cost GetCostByUrlName(string urlName)
//        {
//            var costs = _costRepository.GetAll();

//            return null;
//        }

//        public IEnumerable<Cost> GetCostsByCategory(int costTypeId)
//        {
//            var costs = this.GetCosts().Where(b => b.CostCategoryId == costTypeId);
//            return costs;
//        }

//        public IEnumerable<Cost> Get6CostService()
//        {
//            var costs = this.GetCosts().Where(p => p.CostCategoryId == 6).Take(6);
//            return costs;
//        }
//        public IEnumerable<Cost> Get2CostNews()
//        {
//            var costs = _costRepository.GetAll();
//            return costs;
//        }
//        public IEnumerable<Cost> Get3CostNewsNewest()
//        {
//            var costs = _costRepository.GetAll();
//            return costs;
//        }
//        #endregion


//        public Cost GetCostContact()
//        {
//            var costs = _costRepository.GetAll();
//            return costs.FirstOrDefault();
//        }


//        public IEnumerable<Cost> GetNewPost()
//        {
//            return _costRepository.GetAll().Where(p => p.CostCategoryId == 3).OrderByDescending(p => p.DateCreated).Take(5);
//        }
//    }
//}
