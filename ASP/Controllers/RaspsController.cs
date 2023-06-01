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
    public class RaspsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rasps
        public ActionResult Index()
        {
            var rasps = db.Rasps.Include(r => r.Cabinet).Include(r => r.GroupStud).Include(r => r.ItemLesson);
            return View(rasps.ToList());
        }

        // GET: Rasps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rasp rasp = db.Rasps.Find(id);
            if (rasp == null)
            {
                return HttpNotFound();
            }
            return View(rasp);
        }

        // GET: Rasps/Create
        public ActionResult Create()
        {
            ViewBag.CabinetId = new SelectList(db.Cabinets, "Id", "Name");
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name");
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name");
            return View();
        }

        // POST: Rasps/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupStudId,ItemLessonId,CabinetId,Date")] Rasp rasp)
        {
            if (ModelState.IsValid)
            {
                db.Rasps.Add(rasp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CabinetId = new SelectList(db.Cabinets, "Id", "Name", rasp.CabinetId);
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", rasp.GroupStudId);
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name", rasp.ItemLessonId);
            return View(rasp);
        }

        // GET: Rasps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rasp rasp = db.Rasps.Find(id);
            if (rasp == null)
            {
                return HttpNotFound();
            }
            ViewBag.CabinetId = new SelectList(db.Cabinets, "Id", "Name", rasp.CabinetId);
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", rasp.GroupStudId);
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name", rasp.ItemLessonId);
            return View(rasp);
        }

        // POST: Rasps/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupStudId,ItemLessonId,CabinetId,Date")] Rasp rasp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rasp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CabinetId = new SelectList(db.Cabinets, "Id", "Name", rasp.CabinetId);
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", rasp.GroupStudId);
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name", rasp.ItemLessonId);
            return View(rasp);
        }

        // GET: Rasps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rasp rasp = db.Rasps.Find(id);
            if (rasp == null)
            {
                return HttpNotFound();
            }
            return View(rasp);
        }

        // POST: Rasps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rasp rasp = db.Rasps.Find(id);
            db.Rasps.Remove(rasp);
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
