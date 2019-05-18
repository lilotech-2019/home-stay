using ClosedXML.Excel;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Labixa.Areas.Portal.Controllers
{
    [RouteArea("Portal")]
    [RoutePrefix("Hotels")]
    [Authorize]
    public class HotelsController : Controller
    {
        #region Fields

        private readonly IHotelService _hotelService;
        private readonly ICostsService _costsService;
        private readonly IHotelCategoryService _categoryHotelService;
      

        #endregion

        #region Ctor

        public HotelsController(IHotelService hotelService, ICostsService costsService,
            IHotelCategoryService categoryHotelService)
        {
            _costsService = costsService;
            _hotelService = hotelService;
            _categoryHotelService = categoryHotelService;
        }

        private ApplicationUserManager UserManager =>  HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        #endregion

        #region Index

        /// <summary>
        /// Index - Get
        /// </summary>
        /// <param name="categoryId">Hotel Category Id</param>
        /// <returns></returns>
        public ActionResult Index(int? categoryId)
        {
            var hotels = _hotelService.FindAll().Where(w => w.HostEmail == User.Identity.Name);
            if (User.IsInRole(Role.Admin))
            {
                hotels = _hotelService.FindAll();
            }

            if (categoryId != null)
            {
                hotels = hotels.Where(w => w.HotelCategoryId == categoryId);
                ViewBag.hotelCategoryId = categoryId;
            }
            hotels = hotels.AsNoTracking();
            return View(hotels);
        }

        #endregion

        #region Details

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hotel = _hotelService.FindById((int)id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int? categoryId)
        {
            var category = _categoryHotelService.FindSelectList();
            if (categoryId != null)
            {
                category = category.Where(w => w.Id == categoryId);
            }
            ViewBag.HotelCategoryId = new SelectList(category, "Id", "Name", categoryId);

            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(int? categoryId, Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole(Role.Admin))
                {
                    var user = UserManager.FindByEmail(User.Identity.Name);
                    hotel.HostEmail = user.Email;
                    hotel.HostPhone = user.PhoneNumber;
                    hotel.HostAddress = user.Address;
                    hotel.HostName = user.DisplayName;
                }

                hotel.Slug = StringConvert.ConvertShortName(hotel.Name);
                _hotelService.Create(hotel);
                return RedirectToAction("Index", new { categoryId });
            }

            var hotelCategories = _categoryHotelService.FindSelectList();
            if (categoryId != null)
            {
                hotelCategories = hotelCategories.Where(w => w.Id == categoryId);
            }
            ViewBag.HotelCategoryId = new SelectList(hotelCategories, "Id", "Name", hotel.HotelCategoryId);

            return View(hotel);
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit - GET
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id, int? categoryId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hotel = _hotelService.FindAll().FirstOrDefault(w => w.HostEmail == User.Identity.Name & w.Id == id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            var category = _categoryHotelService.FindSelectList();
            if (categoryId != null)
            {
                category = category.Where(_ => _.Id == categoryId);
            }
            ViewBag.HotelCategoryId = new SelectList(category, "Id", "Name", hotel.HotelCategoryId);
            return View(hotel);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int? categoryId, Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole(Role.Admin))
                {
                    var user = UserManager.FindByEmail(User.Identity.Name);
                    hotel.HostEmail = user.Email;
                    hotel.HostPhone = user.PhoneNumber;
                    hotel.HostAddress = user.Address;
                    hotel.HostName = user.DisplayName;
                }
                hotel.Slug = StringConvert.ConvertShortName(hotel.Name);
                _hotelService.Edit(hotel);
                return RedirectToAction("Index", new { categoryId });
            }
            var category = _categoryHotelService.FindSelectList();
            if (categoryId != null)
            {
                category = category.Where(_ => _.Id == categoryId);
            }
            ViewBag.HotelCategoryId = new SelectList(category, "Id", "Name", hotel.HotelCategoryId);
            return View(hotel);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoryHotels = _hotelService.FindById((int)id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        /// <summary>
        /// Delete - POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hotels = _hotelService.FindById(id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            _hotelService.Delete(hotels);
            return RedirectToAction("Index");
        }

        #endregion

        #region HotelSubMenu

        /// <summary>
        /// HotelSubMenu
        /// </summary>
        /// <returns></returns>
        public ActionResult HotelSubMenu()
        {
            var hotels = _hotelService.FindAll();
            return PartialView("_HotelSubMenu", hotels.AsNoTracking().ToList());
        }

        #endregion

        public ActionResult Preview(int hotelId)
        {
            var hotel = _hotelService.FindById(hotelId);
            return View(hotel);
        }

        [HttpPost]
        public FileResult ExportData(int id)
        {
            var costs = _costsService.FindAll().Where(w => w.HotelId == id);
            var costsIncome = costs.Where(w => w.Type == Outsourcing.Data.Models.HMS.CostType.Income);
            var costsOutcome = costs.Where(w => w.Type == Outsourcing.Data.Models.HMS.CostType.Outcome);
            var costsOthers = costs.Where(w => w.Type == Outsourcing.Data.Models.HMS.CostType.Others);

            //All
            DataTable dtAll = new DataTable("Report");
            dtAll.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Amount")
            });

            foreach (var item in costs)
            {
                dtAll.Rows.Add(item.Id, item.Name, item.Amount);
            }

            //Income
            DataTable dtIncome = new DataTable("Income");
            dtIncome.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Amount")
            });

            foreach (var item in costsIncome)
            {
                dtIncome.Rows.Add(item.Id, item.Name, item.Amount);
            }

            //Outcome
            DataTable dtOutcome = new DataTable("Outcome");
            dtOutcome.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Amount")
            });

            foreach (var item in costsOutcome)
            {
                dtOutcome.Rows.Add(item.Id, item.Name, item.Amount);
            }

            //Others
            DataTable dtOthers = new DataTable("Others");
            dtOthers.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Amount")
            });

            foreach (var item in costsOthers)
            {
                dtOthers.Rows.Add(item.Id, item.Name, item.Amount);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtAll);
                wb.Worksheets.Add(dtIncome);
                wb.Worksheets.Add(dtOutcome);
                wb.Worksheets.Add(dtOthers);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Report.xlsx");
                }
            }
        }
    }
}