using System;
using System.Web.Mvc;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Common;
using Outsourcing.Core.Framework.Controllers;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomOrderController : Controller
    {
        #region Field

        readonly IRoomService _roomService;
        readonly IRoomOrderService _roomOrderService;
        readonly IHotelService _hotelService;
        readonly IRoomOrderItemService _roomOrderItemService;


        #endregion
        public RoomOrderController(IRoomService roomService,
         IHotelService hotelService, IRoomOrderItemService roomOrderItemService, IRoomOrderService roomOrderService)
        {
            this._hotelService = hotelService;
            this._roomOrderItemService = roomOrderItemService;
            this._roomService = roomService;
            this._roomOrderService = roomOrderService;
        }
        //
        // GET: /HMSAdmin/RoomOrder/
        public ActionResult Index()
        {
            var model = _roomOrderService.GetRoomOrders();
            return View(model);
        }
        public ActionResult CreateRoomOrder(int? roomId)
        {
            var roomOrder = new RoomOrders();
            if (!(roomId == null))
            {

                var room = _roomService.GetRoomById(int.Parse(roomId.ToString()));
                roomOrder.CheckIn = DateTime.Now.Date;
                roomOrder.DateCreated = DateTime.Now.Date;
                roomOrder.Deadline = DateTime.Now.Date;
                roomOrder.Deleted = false;
                roomOrder.RoomId = room.Id;
                roomOrder.Status = 0;
                var total = Calculator.CalcNumOfDay(DateTime.Parse(roomOrder.CheckIn.ToString()), DateTime.Parse(roomOrder.Deadline.ToString()));
                roomOrder.TotalBookPrice = total * room.Price;
                _roomOrderService.CreateRoomOrder(roomOrder);
                return View("CreateRoomOrder", roomOrder);
            }
            return View("CreateRoomOrder", roomOrder);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult CreateRoomOrder(RoomOrders roomOrder, bool continueEditing)
        {
            if (!(roomOrder == null))
            {
                var room = _roomService.GetRoomById(int.Parse(roomOrder.RoomId.ToString()));

                var total = Calculator.CalcNumOfDay(DateTime.Parse(roomOrder.CheckIn.ToString()), DateTime.Parse(roomOrder.Deadline.ToString()));
                roomOrder.TotalBookPrice = double.Parse(roomOrder.TotalPaymentRoom_DraftCheckIn.ToString()) + (total * room.Price);
                _roomOrderService.EditRoomOrder(roomOrder);
                return continueEditing ? RedirectToAction("Edit", "RoomOrder", new { RoomOrderId = roomOrder.Id })
                                 : RedirectToAction("Index", "RoomOrder");
            }
            return View("Edit", roomOrder);
        }
        public ActionResult Edit(int roomOrderId)
        {
            var model = _roomOrderService.GetRoomOrderById(roomOrderId);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(RoomOrders roomOrderEdit, bool continueEditing)
        {
            if (!(roomOrderEdit == null))
            {
                var room = _roomService.GetRoomById(int.Parse(roomOrderEdit.RoomId.ToString()));

                var total = Calculator.CalcNumOfDay(DateTime.Parse(roomOrderEdit.CheckIn.ToString()), DateTime.Parse(roomOrderEdit.Deadline.ToString()));
                Rooms room1 = room;
                roomOrderEdit.TotalBookPrice = double.Parse(roomOrderEdit.TotalPaymentRoom_DraftCheckIn.ToString()) + (total * room1.Price);
                _roomOrderService.EditRoomOrder(roomOrderEdit);
                return continueEditing ? RedirectToAction("Edit", "RoomOrder", new { RoomOrderId = roomOrderEdit.Id })
                                 : RedirectToAction("Index", "RoomOrder");
            }
            return View("Edit", roomOrderEdit);
        }
        public ActionResult Status(int roomOrderId, int status = -1)
        {
            var roomOrder = _roomOrderService.GetRoomOrderById(roomOrderId);
            if (status>-1)
            {
                if (status==0)
                {
                    roomOrder.Status = 1;
                    _roomOrderService.EditRoomOrder(roomOrder);
                }
                else if(status==1)
                {
                    roomOrder.Status = 2;
                    _roomOrderService.EditRoomOrder(roomOrder);
                }
            }
            return RedirectToAction("Index","RoomOrder");
        }
    }
}