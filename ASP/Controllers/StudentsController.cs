using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.GroupStud);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GroupStudId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", student.GroupStudId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            //var user = userManager.Users.SingleOrDefault(u => u.ContextId == id && u.Roles.Any(r => r.RoleId == "Student"));
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            //user.Name = student.Name;
            //userManager.Update(user);
            //student.Name = user.Name;

            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", student.GroupStudId);
            
            
            return View(student);
        }

        // POST: Students/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GroupStudId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                var roleStudentId = roleManager.Roles.FirstOrDefault(r => r.Name == "Student")?.Id;
                var user = userManager.Users.SingleOrDefault(u => u.ContextId == student.Id && u.Roles.Any(ro => ro.RoleId == roleStudentId /*находим роль по имени а не по айди*/));
                var editedStudent = db.Students.Find(student.Id);
                if (user == null)
                {
                   return HttpNotFound();
                }

                user.Name = editedStudent.Name;
                userManager.Update(user);

                return RedirectToAction("Index");
            }
            ViewBag.GroupStudId = new SelectList(db.GroupStuds, "Id", "Name", student.GroupStudId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            var roleStudentId = roleManager.Roles.FirstOrDefault(r => r.Name == "Student")?.Id;
            var user = userManager.Users.SingleOrDefault(u => u.ContextId == student.Id && u.Roles.Any(ro => ro.RoleId == roleStudentId /*находим роль по имени а не по айди*/));
            db.Students.Remove(student);
            db.SaveChanges();
            userManager.Delete(user);
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
