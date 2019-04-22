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

        public RoomVer3Controller(IRoomService roomService, IVendorService vendorService,
            IRoomOrderService roomOrderService, IRoomOrderItemService roomOrderItem, ICustomerService customerService)
        {
            _roomService = roomService;
            _roomOrderService = roomOrderService;
            _customerservice = customerService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ShortRoom");
        }

        public ActionResult ShortRoom(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var listShortRoom = _roomService.FindByType(RoomType.ShortTemp).OrderBy(_ => _.Name);
            return View(listShortRoom.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LongRoom(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var listLongRoom = _roomService.FindByType(RoomType.LongTemp).OrderBy(_ => _.Name);
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
        public ActionResult BookingRoom(RoomOrder modelBooking, String Name, String Email,
            String Phone)
        {
            Room room = new Room();
            room.Name = Name;
            string adminGmail = "minhtrungmessi@gmail.com";
            string password = "abc65432abc65432";
            string subject = "Đặt phòng thành công";
            string content = "<table border=" + 1 + "><thead>" +
                             "<th> Họ Tên Khách Hàng </th>" +
                             "<th> Ngày Check In</th>" +
                             "<th> Ngày Check Out</th>" +
                             "<th> Email Khách Hàng</th>" +
                             "<th> Số Điện Thoại</th>" +
                             "<th> Số Lượng Người</th>" +
                             "<th> Số Tiền</th>" +
                             "</thead>" +
                             "<tbody>" +
                             "<td>" + Name + "</td>" +
                             "<td>" + modelBooking.CheckIn + "</td>" +
                             "<td>" + modelBooking.CheckOut + "</td>" +
                             "<td>" + Email + "</td>" +
                             "<td>" + Phone + "</td>" +
                             "<td>" + modelBooking.AmountOfPeople + "</td>" +
                             "<td>" + modelBooking.Price + "</td>" +
                             "</tbody></table>";

            var customer = _customerservice.FindByPhone(Phone);
            if (customer == null)
            {
                customer = new Customer
                {
                    Name = Name,
                    Email = Email,
                    Phone = Phone
                };
                _customerservice.Create(customer);
            }

            modelBooking.CustomerId = customer.Id;
            modelBooking.Status = true;
            modelBooking.Deleted = false;
            _roomOrderService.Create(modelBooking);
            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential(adminGmail, password),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(adminGmail);
            mess.ReplyToList.Add(adminGmail);
            mess.To.Add(new MailAddress(Email));
            mess.Subject = subject;
            mess.Body = content;
            mess.IsBodyHtml = true;
            mail.Send(mess);
            return RedirectToAction("ShortRoom", "RoomVer3");
        }

        [HttpPost]
        public ActionResult BookingLongRoom(RoomOrder modelBookingLongRoom, String Name, String Email,
            String Phone, String name)
        {
            Room room = new Room();
            room.Name = name;
            string adminGmail = "minhtrungmessi@gmail.com";
            string password = "abc65432abc65432";
            string subject = "Đặt phòng thành công";
            string content = "<table border=" + 1 + "><thead>" +
                             "<th> Họ Tên Khách Hàng </th>" +
                             "<th> Email Khách Hàng</th>" +
                             "<th> Số Điện Thoại</th>" +
                             "<th> Số Lượng Người</th>" +
                             "<th> Số Tiền</th>" +
                             "</thead>" +
                             "<tbody>" +
                             "<td>" + Name + "</td>" +
                             "<td>" + Email + "</td>" +
                             "<td>" + Phone + "</td>" +
                             "<td>" + modelBookingLongRoom.AmountOfPeople + "</td>" +
                             "<td>" + modelBookingLongRoom.Price + "</td>" +
                             "</tbody></table>";

            var customer = _customerservice.FindByPhone(Phone);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = Name,
                    Email = Email,
                    Phone = Phone
                };
                _customerservice.Create(customer);
            }
            modelBookingLongRoom.CheckIn = DateTime.Now;
            modelBookingLongRoom.CheckOut = DateTime.Now;
            modelBookingLongRoom.CustomerId = customer.Id;
            modelBookingLongRoom.Status = true;
            modelBookingLongRoom.Deleted = false;
            _roomOrderService.Create(modelBookingLongRoom);
            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential(adminGmail, password),
                EnableSsl = true
            };
            var mess = new MailMessage { From = new MailAddress(adminGmail) };
            mess.ReplyToList.Add(adminGmail);
            mess.To.Add(new MailAddress(Email));
            mess.Subject = subject;
            mess.Body = content;
            mess.IsBodyHtml = true;
            mail.Send(mess);
            return RedirectToAction("LongRoom", "RoomVer3");
        }
    }
}