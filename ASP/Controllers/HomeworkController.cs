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
    public class HomeworkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Homework
        public ActionResult Index()
        {
            var homeworks = db.Homeworks.Include(h => h.GroupStud).Include(h => h.ItemLesson);
            return View(homeworks.ToList());
        }

        // GET: Homework/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homeworks.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // GET: Homework/Create
        public ActionResult Create()
        {
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name");
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name");
            return View();
        }

        // POST: Homework/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ItemLessonId,GroupStudId")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                db.Homeworks.Add(homework);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", homework.GroupStudId);
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name", homework.ItemLessonId);
            return View(homework);
        }

        // GET: Homework/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homeworks.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", homework.GroupStudId);
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name", homework.ItemLessonId);
            return View(homework);
        }

        // POST: Homework/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ItemLessonId,GroupStudId")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homework).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", homework.GroupStudId);
            ViewBag.ItemLessonId = new SelectList(db.ItemLessons, "Id", "Name", homework.ItemLessonId);
            return View(homework);
        }

        // GET: Homework/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homeworks.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homework homework = db.Homeworks.Find(id);
            db.Homeworks.Remove(homework);
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
