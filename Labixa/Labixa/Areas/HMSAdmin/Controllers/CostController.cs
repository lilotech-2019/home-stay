using System;
using System.Web.Mvc;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Areas.HMSAdmin.ViewModels;


namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class CostController : BaseController
    {
        readonly ICostService _costService;
        readonly ICostCategoryService _costCategoryService;
        public CostController(ICostService costService, ICostCategoryService costCategoryService)
        {
            _costCategoryService = costCategoryService;
            _costService = costService;
        }
        //
        // GET: /HMSAdmin/Cost/
        public ActionResult Index()
        {
            var model = _costService.GetCosts();
            return View(model);
        }
        public ActionResult Create()
        {
            CostModel model = new CostModel
            {
                ListCostCategory = _costCategoryService.GetProductCategories().ToSelectListItems(-1)
            };

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(CostModel newCost, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var cost = new Cost();
                cost = newCost.Cost;
                cost.IsDelete = false;
                cost.IsImport = false;
                cost.OrginalPrice = 0;
                cost.Price = 0;
                cost.Quantity = 0;
                cost.DateCreated = DateTime.Now.Date;
                //Create Hotel
                _costService.CreateCost(cost);
                return continueEditing ? RedirectToAction("Edit", "Cost", new { HotelId = cost.Id })
                                  : RedirectToAction("Index", "Cost");
            }
            else
            {
                newCost.ListCostCategory = _costCategoryService.GetProductCategories().ToSelectListItems(newCost.Cost.CostCategoryId);
                return View("Create", newCost);
            }
        }

        [HttpGet]
        public ActionResult Edit(int costId)
        {

            var cost = _costService.GetCostById(costId);
            //CostModel HotelFormModel = Mapper.Map<Hotel, CostModel>(Hotel);
            CostModel model = new CostModel();
            model.Cost = cost;

            model.ListCostCategory = _costCategoryService.GetProductCategories().ToSelectListItems(cost.CostCategoryId);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(CostModel costToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var cost = _costService.GetCostById(costToEdit.Cost.Id);
                cost.CostCategoryId = costToEdit.Cost.CostCategoryId;
                cost.Name = costToEdit.Cost.Name;
                cost.Description= costToEdit.Cost.Description;

                _costService.EditCost(cost);
                return continueEditing ? RedirectToAction("Edit", "Cost", new { HotelId = cost.Id })
                                 : RedirectToAction("Index", "Cost");
            }
            else
            {
                costToEdit.ListCostCategory = _costCategoryService.GetProductCategories().ToSelectListItems(costToEdit.Cost.CostCategoryId);
                return View("Edit", costToEdit);
            }
        }





        public ActionResult Delete(int costId)
        {
            _costService.DeleteCost(costId);
            return RedirectToAction("Index");
        }
    }
}