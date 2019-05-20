using Labixa.Areas.Portal.ViewModels.RoomOrders;
using Outsourcing.Core.Email;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Outsourcing.Service;
using IHotelService = Outsourcing.Service.IHotelService;
using IRoomOrderService = Outsourcing.Service.IRoomOrderService;

namespace Labixa.Areas.Portal.Controllers
{
    [Authorize]
    public class RoomOrdersController : Controller
    {
        #region Fields

        private readonly IRoomOrderService _roomOrderService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;
        private readonly IHotelService _hotelService;

        #endregion

        #region Ctor

        public RoomOrdersController(IRoomOrderService roomOrderService, IRoomService roomService,
            ICustomerService customerService, IHotelService hotelService)
        {
            _roomOrderService = roomOrderService;
            _roomService = roomService;
            _customerService = customerService;
            _hotelService = hotelService;
        }

        #endregion

        #region UpdateStatus

        /// <summary>
        /// UpdateStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult UpdateStatus(int id, RoomOrderStatus status)
        {
            _roomOrderService.UpdateStatus(id, status);
            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Index

        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? hotelId)
        {
            var roomOrders = _roomOrderService.FindAll().AsNoTracking();
            if (hotelId != null)
            {
                var hotel = _hotelService.FindById((int) hotelId);
                roomOrders = roomOrders.Where(w => w.Room.Hotel.Id == hotel.Id);
            }
            return View(await roomOrders.ToListAsync());
        }

        #endregion

        #region Details

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomOrder = _roomOrderService.FindById((int) id);
            roomOrder.Total = _roomOrderService.GetTotalPrice((int)id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            
            return View(roomOrder);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int? hotelId, int? roomId, string phone)
        {
            Room room;
            if (roomId != null)
            {
                room = _roomService.FindById((int) roomId);
                ViewBag.RoomId = new SelectList(new List<Room> {room}, "Id", "Name");
            }
            else
            {
                return RedirectToAction("Index", "Rooms");
            }

            var entity = new CreateViewModel
            {
                Customer = new Customer(),
                RoomOrders = new RoomOrder {Price = room.Price, RoomId = room.Id}
            };

            if (phone != "")
            {
                var customer = _customerService.FindByPhone(phone);
                if (customer != null)
                {
                    entity.RoomOrders.CustomerId = customer.Id;
                }
                entity.Customer = customer;
            }
            ViewBag.Phone = phone;

            return View(entity);
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var roomOrder = viewModel.RoomOrders;
                roomOrder.OrderStatus = RoomOrderStatus.New;
                var customer = _customerService.FindByPhone(viewModel.Customer.Phone);
                if (customer == null)
                {
                    roomOrder.Customer = viewModel.Customer;
                }
                else
                {
                    roomOrder.CustomerId = customer.Id;
                    customer.Name = viewModel.Customer.Name?.Trim() ?? "";
                    customer.Address = viewModel.Customer.Address?.Trim() ?? "";
                    customer.Phone = viewModel.Customer.Phone?.Trim() ?? "";
                    customer.Email = viewModel.Customer.Email?.Trim() ?? "";
                    customer.LastModify = DateTime.Now;
                    _customerService.Edit(customer);
                }


                _roomOrderService.Create(roomOrder);

                //====================<Mail>==============================
                string subject = "Đặt phòng thành công";

                //TODO a để cái này để fix tạm bug của em. tìm coi còn cách nào làm gọn code chỗ này lại nhé.
                // TOTO bug xuất hiện khi em không tìm thấy customer. nếu em tìm thấy thì nó gửi mail ngon lành.
                //TODO THANHPTP
                customer = viewModel.Customer;

                string content = "<html><head><style type='text/css'>" +
                                 ".mail{width: 100%; height: 100% ; background-color: #f5f5f5f5; float: left; background-image: url('https://i.ibb.co/7CL0frY/1.jpg')}" +
                                 ".content-mail{width: 60%; background-color: #ffffff; float: left; margin: 100px 20%; border: 1px solid gray;}.logo-img{padding: 2% 5% 0px 5%;}" +
                                 ".logo-img img{height: 50px; width: 173px}.content-mail table  {margin: 5% 25% 5% 17%;}.content-mail table tr{margin-bottom: 5%; display: grid;}" +
                                 ".content-mail table tr th {font-size: 20px; text-align: left;}.content-mail table tr td {font-size: 30px; } </style></head>" +
                                 "<div class='mail'>" +
                                 "<div class='content-mail'>" +
                                 "<div class='logo-img'>" +
                                 "<img src='https://i.ibb.co/5vwLsTR/logo2.png' alt='logo2' border='0'>" +
                                 "</div>" +
                                 "<table>" +
                                 "<tr>" +
                                 "<th>Họ và Tên Khách Hàng: </th>" +
                                 "<td>" + customer.Name + "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                 "<th>Ngày CheckIn: </th>" +
                                 "<td>" + roomOrder.CheckIn.ToString("dd/MM/yyyy") + "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                 "<th>Ngày CheckOut: </th>" +
                                 "<td>" + roomOrder.CheckOut.ToString("dd/MM/yyyy") + "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                 "<th>Email Khách Hàng: </th>" +
                                 "<td>" + customer.Email + "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                 "<th>Số Điện Thoại: </th>" +
                                 "<td>" + customer.Phone + "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                 "<th>Số Lượng Người: </th>" +
                                 "<td>" + roomOrder.AmountOfPeople + "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                 "<th>Tạm Tính </th>" +
                                 "<td>" + roomOrder.Price + "</td>" +
                                 "</tr>" +
                                 "</table></div></div></html>";
                await EmailHelper.SendEmailAsync(customer.Email, content, subject);

                //====================</Mail>==============================

                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(), "Id", "Name");
            return View(viewModel);
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomOrder = _roomOrderService.FindById((int) id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomOrder.RoomId), "Id", "Name",
                roomOrder.RoomId);
            return View(roomOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(RoomOrder roomOrder)
        {
            if (ModelState.IsValid)
            {
                _roomOrderService.Edit(roomOrder);
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomOrder.RoomId), "Id", "Name",
                roomOrder.RoomId);
            return View(roomOrder);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomOrder = _roomOrderService.FindById((int) id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            return View(roomOrder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var roomOrders = _roomOrderService.FindById(id);
            if (roomOrders == null)
            {
                return HttpNotFound();
            }
            _roomOrderService.Delete(roomOrders);
            return RedirectToAction("Index");
        }

        #endregion

        #region Checkout

        /// <summary>
        /// Checkout
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Checkout(int id)
        {
            var entity = _roomOrderService.FindById(id);
            entity.Total = _roomOrderService.GetTotalPrice(id);
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(), "Id", "Name");

            return View(entity);
        }

        #endregion

        #region Preview

        /// <summary>
        /// Preview
        /// </summary>
        /// <param name="roomOrder"></param>
        /// <returns></returns>
        public ActionResult Preview(RoomOrder roomOrder)
        {
            var entity = roomOrder;
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomOrder.RoomId), "Id", "Name",
                roomOrder.RoomId);
            entity.Total = _roomOrderService.GetTotalPrice(roomOrder.Id);
            return View(entity);
        }

        /// <summary>
        /// Preview - POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Preview(int id)
        {
            var entity = _roomOrderService.FindById(id);
            entity.OrderStatus = RoomOrderStatus.CheckOut;

            _roomOrderService.Edit(entity);
            return RedirectToAction("Index");
        }

        #endregion
    }
}