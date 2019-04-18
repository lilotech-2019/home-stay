using Outsourcing.Core.Common;
using Outsourcing.Data;
using Outsourcing.Data.Models.HMS;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class CategoryHotelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /HMSAdmin/CategoryHotels/
        public async Task<ActionResult> Index()
        {
            return View(await db.CategoryHotels.Where(w=>w.IsDelete == false).AsNoTracking().ToListAsync());
        }

        // GET: /HMSAdmin/CategoryHotels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryHotels categoryHotels = await db.CategoryHotels.Where(w => w.IsDelete == false & w.Id == id).AsNoTracking().SingleOrDefaultAsync();
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        // GET: /HMSAdmin/CategoryHotels/Create
        public ActionResult Create()
        {
            return View(new CategoryHotels { SharePercent = 0 });
        }

        // POST: /HMSAdmin/CategoryHotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryHotels categoryHotels)
        {
            if (ModelState.IsValid)
            {
                categoryHotels.Slug = StringConvert.ConvertShortName(categoryHotels.Name);
                db.CategoryHotels.Add(categoryHotels);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categoryHotels);
        }

        // GET: /HMSAdmin/CategoryHotels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryHotels categoryHotels = await db.CategoryHotels.FindAsync(id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        // POST: /HMSAdmin/CategoryHotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryHotels categoryHotels)
        {
            if (ModelState.IsValid)
            {
                categoryHotels.Slug = StringConvert.ConvertShortName(categoryHotels.Name);
                db.Entry(categoryHotels).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoryHotels);
        }

        // GET: /HMSAdmin/CategoryHotels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryHotels categoryHotels = await db.CategoryHotels.FindAsync(id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        // POST: /HMSAdmin/CategoryHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategoryHotels categoryHotels = await db.CategoryHotels.FindAsync(id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            categoryHotels.IsDelete = true;
            db.Entry(categoryHotels).State = EntityState.Modified;
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
