using System.Linq;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using PagedList;
using Labixa.ViewModels;
using Outsourcing.Service;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;

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
                roomHome = _roomService.FindAll(),
                blogHome = _blogService.FindAll(),
                imageHome1 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner1")),
                imageHome2 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner2")),
                imageHome3 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner3"))
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Deposit(Vendors model)
        {
            _vendorService.Create(model);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Booking()
        {
            //Vendor model = new Vendor();
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


        //nếu ko có gì nó sẽ là action có method là Get
        public ActionResult Contact()
        {
            // nó sẽ mặc định lấy cái view tên trùng với tên action luôn

            //muốn gom model, thì phải khởi tạo model trước ở method get
            //Colors model = new Colors();
            return View();
        }

        //nếu để anotation như vậy thì action này có method là Post
        //khi gom model thì kiểu trả về không còn là mở string xàm nữa, mà là model
        [HttpPost]
        public ActionResult ContactBookingRooom(Colors modelContact)
        {
            //khởi tạo obj Vendor, đổ data
            //create obj vendor
            _colorService.Create(modelContact); //xong lưu database
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