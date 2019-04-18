using System.Linq;
using System.Web.Mvc;
using Outsourcing.Service;
using Outsourcing.Service.HMS;
using PagedList;
using Labixa.ViewModels;
using Outsourcing.Data.Models.HMS;
using System;

namespace Labixa.Controllers
{
    public class RoomVer3Controller : BaseHomeController
    {
        private readonly IRoomService _roomService;
        

        private readonly IRoomOrderService _roomOrderService;

        public RoomVer3Controller(IRoomService roomService, IVendorService vendorService, IRoomOrderService roomOrderService, IRoomOrderItemService roomOrderItem)
        {
            _roomService = roomService;
            _roomOrderService = roomOrderService;
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
            var listShortRoom = _roomService.GetRooms().Where(p=>p.Hotel.Layout==0);
            return View(listShortRoom.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// chi tiết phòng ngắn hạn
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult DetailShortRoom(string slug)
        {
            //var model = _RoomService.GetRoomByUrlName(Slug);
            RoomVer3ViewModel viewModel = new RoomVer3ViewModel
            {
                RelatedRoom = _roomService.Get3RoomShortNews(),
                listRoom = _roomService.GetRoomByUrlName(slug)
            };
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
            var listLongRoom = _roomService.GetRooms().Where(p => p.Hotel.Layout == 2);
            return View(listLongRoom.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// chi tiết phòng dài hạn
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult DetailLongRoom(string slug)
        {
            RoomVer3ViewModel viewModel = new RoomVer3ViewModel
            {
                RelatedRoomLong = _roomService.Get3RoomLongNews(),
                listRoom = _roomService.GetRoomByUrlName(slug)
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult BookingRoom(RoomOrder modelBooking, String CustomerName, String CustomerEmail)
        {
            
            _roomOrderService.Create(modelBooking);

            return RedirectToAction("ShortRoom", "RoomVer3");
          
        }
        [HttpPost]
        public ActionResult BookingLongRoom(RoomOrder modelBookingLongRoom)
        {
            _roomOrderService.Create(modelBookingLongRoom);
            return RedirectToAction("LongRoom", "RoomVer3");

        }
    }
}