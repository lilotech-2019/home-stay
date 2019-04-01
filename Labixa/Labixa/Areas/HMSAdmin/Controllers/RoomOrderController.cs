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
using WebGrease.Css.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Helpers;
using Labixa.Areas.HMSAdmin.ViewModels;
using System.Collections.ObjectModel;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomOrderController : Controller
    {
        #region Field

        readonly IRoomService _RoomService;
        readonly IRoomOrderService _RoomOrderService;
        readonly IHotelService _HotelService;
        readonly IRoomOrderItemService _RoomOrderItemService;


        #endregion
        public RoomOrderController(IRoomService RoomService,
         IHotelService HotelService, IRoomOrderItemService RoomOrderItemService, IRoomOrderService RoomOrderService)
        {
            this._HotelService = HotelService;
            this._RoomOrderItemService = RoomOrderItemService;
            this._RoomService = RoomService;
            this._RoomOrderService = RoomOrderService;
        }
        //
        // GET: /HMSAdmin/RoomOrder/
        public ActionResult Index()
        {
            var model = _RoomOrderService.GetRoomOrders();
            return View(model: model);
        }
        public ActionResult CreateRoomOrder(int? RoomId)
        {
            var _RoomOrder = new RoomOrder();
            if (!(RoomId == null))
            {

                var room = _RoomService.GetRoomById(int.Parse(RoomId.ToString()));
                _RoomOrder.CheckIn = DateTime.Now.Date;
                _RoomOrder.DateCreated = DateTime.Now.Date;
                _RoomOrder.Deadline = DateTime.Now.Date;
                _RoomOrder.Deleted = false;
                _RoomOrder.RoomId = room.Id;
                _RoomOrder.Status = 0;
                var total = Calculator.CalcNumOfDay(DateTime.Parse(_RoomOrder.CheckIn.ToString()), DateTime.Parse(_RoomOrder.Deadline.ToString()));
                _RoomOrder.TotalBookPrice = total * room.Price;
                _RoomOrderService.CreateRoomOrder(_RoomOrder);
                return View("CreateRoomOrder", _RoomOrder);
            }
            return View("CreateRoomOrder", _RoomOrder);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult CreateRoomOrder(RoomOrder roomOrder, bool continueEditing)
        {
            if (!(roomOrder == null))
            {
                var room = _RoomService.GetRoomById(int.Parse(roomOrder.RoomId.ToString()));

                var total = Calculator.CalcNumOfDay(DateTime.Parse(roomOrder.CheckIn.ToString()), DateTime.Parse(roomOrder.Deadline.ToString()));
                roomOrder.TotalBookPrice = double.Parse(roomOrder.TotalPaymentRoom_DraftCheckIn.ToString()) + (total * room.Price);
                _RoomOrderService.EditRoomOrder(roomOrder);
                return continueEditing ? RedirectToAction("Edit", "RoomOrder", new { RoomOrderId = roomOrder.Id })
                                 : RedirectToAction("Index", "RoomOrder");
            }
            return View("Edit", roomOrder);
        }
        public ActionResult Edit(int RoomOrderId)
        {
            var model = _RoomOrderService.GetRoomOrderById(RoomOrderId);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(RoomOrder RoomOrderEdit, bool continueEditing)
        {
            if (!(RoomOrderEdit == null))
            {
                var room = _RoomService.GetRoomById(int.Parse(RoomOrderEdit.RoomId.ToString()));

                var total = Calculator.CalcNumOfDay(DateTime.Parse(RoomOrderEdit.CheckIn.ToString()), DateTime.Parse(RoomOrderEdit.Deadline.ToString()));
                Room room1 = room;
                RoomOrderEdit.TotalBookPrice = double.Parse(RoomOrderEdit.TotalPaymentRoom_DraftCheckIn.ToString()) + (total * room1.Price);
                _RoomOrderService.EditRoomOrder(RoomOrderEdit);
                return continueEditing ? RedirectToAction("Edit", "RoomOrder", new { RoomOrderId = RoomOrderEdit.Id })
                                 : RedirectToAction("Index", "RoomOrder");
            }
            return View("Edit", RoomOrderEdit);
        }
        public ActionResult Status(int RoomOrderId, int status = -1)
        {
            var roomOrder = _RoomOrderService.GetRoomOrderById(RoomOrderId);
            if (status>-1)
            {
                if (status==0)
                {
                    roomOrder.Status = 1;
                    _RoomOrderService.EditRoomOrder(roomOrder);
                }
                else if(status==1)
                {
                    roomOrder.Status = 2;
                    _RoomOrderService.EditRoomOrder(roomOrder);
                }
            }
            return RedirectToAction("Index","RoomOrder");
        }
    }
}