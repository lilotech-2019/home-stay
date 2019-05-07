using Outsourcing.Data.Models;
using Outsourcing.Service;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class DepositController : Controller
    {
        #region Fields
        private readonly IDepositService _depositService;
        #endregion

        #region Ctor
        public DepositController(IDepositService depositService)
        {
            _depositService = depositService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var colors = await _depositService.FindAll().AsNoTracking().ToListAsync();
            return View(colors);
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
            Deposit deposit = _depositService.FindById((int)id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
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
        /// <param name="deposit"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _depositService.Create(deposit);
                return RedirectToAction("Index");
            }

            return View(deposit);
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
            Deposit deposit = _depositService.FindById((int)id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="deposit"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _depositService.Edit(deposit);
                return RedirectToAction("Index");
            }
            return View(deposit);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var colors = _depositService.FindById((int)id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            return View(colors);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var colors = _depositService.FindById(id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            _depositService.Delete(colors);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
