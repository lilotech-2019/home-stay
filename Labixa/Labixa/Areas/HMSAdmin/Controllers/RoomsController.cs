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
using Outsourcing.Data.Models.HMS;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /HMSAdmin/Rooms/
        public async Task<ActionResult> Index()
        {
            var room = db.Room.Include(r => r.Hotel);
            return View(await room.ToListAsync());
        }

        // GET: /HMSAdmin/Rooms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = await db.Room.FindAsync(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // GET: /HMSAdmin/Rooms/Create
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "MetaKeywords");
            return View();
        }

        // POST: /HMSAdmin/Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,NameEN,Description,DescriptionENG,SharePercent,Status,Price,DiscountPercent,Slug,Layout,DisplayOrder,IsStaticPage,Noted,string1,Utility_Tivi,Utility_TuDo,Utility_HotWater,Utility_DryHair,Utility_Iron,Utility_Kitchen,Utility_TeaCoffee,Utility_Snack,Utility_WashMachine,MetaKeywords,MetaTitle,MetaDescription,HotelId")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Room.Add(rooms);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "MetaKeywords", rooms.HotelId);
            return View(rooms);
        }

        // GET: /HMSAdmin/Rooms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = await db.Room.FindAsync(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "MetaKeywords", rooms.HotelId);
            return View(rooms);
        }

        // POST: /HMSAdmin/Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,NameEN,Description,DescriptionENG,SharePercent,Status,Price,DiscountPercent,Slug,Layout,DisplayOrder,IsStaticPage,Noted,string1,Utility_Tivi,Utility_TuDo,Utility_HotWater,Utility_DryHair,Utility_Iron,Utility_Kitchen,Utility_TeaCoffee,Utility_Snack,Utility_WashMachine,MetaKeywords,MetaTitle,MetaDescription,HotelId")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rooms).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "MetaKeywords", rooms.HotelId);
            return View(rooms);
        }

        // GET: /HMSAdmin/Rooms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = await db.Room.FindAsync(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: /HMSAdmin/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rooms rooms = await db.Room.FindAsync(id);
            db.Room.Remove(rooms);
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
