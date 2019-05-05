using Labixa.Areas.Portal.ViewModels.RoomOrders;
using Labixa.Areas.Portal.ViewModels.Rooms;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class RoomOrdersController : Controller
    {
        #region Fields
        private readonly IRoomOrderService _roomOrderService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;
        #endregion

        #region Ctor
        public RoomOrdersController(IRoomOrderService roomOrderService, IRoomService roomService, ICustomerService customerService)
        {
            _roomOrderService = roomOrderService;
            _roomService = roomService;
            _customerService = customerService;
        }
        #endregion

        #region UpdateStatus
        /// <summary>
        /// UpdateStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult UpdateStatus(int id, int status)
        {
            _roomOrderService.UpdateStatus(id, (RoomOrderStatus)status);
            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Index
        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var roomOrders = _roomOrderService.FindAll().AsNoTracking();
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
            var roomOrder = _roomOrderService.FindById((int)id);
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
        public ActionResult Create(int? roomId, string phone)
        {
            var room = new Room();
            if (roomId != null)
            {
                room = _roomService.FindById((int)roomId);
                ViewBag.RoomId = new SelectList(new List<Room> { room }, "Id", "Name");
            }
            else {
                ViewBag.RoomId = new SelectList(_roomService.FindSelectList(), "Id", "Name");
            }

            var entity = new CreateViewModel { Customer = new Customer(), RoomOrders = new RoomOrder { Price = room.Price, RoomId = room.Id } };

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
        public ActionResult Create(CreateViewModel viewModel)
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
                    customer.Name = viewModel.Customer.Name;
                    customer.Address = viewModel.Customer.Address;
                    customer.Phone = viewModel.Customer.Phone;
                    customer.Email = viewModel.Customer.Email;
                    customer.LastModify = DateTime.Now;
                    _customerService.Edit(customer);
                }

                _roomOrderService.Create(roomOrder);
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
            var roomOrder = _roomOrderService.FindById((int)id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomOrder.RoomId), "Id", "Name", roomOrder.RoomId);
            return View(roomOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomOrder roomOrder)
        {
            if (ModelState.IsValid)
            {
                _roomOrderService.Edit(roomOrder);
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomOrder.RoomId), "Id", "Name", roomOrder.RoomId);
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
            var roomOrder = _roomOrderService.FindById((int)id);
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
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomOrder.RoomId), "Id", "Name", roomOrder.RoomId);
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

        public ActionResult PartialSubMenuCategory()
        {
            var data = _roomOrderService.FindAll().AsNoTracking();
            var viewModel = new PartialSubMenuCategoryViewModel
            {
                Count = data.Count(),
                RoomOrders = data
            };
            return PartialView("_PartialSubMenuCategory", viewModel);
        }
    }
}