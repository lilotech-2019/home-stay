using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;

namespace Labixa.Areas.Portal.Controllers
{
    public class AssetsController : Controller
    {
        #region Fields
        private readonly IAssetService _assertService;
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var assets = await _assertService.FindAll().AsNoTracking()
                            .ToListAsync();
            return View(assets);
        }
        #endregion

        // GET: /Portal/Assets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = await db.Assets.FindAsync(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: /Portal/Assets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Portal/Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,Price,Quantity,RoomId,Deleted,Status,DateCreated,LastModify")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(asset);
        }

        // GET: /Portal/Assets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = await db.Assets.FindAsync(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: /Portal/Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Price,Quantity,RoomId,Deleted,Status,DateCreated,LastModify")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: /Portal/Assets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = await db.Assets.FindAsync(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: /Portal/Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Asset asset = await db.Assets.FindAsync(id);
            db.Assets.Remove(asset);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
