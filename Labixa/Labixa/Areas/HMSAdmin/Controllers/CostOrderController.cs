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
    public class CostOrderController : BaseController
    {
        readonly ICostService _CostService;
        readonly ICostOrderItemService _CostOrderItemService;
        readonly ICostOrderService _CostOrderService;
        readonly IHotelService _HotelService;
        public CostOrderController(ICostService CostService,
         ICostOrderItemService CostOrderItemService,
         ICostOrderService CostOrderService,
         IHotelService HotelService)
        {
            _CostService = CostService;
            _CostOrderItemService = CostOrderItemService;
            _CostOrderService = CostOrderService;
            _HotelService = HotelService;
        }
        //
        // GET: /HMSAdmin/CostOrder/
        public ActionResult Index()
        {
            var model = _CostOrderService.GetCostOrders();
            return View(model: model);
        }

        public ActionResult Create()
        {
            CostOrderCreateModel model = new CostOrderCreateModel();
            model.ListHotels = _HotelService.GetHotels().ToSelectListItems(-1);
            return View(model: model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(CostOrderCreateModel newCost, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var listCost = _CostService.GetCosts();
                var CostOrder = new CostOrder();
                CostOrder = newCost.CostOrderModel;
                CostOrder.Deleted = false;
                CostOrder.TotalPaymentRoom_DraftCheckIn = 0;
                CostOrder.TotalPayment_CheckOut = 0;
                CostOrder.Status = 0;
                CostOrder.ShipmentId = 0;
                CostOrder.ShipmentFee = 0;
                CostOrder.DateCreated = DateTime.Now.Date;
                //Create Hotel
                _CostOrderService.CreateCostOrder(CostOrder);
                foreach (var item in listCost)
                {
                    CostOrderItem costItem = new CostOrderItem();
                    costItem.CostId = item.Id;
                    costItem.CostOrderId = CostOrder.Id;
                    costItem.Price = 0;
                    costItem.Quantity = 0;
                    costItem.Discount = 0;
                    _CostOrderItemService.CreateCostOrderItem(costItem);
                }
                return continueEditing ? RedirectToAction("Edit", "CostOrder", new { HotelId = CostOrder.Id })
                                  : RedirectToAction("Index", "CostOrder");
            }
            else
            {
                newCost.ListHotels = _HotelService.GetHotels().ToSelectListItems(newCost.CostOrderModel.HotelId);
                return View("Create", newCost);
            }
        }

        public ActionResult Edit(int CostOrderId)
        {
            CostOrderEditModel model = new CostOrderEditModel();
            var listHotels = _HotelService.GetHotels();
            var listCost = _CostService.GetCosts();
            var costOrder = _CostOrderService.GetCostOrderById(CostOrderId);
            model.CostOrderModel = costOrder;
            model.ListHotels = listHotels.ToSelectListItems(costOrder.HotelId);

            //if (costOrder.CostOrderItems != null && costOrder.CostOrderItems.Any())
            //{
            //    foreach (var item in costOrder.CostOrderItems)
            //    {
            //        CostOrderItemEditModel costItem = new CostOrderItemEditModel();
            //        costItem.CostOrderItemModel = item;
            //        costItem.ListCosts = listCost.ToSelectListItems(item.CostId);
            //        model.ListCostOrderItemEditModels.Add(costItem);
            //    }
            //}
            return View(model: model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(CostOrderEditModel EditCostOrder, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in EditCostOrder.CostOrderModel.CostOrderItems)
                {
                    //item.CostOrderId = EditCostOrder.CostOrderModel.Id;
                    _CostOrderItemService.EditCostOrderItem(CostOrderItemToEdit: item);
                }
                var TotalCost = Calculator.SumOfCostOrder(EditCostOrder.CostOrderModel);
                var CostOrder = _CostOrderService.GetCostOrderById(EditCostOrder.CostOrderModel.Id);
                CostOrder.TotalPayment_CheckOut = TotalCost;
                CostOrder.CustomerName = EditCostOrder.CostOrderModel.CustomerName;
                CostOrder.Deadline= EditCostOrder.CostOrderModel.Deadline;
                CostOrder.HotelId= EditCostOrder.CostOrderModel.HotelId;
                CostOrder.CostOrderItems = EditCostOrder.CostOrderModel.CostOrderItems;
                _CostOrderService.EditCostOrder(CostOrder);
                return continueEditing ? RedirectToAction("Edit", "CostOrder", new { CostOrderId = CostOrder.Id })
                                 : RedirectToAction("Index", "CostOrder");
            }
            else
            {
                EditCostOrder.ListHotels = _HotelService.GetHotels().ToSelectListItems(EditCostOrder.CostOrderModel.HotelId);
                return View("Edit", EditCostOrder);
            }
        }
    }
}