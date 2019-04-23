using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
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
        /// Index
        /// </summary>
        /// <param name="hotelId">Room Id</param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? hotelId)
        {
            IEnumerable<Room> rooms;
            if (hotelId == null)
            {
                rooms = await _roomService.FindAll().AsNoTracking().ToListAsync();
            }
            else
            {
                rooms = await _roomService.FindByHotelId((int)hotelId).AsNoTracking().ToListAsync();
            }
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
            Room room = _roomService.FindById((int)id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(int? hotelId)
        {
            if (hotelId != null)
            {
                ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(hotelId), "Id", "Name", hotelId);
                ViewBag.HotelCategoryId = _hotelService.FindById((int)hotelId).HotelCategoryId;
                return View(new Room { SharePercent = 0, HotelId = (int)hotelId });
            }
            else {
                ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(), "Id", "Name");
                return View(new Room { SharePercent = 0 });
            }
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                var asset = new List<Asset>();
                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "DryHair",
                        Price = 1,
                        Quantity = "1 Cái"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "TiVi",
                        Price = 1,
                        Quantity = "1 Cái"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "TuDo",
                        Price = 1,
                        Quantity = "1 Cái"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "HotWater",
                        Price = 1,
                        Quantity = "1 khối"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "Iron",
                        Price = 1,
                        Quantity = "1 Cái"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "Kitchen",
                        Price = 1,
                        Quantity = "1 Cái"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "TeaCoffee",
                        Price = 1,
                        Quantity = "1 ly"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "Snack",
                        Price = 1,
                        Quantity = "1 bịch"
                    });
                }

                if (room.Utility_DryHair)
                {
                    asset.Add(new Asset
                    {
                        Name = "WashMachine",
                        Price = 1,
                        Quantity = "1 Cái"
                    });
                }

                room.Slug = StringConvert.ConvertShortName(room.Name);
                room.Assets = asset;
                _roomService.Create(room);
                var hotelCategoryId = _hotelService.FindById(room.HotelId).HotelCategoryId;
                return RedirectToAction("Index", "Hotels", hotelCategoryId);
            }

            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name");
            return View(room);
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
            Room room = _roomService.FindById((int)id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name", room.HotelId);
            return View(room);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                room.Slug = StringConvert.ConvertShortName(room.Name);
                _roomService.Edit(room);
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name", room.HotelId);
            return View(room);
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
