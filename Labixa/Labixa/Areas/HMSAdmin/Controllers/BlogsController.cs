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
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /HMSAdmin/Blogs/
        public async Task<ActionResult> Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogCategory);
            return View(await blogs.ToListAsync());
        }

        // GET: /HMSAdmin/Blogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = await db.Blogs.FindAsync(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // GET: /HMSAdmin/Blogs/Create
        public ActionResult Create()
        {
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name");
            return View();
        }

        // POST: /HMSAdmin/Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Title,TitleENG,Slug,BlogImage,Description,DescriptionENG,Temp_1,Temp_2,Temp_3,Temp_4,Temp_5,Content,ContentENG,MetaKeywords,MetaTitle,MetaTitleEN,MetaDescription,MetaDescriptionEN,IsAvailable,IsHomePage,Deleted,DateCreated,LastEditedTime,PictureId,BlogCategoryId,Position")] Blogs blogs)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blogs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blogs.BlogCategoryId);
            return View(blogs);
        }

        // GET: /HMSAdmin/Blogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = await db.Blogs.FindAsync(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blogs.BlogCategoryId);
            return View(blogs);
        }

        // POST: /HMSAdmin/Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,TitleENG,Slug,BlogImage,Description,DescriptionENG,Temp_1,Temp_2,Temp_3,Temp_4,Temp_5,Content,ContentENG,MetaKeywords,MetaTitle,MetaTitleEN,MetaDescription,MetaDescriptionEN,IsAvailable,IsHomePage,Deleted,DateCreated,LastEditedTime,PictureId,BlogCategoryId,Position")] Blogs blogs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blogs.BlogCategoryId);
            return View(blogs);
        }

        // GET: /HMSAdmin/Blogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = await db.Blogs.FindAsync(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: /HMSAdmin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Blogs blogs = await db.Blogs.FindAsync(id);
            db.Blogs.Remove(blogs);
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
