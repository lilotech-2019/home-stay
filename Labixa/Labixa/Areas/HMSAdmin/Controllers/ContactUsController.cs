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

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class ContactUsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /HMSAdmin/ContactUs/
        public async Task<ActionResult> Index()
        {
            return View(await db.Vendors.ToListAsync());
        }

        // GET: /HMSAdmin/ContactUs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendors vendors = await db.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            return View(vendors);
        }

        // GET: /HMSAdmin/ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /HMSAdmin/ContactUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,Address,Phone,Description,Content,Type,Note,Percent,IsDelete,Price")] Vendors vendors)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendors);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vendors);
        }

        // GET: /HMSAdmin/ContactUs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendors vendors = await db.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            return View(vendors);
        }

        // POST: /HMSAdmin/ContactUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Address,Phone,Description,Content,Type,Note,Percent,IsDelete,Price")] Vendors vendors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendors).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vendors);
        }

        // GET: /HMSAdmin/ContactUs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendors vendors = await db.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            return View(vendors);
        }

        // POST: /HMSAdmin/ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vendors vendors = await db.Vendors.FindAsync(id);
            db.Vendors.Remove(vendors);
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
