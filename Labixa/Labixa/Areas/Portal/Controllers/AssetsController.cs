using Outsourcing.Data.Models;
using Outsourcing.Service;
using Outsourcing.Service.Portal.Outsourcing.Service.Portal;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class AssetsController : Controller
    {
        #region Fields
        private readonly IAssetService _assetService;
        #endregion

        #region Ctor
        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var assets = await _assetService.FindAll().AsNoTracking().ToListAsync();
            return View(assets);
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
            var asset = _assetService.FindById((int)id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
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
        public ActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assetService.Create(asset);
                return RedirectToAction("Index");
            }

            return View(asset);
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
            var asset = _assetService.FindById((int)id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="deposit"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assetService.Edit(asset);
                return RedirectToAction("Index");
            }
            return View(asset);
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
            var asset = _assetService.FindById((int)id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var asset = _assetService.FindById(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            _assetService.Delete(asset);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
