using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagement.Models;

namespace UniversityManagement.Controllers
{
    public class SaveTeachersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: SaveTeachers
        public ActionResult Index()
        {
            var saveTeacher = db.SaveTeacher.Include(s => s.Department).Include(s => s.Designation);
            return View(saveTeacher.ToList());
        }

        // GET: SaveTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveTeacher saveTeacher = db.SaveTeacher.Find(id);
            if (saveTeacher == null)
            {
                return HttpNotFound();
            }
            return View(saveTeacher);
        }

        // GET: SaveTeachers/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Name");
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations");
            return View();
        }

        // POST: SaveTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaveTeacher saveTeacher)
        {
            var email = db.SaveTeacher.Where(c => c.Email == saveTeacher.Email).FirstOrDefault();

            if (email != null)
            {
                ViewBag.Message = "Already Have an Email Address";
            }
            if (ModelState.IsValid)
            {
                if (email != null)
                {
                    ViewBag.Message = "Already Have an Email Address";
                }
                if (email == null)
                {
                    db.SaveTeacher.Add(saveTeacher);
                    db.SaveChanges();
                    ViewBag.Message = "Save Teacher Successfully";
                    //return RedirectToAction("Index");
               
                }
                
            }

            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Name", saveTeacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", saveTeacher.DesignationId);
            return View(saveTeacher);
        }

        // GET: SaveTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveTeacher saveTeacher = db.SaveTeacher.Find(id);
            if (saveTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Code", saveTeacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", saveTeacher.DesignationId);
            return View(saveTeacher);
        }

        // POST: SaveTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Email,ContactNo,DesignationId,DepartmentId,CraditTaken")] SaveTeacher saveTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saveTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Code", saveTeacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", saveTeacher.DesignationId);
            return View(saveTeacher);
        }

        // GET: SaveTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveTeacher saveTeacher = db.SaveTeacher.Find(id);
            if (saveTeacher == null)
            {
                return HttpNotFound();
            }
            return View(saveTeacher);
        }

        // POST: SaveTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaveTeacher saveTeacher = db.SaveTeacher.Find(id);
            db.SaveTeacher.Remove(saveTeacher);
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
