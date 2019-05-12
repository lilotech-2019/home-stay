using System.Linq;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using Labixa.ViewModels;
using Outsourcing.Service;
using Outsourcing.Service.HMS;
using Outsourcing.Core.Email;
using System.Threading.Tasks;
using System.Collections.Generic;
using Outsourcing.Service.Portal;
using static Labixa.ViewModels.IndexViewModel;
using IBlogService = Outsourcing.Service.IBlogService;
using IRoomService = Outsourcing.Service.HMS.IRoomService;

namespace Labixa.Controllers
{
    public class HomeController : BaseHomeController
    {
        private readonly IBlogService _blogService;
        private readonly IWebsiteAttributeService _websiteAttributeService;
        private readonly IRoomService _roomService;
        private readonly IDepositService _depositService;
        private readonly IMessageService _messageService;

        public HomeController(IVendorService vendorService, IProductService productService, IBlogService blogService,
            IWebsiteAttributeService websiteAttributeService, IRoomService roomService, IDepositService depositService,
            IMessageService messageService)
        {
            _blogService = blogService;
            _websiteAttributeService = websiteAttributeService;
            _roomService = roomService;
            _depositService = depositService;
            _messageService = messageService;
        }


        public ActionResult Index()
        {
            var slideAtrributes = _websiteAttributeService.GetWebsiteAttributes().OrderBy(q => q.Status == true)
                .Where(p => p.Name.Equals("Labixa.Home.Index.Banner"));
            var slideViewModel = new List<SlideViewModel>();
            var count = 0;
            foreach (var item in slideAtrributes)
            {
                ++count;
                slideViewModel.Add(new SlideViewModel()
                {
                    Style = count % 2 == 0 ? "nhs-caption2" : "nhs-caption3",
                    ImageURL = string.IsNullOrEmpty(item.Value) ? "../../Content/HMS/images/slider/1.jpg" : item.Value,
                    Title = item.Title,
                    TitleEnglish = item.TitleEnglish,
                    Caption = item.Caption,
                    CaptionEnglish = item.CaptionEnglish,
                });
            }
            var model = new IndexViewModel
            {
                roomHome = _roomService.FindAll(),
                blogHome = _blogService.FindAll().Take(3),
                Slider = slideViewModel
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Deposit(Deposit model)
        {
            string subject = "Đặt phòng thành công";
            string content = "<html><head><style type='text/css'>" +
                             ".mail{width: 100%; height: 100% ; background-color: #f5f5f5f5; float: left; background-image: url('https://i.ibb.co/7CL0frY/1.jpg')}" +
                             ".content-mail{width: 60%; background-color: #ffffff; float: left; margin: 100px 20%; border: 1px solid gray;}.logo-img{padding: 2% 5% 0px 5%;}" +
                             ".logo-img img{height: 50px; width: 173px}.content-mail table  {margin: 5% 25% 5% 17%;}.content-mail table tr{margin-bottom: 5%; display: grid;}" +
                             ".content-mail table tr th {font-size: 20px; text-align: left;}.content-mail table tr td {font-size: 30px; } </style></head>" +
                             "<div class='mail'>" +
                             "<div class='content-mail'>" +
                             "<div class='logo-img'>" +
                             "<img src='https://i.ibb.co/5vwLsTR/logo2.png' alt='logo2' border='0'>" +
                             "</div>" +
                             "<table>" +
                             "<tr>" +
                             "<th>Họ và Tên Khách Hàng: </th>" +
                             "<td>" + model.Name + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Loại Hình Cho Thuê </th>" +
                                  //"<td>" + (model.Type == RoomType.ShortTempDeposit) + "</td>" +
                             "<td>" + model.Content + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Địa Chỉ: </th>" +
                             "<td>" + model.Address + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Số Tiền: </th>" +
                             "<td>" + model.Price + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Số Điện Thoại: </th>" +
                             "<td>" + model.Phone + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Email Khách Hàng: </th>" +
                             "<td>" + model.Email + "</td>" +
                             "</tr></table></div></div></html>";
            //string content = "Dear Mr/Ms Admin, <br/>" +
            //                 "<table border=" + 1 + "><thead>" +
            //                 "<th> Họ Tên Khách Hàng </th>" +
            //                 "<th> Loại hình cho thuê</th>" +
            //                 "<th> Địa chỉ</th>" +
            //                 "<th> Số Tiền</th>" +
            //                 "<th> Số Điện Thoại</th>" +
            //                 "<th> Email Khách Hàng</th>" + 
            //                 "</thead>" +
            //                 "<tbody>" +
            //                 "<tr>" +
            //                 "<td>" + model.Name + "</td>" +
            //                 "<td>" + model.Content + "</td>" +
            //                 "<td>" + model.Address + "</td>" +
            //                 "<td>" + model.Price + "</td>" +
            //                 "<td>" + model.Phone + "</td>" +
            //                 "<td>" + model.Description + "</td>" + 
            //                 "</tr>" +
            //                 "</tbody></table>";
            model.Status = true;
            model.Deleted = false;
            model.Type = RoomType.ShortTempDeposit;
            _depositService.Create(model);
            await EmailHelper.SendEmailAsync(model.Email, content, subject);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactBookingRooom(Message modelContact)
        {
            string subject = "Đặt phòng thành công";
            string content = "<html><head><style type='text/css'>" +
                             ".mail{width: 100%; height: 100% ; background-color: #f5f5f5f5; float: left; background-image: url('https://i.ibb.co/7CL0frY/1.jpg')}" +
                             ".content-mail{width: 60%; background-color: #ffffff; float: left; margin: 100px 20%; border: 1px solid gray;}.logo-img{padding: 2% 5% 0px 5%;}" +
                             ".logo-img img{height: 50px; width: 173px}.content-mail table  {margin: 5% 25% 5% 17%;}.content-mail table tr{margin-bottom: 5%; display: grid;}" +
                             ".content-mail table tr th {font-size: 20px; text-align: left;}.content-mail table tr td {font-size: 30px; } </style></head>" +
                             "<div class='mail'>" +
                             "<div class='content-mail'>" +
                             "<div class='logo-img'>" +
                             "<img src='https://i.ibb.co/5vwLsTR/logo2.png' alt='logo2' border='0'>" +
                             "</div>" +
                             "<table>" +
                             "<tr>" +
                             "<th>Họ và Tên Khách Hàng: </th>" +
                             "<td>" + modelContact.Name + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Email Khách Hàng: </th>" +
                             "<td>" + modelContact.Email + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Số Điện Thoại: </th>" +
                             "<td>" + modelContact.Phone + "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "<th>Nội Dung: </th>" +
                             "<td>" + modelContact.Content + "</td>" +
                             "</tr>" +
                             "</table></div></div></html>";
            //string content = "Dear Mr/Ms Admin, <br/>" +
            //                 "<table border=" + 1 + "><thead>" +
            //                 "<th> Họ Tên Khách Hàng </th>" +
            //                 "<th> Email Khách Hàng</th>" +
            //                 "<th> Số Điện Thoại</th>" +
            //                 "<th> Nội Dung</th>" +
            //                 "</thead>" +
            //                 "<tbody>" +
            //                 "<tr>" +
            //                 "<td>" + modelContact.Name + "</td>" +
            //                 "<td>" + modelContact.Email + "</td>" +
            //                 "<td>" + modelContact.Description + "</td>" +
            //                 "<td>" + modelContact.Content + "</td>" +
            //                 "</tr>" +
            //                 "</tbody></table>";
            modelContact.Status = true;
            modelContact.Deleted = false;
            modelContact.Type = MessageType.New;
            _messageService.Create(modelContact);
            await EmailHelper.SendEmailAsync(modelContact.Email, content, subject);
            return RedirectToAction("Contact", "Home");
        }
    }
}