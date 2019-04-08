﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Outsourcing.Data;
using Outsourcing.Data.Models.HMS;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomOrdersController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: /HMSAdmin/RoomOrders/
        public async Task<ActionResult> Index()
        {
            var roomorders = _db.RoomOrders.Include(r => r.Room);
            return View(await roomorders.ToListAsync());
        }

        // GET: /HMSAdmin/RoomOrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomOrders roomOrders = await _db.RoomOrders.FindAsync(id);
            if (roomOrders == null)
            {
                return HttpNotFound();
            }
            return View(roomOrders);
        }

        // GET: /HMSAdmin/RoomOrders/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name");
            return View(new RoomOrders());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoomOrders roomOrders)
        {
            if (ModelState.IsValid)
            {
                _db.RoomOrders.Add(roomOrders);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name", roomOrders.RoomId);
            return View(roomOrders);
        }

        // GET: /HMSAdmin/RoomOrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomOrders roomOrders = await _db.RoomOrders.FindAsync(id);
            if (roomOrders == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name", roomOrders.RoomId);
            return View(roomOrders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoomOrders roomOrders)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(roomOrders).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(_db.Room, "Id", "Name", roomOrders.RoomId);
            return View(roomOrders);
        }

        // GET: /HMSAdmin/RoomOrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomOrders roomOrders = await _db.RoomOrders.FindAsync(id);
            if (roomOrders == null)
            {
                return HttpNotFound();
            }
            return View(roomOrders);
        }

        // POST: /HMSAdmin/RoomOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomOrders roomOrders = await _db.RoomOrders.FindAsync(id);
            _db.RoomOrders.Remove(roomOrders ?? throw new InvalidOperationException());
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}