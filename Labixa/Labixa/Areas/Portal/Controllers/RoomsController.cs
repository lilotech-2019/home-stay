﻿using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using System.Data.Entity;
using Outsourcing.Service;
using Labixa.Areas.Portal.ViewModels.Rooms;
using PagedList;

namespace Labixa.Areas.Portal.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        #region Fields

        private readonly IRoomService _roomService;
        private readonly IRoomImageService _roomImageService;
        private readonly IRoomImageMappingService _roomImageMappingService;
        private readonly IHotelService _hotelService;

        #endregion

        #region Ctor

        public RoomsController(IRoomService roomService, IHotelService hotelService,
            IRoomImageMappingService roomImageMappingService, IRoomImageService roomImageService)
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
        /// <param name="type">Room Type</param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? hotelId, RoomType? type)
        {
            IQueryable<Room> rooms = _roomService.FindAll();

            if (hotelId != null)
            {
                rooms = _roomService.FindAll().Where(w => w.HotelId == hotelId);
            }
            if (type != null)
            {
                rooms = rooms.Where(w => w.Type == type);
            }
            if (User.IsInRole(Role.Admin)) return View(await rooms.AsNoTracking().ToListAsync());

            rooms = rooms.Where(w => w.Hotel.HostEmail == User.Identity.Name);
            return View(await rooms.AsNoTracking().ToListAsync());
        }

        #endregion

        #region Details

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _roomService.FindById((int)id);
            if (room == null || room.Hotel.HostEmail != User.Identity.Name)
            {
                return HttpNotFound();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            RoomDetailsSubMenuViewModel roomDetailsSubMenuViewModel = new RoomDetailsSubMenuViewModel
            {
                Room = room,
                RoomOrders = room.RoomOrders.Where(w => w.Deleted != true).ToPagedList(pageNumber, pageSize)
            };

            return View(roomDetailsSubMenuViewModel);
        }

        #endregion

        #region RoomDetailsSubMenu

        /// <summary>
        /// RoomDetailsSubMenu
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomDetailsSubMenu(int id)
        {
            var room = _roomService.FindById(id);
            return PartialView("_RoomDetailsSubMenu", room);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Create(int? hotelId, RoomType? type)
        {
            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
            if (type != null)
            {
                roomTypes = new List<RoomType> { (RoomType)type };
            }

            ViewBag.RoomType = new SelectList(roomTypes, type);

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
            if (User.IsInRole(Role.Admin))
            {
                ViewBag.HotelId = new SelectList(hotels, "Id", "Name", hotelId);
                return View(room);
            }
            if (hotelId == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(hotels.Where(w => w.Id == hotelId), "Id", "Name", hotelId);
            ViewBag.HotelCategoryId = _hotelService.FindById((int)hotelId).HotelCategoryId;
            room.HotelId = (int)hotelId;
            return View(room);
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="type"></param>
        /// <param name="room"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(int? hotelId, RoomType? type, Room room)
        {
            room.Slug = StringConvert.ConvertShortName(room.Name);
            room.SlugEnglish = StringConvert.ConvertShortName(room.NameEnglish);
            _roomService.Create(room);
            if (hotelId != null) {
                return RedirectToAction("Details","Hotels", new { hotelId = room.HotelId });
            }
            return RedirectToAction("Index", new { hotelId = room.HotelId, type });
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit - GET
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotelId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id, int? hotelId, RoomType? type)
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
            var hotels = _hotelService.FindSelectList();
            if (hotelId != null)
            {
                hotels = hotels.Where(w => w.Id == hotelId);
            }

            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
            if (type != null)
            {
                roomTypes = new List<RoomType> { (RoomType)type };
            }

            ViewBag.RoomType = new SelectList(roomTypes, room.Type);
            ViewBag.HotelId = new SelectList(hotels, "Id", "Name");
            return View(room);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="room"></param>
        /// <param name="hotelId"></param>
        /// <param name="type"></param>
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