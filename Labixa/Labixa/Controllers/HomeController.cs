using System.Linq;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using PagedList;
using Labixa.ViewModels;
using Outsourcing.Service;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Email;
using System.Threading.Tasks;

namespace Labixa.Controllers
{
    public class HomeController : BaseHomeController
    {
        private readonly IProductService _productService;
        private readonly IBlogService _blogService;
        private readonly IWebsiteAttributeService _websiteAttributeService;

        //khởi tạo service Vendor
        private readonly IVendorService _vendorService;

        private readonly IRoomService _roomService;


        private readonly IColorService _colorService;

        private readonly ICostService _costService;

        public HomeController(IVendorService vendorService, IProductService productService, IBlogService blogService,
            IWebsiteAttributeService websiteAttributeService, IRoomService roomService, IColorService colorService,
            ICostService costService)
        {
            _vendorService = vendorService;
            _productService = productService;
            _blogService = blogService;
            _websiteAttributeService = websiteAttributeService;
            _roomService = roomService;
            _colorService = colorService;
            _costService = costService;
        }


        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                RoomHome = _roomService.FindAll(),
                BlogHome = _blogService.FindAll().Take(3),
                imageHome1 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner1")),
                imageHome2 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner2")),
                imageHome3 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner3"))
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Deposit(ContactUs model)
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
               "<td>" + model.Type + "</td>" +
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
               "<td>" + model.Description + "</td>" +
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
            model.Type = 0;
            _vendorService.Create(model);
            await EmailHelper.SendEmailAsync(model.Description, content, subject);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Booking()
        {
            return View();
        }

        public ActionResult BookingRoom(Costs modelBooking)
        {
            modelBooking.CostCategoryId = 1;
            _costService.CreateCost(modelBooking);
            return RedirectToAction("Booking", "Home");
        }

        public ActionResult Activities()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactBookingRooom(Deposit modelContact)
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
             "<td>" + modelContact.Description + "</td>" +
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
            _colorService.Create(modelContact);
            await EmailHelper.SendEmailAsync(modelContact.Email, content, subject);
            return RedirectToAction("Contact", "Home");
        }

        public ActionResult Detail(string slug)
        {
            var obj = _productService.GetProducts().FirstOrDefault(p => p.Slug.Equals(slug));
            return View(obj);
        }

        public ActionResult ImageLogo()
        {
            var url = _websiteAttributeService.GetWebsiteAttributes()
                .FirstOrDefault(p => p.Name.Equals("LoadPageImage"));
            return PartialView("_logoPartial", url);
        }

        public ActionResult BannerAttribute()
        {
            BannerViewModel bannerViewModel = new BannerViewModel();
            bannerViewModel.bannerLogo = _websiteAttributeService
                .GetWebsiteAttributes().FirstOrDefault(p => p.Name.Equals("BannerLogo"));
            bannerViewModel.bannerTitle = _websiteAttributeService
                .GetWebsiteAttributes().FirstOrDefault(p => p.Name.Equals("BannerTitle"));
            bannerViewModel.bannerImage = _websiteAttributeService
                .GetWebsiteAttributes().FirstOrDefault(p => p.Name.Equals("BannerImage"));

            return PartialView("_bannerPartial", bannerViewModel);
        }

        public ActionResult ContactValue()
        {
            var model = _websiteAttributeService.GetWebsiteAttributes()
                .FirstOrDefault(p => p.Name.Equals("Contact"));
            return PartialView("_contactPartial", model);
        }

        public ActionResult Menu()
        {
            MenuViewModel menuViewModel = new MenuViewModel
            {
                pageLogo = _websiteAttributeService
                    .GetWebsiteAttributes().FirstOrDefault(p => p.Name.Equals("PageLogo")),
                pageSlogan = _websiteAttributeService
                    .GetWebsiteAttributes().FirstOrDefault(p => p.Name.Equals("PageSlogan")),
                pageTitle = _websiteAttributeService
                    .GetWebsiteAttributes().FirstOrDefault(p => p.Name.Equals("PageTitle"))
            };
            return PartialView("_menuPartial", menuViewModel);
        }

        public ActionResult DetailService(string slug)
        {
            var model = _blogService.FindAll().FirstOrDefault(p => p.Slug.Equals(slug));

            return View(model);
        }

        public ActionResult DetailBlog(string slug)
        {
            var model = _blogService.FindAll().Where(w => w.BlogCategory.Id == 3)
                .FirstOrDefault(p => p.Slug.Equals(slug));
            return View(model);
        }

        public ActionResult BlogsCategories(int? page = 1)
        {
            var model = _blogService.FindAll().Where(w => w.BlogCategory.Id == 3);
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, 1));
        }
    }
}