﻿using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Outsourcing.Data.Models.HMS;

namespace Labixa.Areas.Portal.Controllers
{
    public class RoomsController : Controller
    {
        #region Fields

        private readonly IRoomService _roomService;
        private readonly Outsourcing.Service.HMS.IRoomImageService _roomImageService;
        private readonly Outsourcing.Service.HMS.IRoomImageMappingService _roomImageMappingService;
        private readonly IHotelService _hotelService;

        #endregion

        #region Ctor

        public RoomsController(IRoomService roomService, IHotelService hotelService, Outsourcing.Service.HMS.IRoomImageMappingService roomImageMappingService, Outsourcing.Service.HMS.IRoomImageService roomImageService)
        {
            _roomImageService = roomImageService;
            _roomImageMappingService = roomImageMappingService;
            _roomService = roomService;
            _hotelService = hotelService;
        }

        #endregion

        #region Index

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? hotelId, RoomType? type)
        {
            var rooms = _roomService.FindAll();
            if (hotelId != null)
            {
                rooms = rooms.Where(w => w.HotelId == hotelId);
            }
            if (type != null) {
                rooms = rooms = rooms.Where(w => w.Type == type);
            }

            return View(await rooms.AsNoTracking().ToListAsync());
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
            Room room = _roomService.FindById((int) id);
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
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public ActionResult Create(int? hotelId)
        {
            var roomImage = new List<RoomImageMappings>
            {
                new RoomImageMappings {IsMainPicture = true, Title = "Cover"},
                new RoomImageMappings {IsMainPicture = false, Title = "1"},
                new RoomImageMappings {IsMainPicture = false, Title = "2"},
                new RoomImageMappings {IsMainPicture = false, Title = "3"},
                new RoomImageMappings {IsMainPicture = false, Title = "4"},
                new RoomImageMappings {IsMainPicture = false, Title = "5"}
            };
            var room = new Room
            {
                SharePercent = 0,
                //   RoomAssets = roomAssets,
                RoomImageMappings = roomImage
            };
            var hotels = _hotelService.FindSelectList();
            if (hotelId != null)
            {
                ViewBag.HotelId = new SelectList(hotels.Where(w => w.Id == hotelId), "Id", "Name", hotelId);
                ViewBag.HotelCategoryId = _hotelService.FindById((int) hotelId).HotelCategoryId;
                room.HotelId = (int) hotelId;
                return View(room);
            }
            ViewBag.HotelId = new SelectList(hotels, "Id", "Name");
            return View(room);
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(int? hotelId, RoomType? type, Room room)
        {
            room.Slug = StringConvert.ConvertShortName(room.Name);
            room.SlugEnglish = StringConvert.ConvertShortName(room.NameEnglish);
            _roomService.Create(room);
            return RedirectToAction("Index", new { hotelId = room.HotelId, type = type });
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id,int? hotelId, RoomType? type)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _roomService.FindById((int) id);
            if (room == null)
            {
                return HttpNotFound();
            }
            var hotels = _hotelService.FindSelectList();
            if (hotelId != null) {
                hotels = hotels.Where(w => w.Id == hotelId);
            }

            var rooms = _roomService.FindSelectList();
            if (type != null)
            {
                rooms = rooms.Where(w => w.Type == type);
            }

            ViewBag.RoomType = new SelectList(rooms, "Id", "Name");
            ViewBag.HotelId = new SelectList(hotels, "Id", "Name");
            return View(room);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Room room, int? hotelId, RoomType? type)
        {
            if (ModelState.IsValid)
            {
                room.Slug = StringConvert.ConvertShortName(room.Name);

                foreach (RoomImageMappings image in room.RoomImageMappings)
                {
                    _roomImageService.EditRoomImage(image.RoomImage);
                    _roomImageMappingService.EditRoomImageMapping(image);
                }

                _roomService.Edit(room);


                return RedirectToAction("Index", new { hotelId, type });
            }
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(), "Id", "Name");
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
            var rooms = _roomService.FindById((int) id);
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