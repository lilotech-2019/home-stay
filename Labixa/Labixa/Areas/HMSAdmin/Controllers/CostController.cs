using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Helpers;
using Labixa.Areas.HMSAdmin.ViewModels;


namespace Labixa.Areas.HMSAdmin.Controllers
{
    public partial class CostController : BaseController
    {
        readonly ICostService _CostService;
        readonly ICostCategoryService _CostCategoryService;
        public CostController(ICostService CostService, ICostCategoryService CostCategoryService)
        {
            _CostCategoryService = CostCategoryService;
            _CostService = CostService;
        }
        //
        // GET: /HMSAdmin/Cost/
        public ActionResult Index()
        {
            var model = _CostService.GetCosts();
            return View(model);
        }
        public ActionResult Create()
        {
            CostModel model = new CostModel();
           
            model.ListCostCategory = _CostCategoryService.GetProductCategories().ToSelectListItems(-1);
            return View(model: model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(CostModel newCost, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Cost = new Cost();
                Cost = newCost.Cost;
                Cost.IsDelete = false;
                Cost.isImport = false;
                Cost.OrginalPrice = 0;
                Cost.Price = 0;
                Cost.Quantity = 0;
                Cost.DateCreated = DateTime.Now.Date;
                //Create Hotel
                _CostService.CreateCost(Cost);
                return continueEditing ? RedirectToAction("Edit", "Cost", new { HotelId = Cost.Id })
                                  : RedirectToAction("Index", "Cost");
            }
            else
            {
                newCost.ListCostCategory = _CostCategoryService.GetProductCategories().ToSelectListItems(newCost.Cost.CostCategoryId);
                return View("Create", newCost);
            }
        }

        [HttpGet]
        public ActionResult Edit(int CostId)
        {

            var Cost = _CostService.GetCostById(CostId);
            //CostModel HotelFormModel = Mapper.Map<Hotel, CostModel>(Hotel);
            CostModel model = new CostModel();
            model.Cost = Cost;

            model.ListCostCategory = _CostCategoryService.GetProductCategories().ToSelectListItems(Cost.CostCategoryId);

            return View(model: model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(CostModel CostToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Cost = _CostService.GetCostById(CostToEdit.Cost.Id);
                Cost.CostCategoryId = CostToEdit.Cost.CostCategoryId;
                Cost.Name = CostToEdit.Cost.Name;
                Cost.Description= CostToEdit.Cost.Description;

                _CostService.EditCost(Cost);
                return continueEditing ? RedirectToAction("Edit", "Cost", new { HotelId = Cost.Id })
                                 : RedirectToAction("Index", "Cost");
            }
            else
            {
                CostToEdit.ListCostCategory = _CostCategoryService.GetProductCategories().ToSelectListItems(CostToEdit.Cost.CostCategoryId);
                return View("Edit", CostToEdit);
            }
        }





        public ActionResult Delete(int CostId)
        {
            _CostService.DeleteCost(CostId);
            return RedirectToAction("Index");
        }
    }
}