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
            var listShortRoom = _roomService.FindAll();
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
                ();
            viewModel.RelatedRoom = _roomService.FindAll();
            viewModel.listRoom = _roomService.FindAll().SingleOrDefault(w => w.Slug == slug);
            
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
            var listLongRoom = _roomService.FindAll();
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
                RelatedRoomLong = _roomService.FindAll(),
                listRoom = _roomService.FindAll().SingleOrDefault(w=>w.Slug==slug)
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult BookingRoom(RoomOrder modelBooking, String CustomerName, String CustomerEmail, String CustomerPhone, String Name)
        { 
            Rooms room = new Rooms();
            room.Name = Name;
            string AdminGmail = "minhtrungmessi@gmail.com";
            string Password = "abc65432abc65432";
            string Subject = "Đặt phòng thành công";
            string Content = "<table border=" + 1 + "><thead>" +
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
                "<td>" + CustomerName + "</td>" +
                "<td>" + modelBooking.CheckIn + "</td>" +
                "<td>" + modelBooking.CheckOut + "</td>" +
                "<td>" + CustomerEmail + "</td>" +
                "<td>" + CustomerPhone + "</td>" +
                "<td>" + Name + "</td>" +
                "<td>" + modelBooking.AmountOfPeople + "</td>" +
                "<td>" + modelBooking.Price + "</td>" +
                "</tbody></table>";
            Customer cus = new Customer();
            cus.Name = CustomerName;
            cus.Email = CustomerEmail;
            cus.Phone = CustomerPhone;
            var customerId = _customerservice.FindIdByPhone(CustomerPhone);
            modelBooking.CheckInDate = DateTime.Now;
            modelBooking.CheckOutDate = DateTime.Now;
            if (customerId == 0)
            {
                customerId = _customerservice.CreateNewCustomerByPhone(CustomerName, CustomerEmail, CustomerPhone);
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
                Credentials = new NetworkCredential(AdminGmail, Password),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(AdminGmail);
            mess.ReplyToList.Add(AdminGmail);
            mess.To.Add(new MailAddress(CustomerEmail));
            mess.Subject = Subject;
            mess.Body = Content;
            mess.IsBodyHtml = true;
            mail.Send(mess);
            return RedirectToAction("ShortRoom", "RoomVer3");
          
        }
        [HttpPost]
        public ActionResult BookingLongRoom(RoomOrder modelBookingLongRoom, String CustomerName, String CustomerEmail, String CustomerPhone, String Name)
        {
            Rooms room = new Rooms();
            room.Name = Name;
            string AdminGmail = "minhtrungmessi@gmail.com";
            string Password = "abc65432abc65432";
            string Subject = "Đặt phòng thành công";
            string Content = "<table border=" + 1 + "><thead>" +
                "<th> Họ Tên Khách Hàng </th>" +
                "<th> Email Khách Hàng</th>" +
                "<th> Số Điện Thoại</th>" +
                "<th> Tên Phòng</th>" +
                "<th> Số Lượng Người</th>" +
                "<th> Số Tiền</th>" +
                "</thead>" +
                "<tbody>" +
                "<td>" + CustomerName + "</td>" +
                "<td>" + CustomerEmail + "</td>" +
                "<td>" + CustomerPhone + "</td>" +
                "<td>" + Name + "</td>" +
                "<td>" + modelBookingLongRoom.AmountOfPeople + "</td>" +
                "<td>" + modelBookingLongRoom + "</td>" +
                "</tbody></table>";
            Customer cus = new Customer();
            cus.Name = CustomerName;
            cus.Email = CustomerEmail;
            cus.Phone = CustomerPhone;
            var customerId = _customerservice.FindIdByPhone(CustomerPhone);
            modelBookingLongRoom.CheckInDate = DateTime.Now;
            modelBookingLongRoom.CheckOutDate = DateTime.Now;
            if (customerId == 0)
            {
                customerId = _customerservice.CreateNewCustomerByPhone(CustomerName, CustomerEmail, CustomerPhone);
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
                Credentials = new NetworkCredential(AdminGmail, Password),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(AdminGmail);
            mess.ReplyToList.Add(AdminGmail);
            mess.To.Add(new MailAddress(CustomerEmail));
            mess.Subject = Subject;
            mess.Body = Content;
            mess.IsBodyHtml = true;
            mail.Send(mess);
            return RedirectToAction("LongRoom", "RoomVer3");
        }
    }
}