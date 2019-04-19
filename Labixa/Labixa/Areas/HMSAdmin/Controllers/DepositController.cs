using Outsourcing.Data.Models;
using Outsourcing.Service;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class DepositController : Controller
    {
        #region Fields
        private readonly IColorService _colorService;
        #endregion

        #region Ctor
        public DepositController(IColorService colorService)
        {
            _colorService = colorService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var colors = await _colorService.FindAll().AsNoTracking().ToListAsync();
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
            Colors colors = _colorService.FindById((int)id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            return View(colors);
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
        /// <param name="colors"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Colors colors)
        {
            if (ModelState.IsValid)
            {
                _colorService.Create(colors);
                return RedirectToAction("Index");
            }

            return View(colors);
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
            Colors colors = _colorService.FindById((int)id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            return View(colors);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Colors colors)
        {
            if (ModelState.IsValid)
            {
                _colorService.Edit(colors);
                return RedirectToAction("Index");
            }
            return View(colors);
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
            var colors = _colorService.FindById((int)id);
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
            var colors = _colorService.FindById(id);
            if (colors == null)
            {
                return HttpNotFound();
            }
            _colorService.Delete(colors);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
