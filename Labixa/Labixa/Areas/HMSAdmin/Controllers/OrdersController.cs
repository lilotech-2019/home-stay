using System.Net;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShipmentService _shipmentService;

        public OrdersController(IShipmentService shipmentService, IOrderService orderService)
        {
            _shipmentService = shipmentService;
            _orderService = orderService;
        }


        // GET: /HMSAdmin/Orders/
        public ActionResult Index()
        {
            var orders = _orderService.GetOrders();
            return View(orders);
        }

        // GET: /HMSAdmin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.GetOrderById((int) id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /HMSAdmin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.ShipmentId = new SelectList(_shipmentService.GetShipments(), "Id", "Note");
            return View(new Order());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.CreateOrder(order);

                return RedirectToAction("Index");
            }

            ViewBag.ShipmentId = new SelectList(_shipmentService.GetShipments(), "Id", "Note", order.ShipmentId);
            return View(order);
        }

        // GET: /HMSAdmin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.GetOrderById((int) id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipmentId = new SelectList(_shipmentService.GetShipments(), "Id", "Note", order.ShipmentId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.EditOrder(order);
                return RedirectToAction("Index");
            }
            ViewBag.ShipmentId = new SelectList(_shipmentService.GetShipments(), "Id", "Note", order.ShipmentId);
            return View(order);
        }

        // GET: /HMSAdmin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.GetOrderById((int) id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /HMSAdmin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}