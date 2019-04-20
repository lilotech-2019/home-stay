using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.HMS;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomOrdersController : Controller
    {
        #region Fields
        private readonly IRoomOrderService _roomOrderService;
        private readonly IRoomService _roomService;
        #endregion

        #region Ctor
        public RoomOrdersController(IRoomOrderService roomOrderService, IRoomService roomService)
        {
            _roomOrderService = roomOrderService;
            _roomService = roomService;
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
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(null), "Id", "Name");
            return View(new RoomOrder());
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="roomOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomOrder roomOrder)
        {
            if (ModelState.IsValid)
            {
                roomOrder.Status = RoomOrderStatus.New;
                roomOrder.Total = roomOrder.Draff;
                _roomOrderService.Create(roomOrder);
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(null), "Id", "Name");
            return View(roomOrder);
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
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(null), "Id", "Name", roomOrder.RoomId);
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
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(null), "Id", "Name", roomOrder.RoomId);
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

        public ActionResult Checkout(int id)
        {
            var entity = _roomOrderService.FindById(id);
            return View(entity);
        }
    }
}