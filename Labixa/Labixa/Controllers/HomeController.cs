using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labixa.Models;
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
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IWebsiteAttributeService _websiteAttributeService;
        private readonly IStaffService _staffService;
        private readonly ITypeNotifyService _typeNotifyService;
        private readonly IProductAttributeMappingService _productAttributeMappingService;
     
        //khởi tạo service Vendor
        private readonly IVendorService _VendorService;

        private readonly IRoomService _RoomService;

        private readonly IWebsiteAttributeService _IWebsiteAttributeService;



        private readonly IColorService _ColorService;

        private readonly ICostService _CostService;
        public HomeController(IProductService productService, IBlogService blogService,
            IWebsiteAttributeService websiteAttributeService, IBlogCategoryService blogCategoryService,
            IStaffService staffService, IProductAttributeMappingService productAttributeMappingService,
            ITypeNotifyService _typeNotifyService, IVendorService VendorService, IRoomService roomService, IWebsiteAttributeService IWebsiteAttributeService,
            IColorService ColorService, ICostService CostService
           )
        {
            this._productService = productService;
            this._blogService = blogService;
            this._websiteAttributeService = websiteAttributeService;
            this._blogCategoryService = blogCategoryService;
            this._staffService = staffService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._typeNotifyService = _typeNotifyService;
            this._VendorService = VendorService;
            this._RoomService = roomService;
            this._ColorService = ColorService;
            this._CostService = CostService;
        }


        public ActionResult Index()
        {
  
            IndexViewModel model = new IndexViewModel();        
            model.roomHome = _RoomService.Get3RoomShortNews();
            model.blogHome = _blogService.Get3BlogNewsNewest();
            model.imageHome1 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner1"));
            model.imageHome2 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner2"));
            model.imageHome3 = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Banner3"));
            return View(model);
        }
        [HttpPost]
        public ActionResult Deposit(Vendor model)
        {
            _VendorService.CreateVendor(model); 
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index2()
        {

            return View();
        }
        public ActionResult Index3()
        {

            return View();
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
  
     
        public ActionResult BookingRoom(Cost modelBooking)
        {
            CostCategory categoryId = new CostCategory();
            modelBooking.CostCategoryId = 1;
            _CostService.CreateCost(modelBooking);
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
            Color model = new Color();
            return View(model);
        }
        //nếu để anotation như vậy thì action này có method là Post
        //khi gom model thì kiểu trả về không còn là mở string xàm nữa, mà là model
        [HttpPost]
       public ActionResult Contact(Color modelContact) 
        {

            //khởi tạo obj Vendor, đổ data
            //create obj vendor
            _ColorService.CreateColor(modelContact); //xong lưu database
            return RedirectToAction("Contact", "Home");
        }
       public ActionResult Detail(string slug)
        {
            var obj  = _productService.GetProducts().Where(p => p.Slug.Equals(slug)).FirstOrDefault();
            return View(obj);
            
        }
        public ActionResult ImageLogo()
        {
            var url = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("LoadPageImage")).FirstOrDefault();
            return PartialView("_logoPartial", url);
        }
        public ActionResult BannerAttribute()
        {
            BannerViewModel bannerViewModel = new BannerViewModel();
            bannerViewModel.bannerLogo = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("BannerLogo")).FirstOrDefault();
            bannerViewModel.bannerTitle = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("BannerTitle")).FirstOrDefault();
            bannerViewModel.bannerImage = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("BannerImage")).FirstOrDefault();

            return PartialView("_bannerPartial", bannerViewModel);
        }
        public ActionResult contactValue()
        {
            var model = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("Contact")).FirstOrDefault();
            return PartialView("_contactPartial", model);
        }
        
        public ActionResult menu()
        {
            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.pageLogo = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("PageLogo")).FirstOrDefault();
            menuViewModel.pageSlogan = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("PageSlogan")).FirstOrDefault();
            menuViewModel.pageTitle = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Name.Equals("PageTitle")).FirstOrDefault();
            return PartialView("_menuPartial", menuViewModel);
        }
        public ActionResult DetailService(string slug)
        {
            var model = _blogService.GetStaticPage().Where(p => p.Slug.Equals(slug)).FirstOrDefault();
            
            return View(model);
        }
        public ActionResult DetailBlog(string slug)
        {
            var model = _blogService.GetBlogsByCategory(3).Where(p => p.Slug.Equals(slug)).FirstOrDefault();
            return View(model);
        }
        public ActionResult BlogsCategories(int? page=1)
        {   
            var model = _blogService.GetBlogsByCategory(3).ToList();
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, 1));
        }
    }
}