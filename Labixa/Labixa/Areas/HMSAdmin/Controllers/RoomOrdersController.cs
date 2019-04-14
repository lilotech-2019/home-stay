using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Outsourcing.Data;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.HMS;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomOrdersController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        private readonly IRoomOrderService _roomOrderService;

        public RoomOrdersController(IRoomOrderService roomOrderService)
        {
            _roomOrderService = roomOrderService;
        }

        // GET: /HMSAdmin/RoomOrders/
        public async Task<ActionResult> Index()
        {
            //var roomOrders = _db.RoomOrders.Include(r => r.Room).Include(c => c.Customer).Where(w => w.Deleted == false)
            //    .OrderBy(o => o.Status);
            var roomOrders = _roomOrderService.FindAll().AsNoTracking();
            return View(await roomOrders.ToListAsync());
        }

        // GET: /HMSAdmin/RoomOrder/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomOrder = await _db.RoomOrders.FindAsync(id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            return View(roomOrder);
        }

        // GET: /HMSAdmin/RoomOrder/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name");
            return View(new RoomOrder());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoomOrder roomOrder)
        {
            if (ModelState.IsValid)
            {
                roomOrder.Status = RoomOrderStatus.New;
                roomOrder.Total = roomOrder.Draff;
                _db.RoomOrders.Add(roomOrder);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name", roomOrder.RoomId);
            return View(roomOrder);
        }

        // GET: /HMSAdmin/RoomOrder/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomOrder = await _db.RoomOrders.FindAsync(id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name", roomOrder.RoomId);
            return View(roomOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoomOrder roomOrder)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(roomOrder).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name", roomOrder.RoomId);
            return View(roomOrder);
        }

        // GET: /HMSAdmin/RoomOrder/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomOrder = await _db.RoomOrders.FindAsync(id);
            if (roomOrder == null)
            {
                return HttpNotFound();
            }
            return View(roomOrder);
        }

        // POST: /HMSAdmin/RoomOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var roomOrder = await _db.RoomOrders.FindAsync(id);
            _db.RoomOrders.Remove(roomOrder ?? throw new InvalidOperationException());
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}