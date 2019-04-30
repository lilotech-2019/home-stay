using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class RoomAssetsController : Controller
    {
        #region Fields

        private readonly IRoomAssetService _assertService;
        private readonly IRoomService _roomService;

        #endregion

        #region Ctor

        public RoomAssetsController(IRoomAssetService assertService, IRoomService roomService)
        {
            _assertService = assertService;
            _roomService = roomService;
        }

        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int roomId)
        {
            var assets = _assertService.FindAll().AsNoTracking();
         
            ViewBag.RoomId = roomId;
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
            RoomAsset roomAsset = _assertService.FindById((int)id);
            if (roomAsset == null)
            {
                return HttpNotFound();
            }
            return View(roomAsset);
        }
        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int roomId)
        {
            ViewBag.RoomId = new SelectList(_roomService.FindSelectList(roomId), "Id", "Name", roomId);
            return View(new RoomAsset());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomAsset roomAsset)
        {
            if (ModelState.IsValid)
            {
                _assertService.Create(roomAsset);
                return RedirectToAction("Index");
            }

            return View(roomAsset);
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
            RoomAsset roomAsset = _assertService.FindById((int)id);
            if (roomAsset == null)
            {
                return HttpNotFound();
            }
            return View(roomAsset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomAsset roomAsset)
        {
            if (ModelState.IsValid)
            {
                _assertService.Edit(roomAsset);
                return RedirectToAction("Index");
            }
            return View(roomAsset);
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
            RoomAsset roomAsset = _assertService.FindById((int)id);
            if (roomAsset == null)
            {
                return HttpNotFound();
            }
            return View(roomAsset);
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
            RoomAsset roomAsset = _assertService.FindById(id);
            if (roomAsset == null)
            {
                return HttpNotFound();
            }
            _assertService.Delete(roomAsset);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
