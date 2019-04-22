//using System;
//using System.Web.Mvc;
//using Outsourcing.Service.HMS;
//using Outsourcing.Data.Models.HMS;
//using Outsourcing.Core.Common;
//using Outsourcing.Core.Extensions;
//using Outsourcing.Core.Framework.Controllers;
//using Labixa.Areas.HMSAdmin.ViewModels;

//namespace Labixa.Areas.HMSAdmin.Controllers
//{
//    public class CostOrderController : BaseController
//    {
//        readonly ICostService _costService;
//        readonly ICostOrderItemService _costOrderItemService;
//        readonly ICostOrderService _costOrderService;
//        readonly IHotelService _hotelService;

//        public CostOrderController(ICostService costService,
//            ICostOrderItemService costOrderItemService,
//            ICostOrderService costOrderService,
//            IHotelService hotelService)
//        {
//            _costService = costService;
//            _costOrderItemService = costOrderItemService;
//            _costOrderService = costOrderService;
//            _hotelService = hotelService;
//        }

//        //
//        // GET: /HMSAdmin/CostOrder/
//        public ActionResult Index()
//        {
//            var model = _costOrderService.GetCostOrders();
//            return View(model);
//        }

//        public ActionResult Create()
//        {
//            CostOrderCreateModel model =
//                new CostOrderCreateModel {ListHotels = _hotelService.GetHotels().ToSelectListItems(-1)};
//            return View(model);
//        }

//        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
//        [ValidateInput(false)]
//        public ActionResult Create(CostOrderCreateModel newCost, bool continueEditing)
//        {
//            if (ModelState.IsValid)
//            {
//                var listCost = _costService.GetCosts();
//                var costOrder = newCost.CostOrderModel;
//                costOrder.Deleted = false;
//                costOrder.TotalPaymentRoom_DraftCheckIn = 0;
//                costOrder.TotalPayment_CheckOut = 0;
//                costOrder.Status = 0;
//                costOrder.ShipmentId = 0;
//                costOrder.ShipmentFee = 0;
//                costOrder.DateCreated = DateTime.Now.Date;
//                //Create Hotel
//                _costOrderService.CreateCostOrder(costOrder);
//                foreach (var item in listCost)
//                {
//                    CostOrderItem costItem = new CostOrderItem
//                    {
//                        CostId = item.Id,
//                        CostOrderId = costOrder.Id,
//                        Price = 0,
//                        Quantity = 0,
//                        Discount = 0
//                    };
//                    _costOrderItemService.CreateCostOrderItem(costItem);
//                }
//                return continueEditing
//                    ? RedirectToAction("Edit", "CostOrder", new {HotelId = costOrder.Id})
//                    : RedirectToAction("Index", "CostOrder");
//            }
//            else
//            {
//                newCost.ListHotels = _hotelService.GetHotels().ToSelectListItems(newCost.CostOrderModel.HotelId);
//                return View("Create", newCost);
//            }
//        }

//        public ActionResult Edit(int costOrderId)
//        {
//            CostOrderEditModel model = new CostOrderEditModel();
//            var listHotels = _hotelService.GetHotels();
//            _costService.GetCosts();
//            var costOrder = _costOrderService.GetCostOrderById(costOrderId);
//            model.CostOrderModel = costOrder;
//            model.ListHotels = listHotels.ToSelectListItems(costOrder.HotelId);

//            //if (costOrder.CostOrderItems != null && costOrder.CostOrderItems.Any())
//            //{
//            //    foreach (var item in costOrder.CostOrderItems)
//            //    {
//            //        CostOrderItemEditModel costItem = new CostOrderItemEditModel();
//            //        costItem.CostOrderItemModel = item;
//            //        costItem.ListCosts = listCost.ToSelectListItems(item.CostId);
//            //        model.ListCostOrderItemEditModels.Add(costItem);
//            //    }
//            //}
//            return View(model);
//        }

//        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
//        [ValidateInput(false)]
//        public ActionResult Edit(CostOrderEditModel editCostOrder, bool continueEditing)
//        {
//            if (ModelState.IsValid)
//            {
//                foreach (var item in editCostOrder.CostOrderModel.CostOrderItems)
//                {
//                    //item.CostOrderId = EditCostOrder.CostOrderModel.Id;
//                    _costOrderItemService.EditCostOrderItem(item);
//                }
//                var totalCost = Calculator.SumOfCostOrder(editCostOrder.CostOrderModel);
//                var costOrder = _costOrderService.GetCostOrderById(editCostOrder.CostOrderModel.Id);
//                costOrder.TotalPayment_CheckOut = totalCost;
//                costOrder.CustomerName = editCostOrder.CostOrderModel.CustomerName;
//                costOrder.Deadline = editCostOrder.CostOrderModel.Deadline;
//                costOrder.HotelId = editCostOrder.CostOrderModel.HotelId;
//                costOrder.CostOrderItems = editCostOrder.CostOrderModel.CostOrderItems;
//                _costOrderService.EditCostOrder(costOrder);
//                return continueEditing
//                    ? RedirectToAction("Edit", "CostOrder", new {CostOrderId = costOrder.Id})
//                    : RedirectToAction("Index", "CostOrder");
//            }
//            else
//            {
//                editCostOrder.ListHotels =
//                    _hotelService.GetHotels().ToSelectListItems(editCostOrder.CostOrderModel.HotelId);
//                return View("Edit", editCostOrder);
//            }
//        }
//    }
//}