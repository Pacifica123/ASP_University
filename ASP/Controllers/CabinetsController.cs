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
    public class CabinetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cabinets
        public ActionResult Index()
        {
            return View(db.Cabinets.ToList());
        }

        // GET: Cabinets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cabinet cabinet = db.Cabinets.Find(id);
            if (cabinet == null)
            {
                return HttpNotFound();
            }
            return View(cabinet);
        }

        // GET: Cabinets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cabinets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                db.Cabinets.Add(cabinet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cabinet);
        }

        // GET: Cabinets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cabinet cabinet = db.Cabinets.Find(id);
            if (cabinet == null)
            {
                return HttpNotFound();
            }
            return View(cabinet);
        }

        // POST: Cabinets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cabinet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cabinet);
        }

        // GET: Cabinets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cabinet cabinet = db.Cabinets.Find(id);
            if (cabinet == null)
            {
                return HttpNotFound();
            }
            return View(cabinet);
        }

        // POST: Cabinets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cabinet cabinet = db.Cabinets.Find(id);
            db.Cabinets.Remove(cabinet);
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
