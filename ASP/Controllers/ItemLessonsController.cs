using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.Models;

namespace ASP.Controllers
{
    public class ItemLessonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemLessons
        public ActionResult Index()
        {
            var itemLessons = db.ItemLessons.Include(i => i.Teacher);
            return View(itemLessons.ToList());
        }

        // GET: ItemLessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLesson itemLesson = db.ItemLessons.Find(id);
            if (itemLesson == null)
            {
                return HttpNotFound();
            }
            return View(itemLesson);
        }

        // GET: ItemLessons/Create
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: ItemLessons/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TeacherId")] ItemLesson itemLesson)
        {
            if (ModelState.IsValid)
            {
                db.ItemLessons.Add(itemLesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", itemLesson.TeacherId);
            return View(itemLesson);
        }

        // GET: ItemLessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLesson itemLesson = db.ItemLessons.Find(id);
            if (itemLesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", itemLesson.TeacherId);
            return View(itemLesson);
        }

        // POST: ItemLessons/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TeacherId")] ItemLesson itemLesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemLesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", itemLesson.TeacherId);
            return View(itemLesson);
        }

        // GET: ItemLessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLesson itemLesson = db.ItemLessons.Find(id);
            if (itemLesson == null)
            {
                return HttpNotFound();
            }
            return View(itemLesson);
        }

        // POST: ItemLessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemLesson itemLesson = db.ItemLessons.Find(id);
            db.ItemLessons.Remove(itemLesson);
            db.SaveChanges();
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
