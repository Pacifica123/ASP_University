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
    public class GroupStudsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupStuds
        public ActionResult Index()
        {
            return View(db.GroupStuds.ToList());
        }

        // GET: GroupStuds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupStud groupStud = db.GroupStuds.Find(id);
            if (groupStud == null)
            {
                return HttpNotFound();
            }
            return View(groupStud);
        }

        // GET: GroupStuds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupStuds/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] GroupStud groupStud)
        {
            if (ModelState.IsValid)
            {
                db.GroupStuds.Add(groupStud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupStud);
        }

        // GET: GroupStuds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupStud groupStud = db.GroupStuds.Find(id);
            if (groupStud == null)
            {
                return HttpNotFound();
            }
            return View(groupStud);
        }

        // POST: GroupStuds/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GroupStud groupStud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupStud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupStud);
        }

        // GET: GroupStuds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupStud groupStud = db.GroupStuds.Find(id);
            if (groupStud == null)
            {
                return HttpNotFound();
            }
            return View(groupStud);
        }

        // POST: GroupStuds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupStud groupStud = db.GroupStuds.Find(id);
            db.GroupStuds.Remove(groupStud);
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
