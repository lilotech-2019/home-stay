using Outsourcing.Data.Models;
using Outsourcing.Service;
using PagedList;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IRoomOrderService _roomOrderService;

        #endregion

        #region Ctor

        public CustomersController(ICustomerService customerService, IRoomOrderService roomOrderService)
        {
            _customerService = customerService;
            _roomOrderService = roomOrderService;
        }

        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(bool? status, int? page, string searchString)
        {

            var customers = _customerService.FindAll();

            if (status != null)
            {
                customers = customers.Where(w => w.Status == status);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(customers.OrderBy(w => w.Id).ToPagedList(pageNumber, pageSize));
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
            Customer customers = _customerService.FindById((int)id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.Create(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit - GET 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _customerService.FindById((int)id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.Edit(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
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
            var customer = _customerService.FindById((int)id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        /// <summary>
        /// DeleteConfirmed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = _customerService.FindById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            _customerService.Delete(customer);
            return RedirectToAction("Index");
        }

        #endregion
    }
}