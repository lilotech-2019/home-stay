using Outsourcing.Data.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Outsourcing.Service;

namespace Labixa.Areas.Portal.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        #region Fields

        private readonly ICustomerService _customerService;

        #endregion

        #region Ctor

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var customers = await _customerService.FindAll().AsNoTracking()
                            .ToListAsync();
            return View(customers);
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