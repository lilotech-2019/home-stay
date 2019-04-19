using Outsourcing.Core.Common;
using Outsourcing.Data;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.HMS;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomsController : Controller
    {
        #region Fields
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;
        #endregion

        #region Ctor
        public RoomsController(IRoomService roomService, IHotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var rooms = await _roomService.FindAll().AsNoTracking().ToListAsync();
            return View(rooms);
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
            Rooms rooms = _roomService.FindById((int)id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name");
            return View(new Rooms { SharePercent = 0 });
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                rooms.Slug = StringConvert.ConvertShortName(rooms.Name);
                _roomService.Create(rooms);
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name");
            return View(rooms);
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
            Rooms rooms = _roomService.FindById((int)id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name", rooms.HotelId);
            return View(rooms);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                rooms.Slug = StringConvert.ConvertShortName(rooms.Name);
                _roomService.Edit(rooms);
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name", rooms.HotelId);
            return View(rooms);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rooms = _roomService.FindById((int)id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rooms = _roomService.FindById(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            _roomService.Delete(rooms);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
