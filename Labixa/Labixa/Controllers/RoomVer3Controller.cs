
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labixa.Models;
using Outsourcing.Service;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models;
using PagedList;
using Labixa.ViewModels;
using Outsourcing.Data.Models.HMS;

namespace Labixa.Controllers
{
    public class RoomVer3Controller : BaseHomeController
    {
        private readonly IRoomService _RoomService;

        //khởi tạo service Vendor
        private readonly IVendorService _VendorService;

        private readonly IRoomOrderService _RoomOrderService;

        private readonly IRoomOrderItemService _RoomOrderItemService;
        public RoomVer3Controller(IRoomService RoomService, IVendorService VendorService, IRoomOrderService RoomOrderService, IRoomOrderItemService RoomOrderItem)
        {
            this._RoomService = RoomService;
            this._VendorService = VendorService;
            this._RoomOrderService = RoomOrderService;
            this._RoomOrderItemService = RoomOrderItem;

        }
        //do mặc định khi gọi, /room/ -> nó sẽ nhảy vô index cho nên mặc định khi vô đây
        // mình sẽ auto chuyển sang action Shortroom
        //vay la phia view minh se sua thanh , phong ngan hang va phong dai han thoi pk a
        // cái menu mà Minh kêu e sửa , em sửa chưa, cho a xem đi
        // GET: /Room/
        public ActionResult Index()
        {
            return RedirectToAction("ShortRoom");
        }
        /// <summary>
        /// danh sách các phòng ngắn hạn
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ShortRoom(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var listShortRoom = _RoomService.GetRooms().Where(p=>p.Hotel.Layout==0);
            return View(listShortRoom.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// chi tiết phòng ngắn hạn
        /// </summary>
        /// <param name="Slug"></param>
        /// <returns></returns>
        public ActionResult DetailShortRoom(string Slug)
        {
            //var model = _RoomService.GetRoomByUrlName(Slug);
            RoomVer3ViewModel viewModel = new RoomVer3ViewModel();
            viewModel.RelatedRoom = _RoomService.Get3RoomShortNews();
            viewModel.listRoom = _RoomService.GetRoomByUrlName(Slug);
            return View(viewModel);
        }
        /// <summary>
        /// danh sách phòng dài hạn
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult LongRoom(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            var listLongRoom = _RoomService.GetRooms().Where(p => p.Hotel.Layout == 2);
            return View(listLongRoom.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// chi tiết phòng dài hạn
        /// </summary>
        /// <param name="Slug"></param>
        /// <returns></returns>
        public ActionResult DetailLongRoom(string Slug)
        {
            RoomVer3ViewModel viewModel = new RoomVer3ViewModel();
            viewModel.RelatedRoomLong = _RoomService.Get3RoomLongNews();
            viewModel.listRoom = _RoomService.GetRoomByUrlName(Slug);
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult BookingRoom(RoomOrder modelBooking)
        {
            _RoomOrderService.CreateRoomOrder(modelBooking);
            return RedirectToAction("ShortRoom", "RoomVer3");
          
        }
        [HttpPost]
        public ActionResult BookingLongRoom(RoomOrderItem modelBooking)
        {
            _RoomOrderItemService.CreateRoomOrderItem(modelBooking);
            return RedirectToAction("LongRoom", "RoomVer3");

        }
    }
}