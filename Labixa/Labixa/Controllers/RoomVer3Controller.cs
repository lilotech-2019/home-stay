using System.Linq;
using System.Web.Mvc;
using Outsourcing.Service;
using Outsourcing.Service.HMS;
using PagedList;
using Labixa.ViewModels;
using System;
using System.Net.Mail;
using System.Net;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Labixa.Controllers
{
    public class RoomVer3Controller : BaseHomeController
    {
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerservice;

        private readonly IRoomOrderService _roomOrderService;

        public RoomVer3Controller(IRoomService roomService, IVendorService vendorService, IRoomOrderService roomOrderService, IRoomOrderItemService roomOrderItem, ICustomerService customerService)
        {
            _roomService = roomService;
            _roomOrderService = roomOrderService;
            _customerservice = customerService;
        }

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
            var listShortRoom = _roomService.FindByType(RoomType.ShortTemp).OrderBy(_ => _.Name);
            return View(listShortRoom.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// chi tiết phòng ngắn hạn
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult LongRoom(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            var listLongRoom = _roomService.FindByType(RoomType.LongTemp).OrderBy(_=>_.Name);
            return View(listLongRoom.ToPagedList(pageNumber, pageSize));
        }
       
        public ActionResult Details(int id, string slug)
        {
            var room = _roomService.FindByIdAndSlug(id, slug);
            if (room != null)
            {
                var relatedRooms = _roomService.FindByType(room.Type);
                var viewModel = new RoomDetailViewModel
                {
                    RelatedRooms = relatedRooms,
                    Room = room
                };
                return View(viewModel);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult BookingRoom(RoomOrder modelBooking, String customerName, String customerEmail, String customerPhone, String name)
        { 
            Rooms room = new Rooms();
            room.Name = name;
            string adminGmail = "minhtrungmessi@gmail.com";
            string password = "abc65432abc65432";
            string subject = "Đặt phòng thành công";
            string content = "<table border=" + 1 + "><thead>" +
                "<th> Họ Tên Khách Hàng </th>" +
                "<th> Ngày Check In</th>" +
                "<th> Ngày Check Out</th>" +
                "<th> Email Khách Hàng</th>" +
                "<th> Số Điện Thoại</th>" +
                "<th> Tên Phòng</th>" +
                "<th> Số Lượng Người</th>" +
                "<th> Số Tiền</th>" +
                "</thead>" +
                "<tbody>" +
                "<td>" + customerName + "</td>" +
                "<td>" + modelBooking.CheckIn + "</td>" +
                "<td>" + modelBooking.CheckOut + "</td>" +
                "<td>" + customerEmail + "</td>" +
                "<td>" + customerPhone + "</td>" +
                "<td>" + name + "</td>" +
                "<td>" + modelBooking.AmountOfPeople + "</td>" +
                "<td>" + modelBooking.Price + "</td>" +
                "</tbody></table>";
            Customer cus = new Customer();
            cus.Name = customerName;
            cus.Email = customerEmail;
            cus.Phone = customerPhone;
            var customerId = _customerservice.FindIdByPhone(customerPhone);
            modelBooking.CheckInDate = DateTime.Now;
            modelBooking.CheckOutDate = DateTime.Now;
            if (customerId == 0)
            {
                customerId = _customerservice.CreateNewCustomerByPhone(customerName, customerEmail, customerPhone);
            }
            if (customerId != 0)
            {
                modelBooking.CustomerId = customerId;
            }
            modelBooking.CheckInDate = DateTime.Now;
            modelBooking.CheckOutDate = DateTime.Now;
            _roomOrderService.Create(modelBooking);
            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential(adminGmail, password),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(adminGmail);
            mess.ReplyToList.Add(adminGmail);
            mess.To.Add(new MailAddress(customerEmail));
            mess.Subject = subject;
            mess.Body = content;
            mess.IsBodyHtml = true;
            mail.Send(mess);
            return RedirectToAction("ShortRoom", "RoomVer3");
          
        }
        [HttpPost]
        public ActionResult BookingLongRoom(RoomOrder modelBookingLongRoom, String customerName, String customerEmail, String customerPhone, String name)
        {
            Rooms room = new Rooms();
            room.Name = name;
            string adminGmail = "minhtrungmessi@gmail.com";
            string password = "abc65432abc65432";
            string subject = "Đặt phòng thành công";
            string content = "<table border=" + 1 + "><thead>" +
                "<th> Họ Tên Khách Hàng </th>" +
                "<th> Email Khách Hàng</th>" +
                "<th> Số Điện Thoại</th>" +
                "<th> Tên Phòng</th>" +
                "<th> Số Lượng Người</th>" +
                "<th> Số Tiền</th>" +
                "</thead>" +
                "<tbody>" +
                "<td>" + customerName + "</td>" +
                "<td>" + customerEmail + "</td>" +
                "<td>" + customerPhone + "</td>" +
                "<td>" + name + "</td>" +
                "<td>" + modelBookingLongRoom.AmountOfPeople + "</td>" +
                "<td>" + modelBookingLongRoom + "</td>" +
                "</tbody></table>";
            Customer cus = new Customer();
            cus.Name = customerName;
            cus.Email = customerEmail;
            cus.Phone = customerPhone;
            var customerId = _customerservice.FindIdByPhone(customerPhone);
            modelBookingLongRoom.CheckInDate = DateTime.Now;
            modelBookingLongRoom.CheckOutDate = DateTime.Now;
            if (customerId == 0)
            {
                customerId = _customerservice.CreateNewCustomerByPhone(customerName, customerEmail, customerPhone);
            }
            if (customerId != 0)
            {
                modelBookingLongRoom.CustomerId = customerId;
            }
            modelBookingLongRoom.CheckInDate = DateTime.Now;
            modelBookingLongRoom.CheckOutDate = DateTime.Now;
            _roomOrderService.Create(modelBookingLongRoom);
            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential(adminGmail, password),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(adminGmail);
            mess.ReplyToList.Add(adminGmail);
            mess.To.Add(new MailAddress(customerEmail));
            mess.Subject = subject;
            mess.Body = content;
            mess.IsBodyHtml = true;
            mail.Send(mess);
            return RedirectToAction("LongRoom", "RoomVer3");
        }
    }
}