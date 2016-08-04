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
    public class RegisterStudentsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: RegisterStudents
        public ActionResult Index()
        {
            var regiterStudent = db.RegiterStudent.Include(r => r.Department);
            return View(regiterStudent.ToList());
        }

        // GET: RegisterStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterStudent registerStudent = db.RegiterStudent.Find(id);
            if (registerStudent == null)
            {
                return HttpNotFound();
            }
            return View(registerStudent);
        }

        // GET: RegisterStudents/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Name");
            return View();
        }

        // POST: RegisterStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterStudent registerStudent)
        {

            if (ModelState.IsValid)
            {
                registerStudent.RegNo = GetRegNo(registerStudent);
                db.RegiterStudent.Add(registerStudent);
                db.SaveChanges();
                ViewBag.Message = "Student Save successfully";
                //return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Name", registerStudent.DepartmentId);
            return View(registerStudent);
        }

        public string GetRegNo(RegisterStudent student)
        {
            int id = db.RegiterStudent.Count(t => (t.DepartmentId == student.DepartmentId) && (t.date.Year == student.date.Year)) + 1;

            Department department = db.department.FirstOrDefault(d => d.Id == student.DepartmentId);
            string deptName = department.Name;
            string regId = deptName + "-" + student.date.Year + "-";
            int len = 3 - id.ToString().Length;
            string addZero = "";
            for (int i = 0; i < len; i++)
            {
                addZero = "0" + addZero;
            }
            return regId + addZero + id;
        }
        // GET: RegisterStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterStudent registerStudent = db.RegiterStudent.Find(id);
            if (registerStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Code", registerStudent.DepartmentId);
            return View(registerStudent);
        }

        // POST: RegisterStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,ContactNo,date,Address,DepartmentId,RegNo")] RegisterStudent registerStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Code", registerStudent.DepartmentId);
            return View(registerStudent);
        }

        // GET: RegisterStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterStudent registerStudent = db.RegiterStudent.Find(id);
            if (registerStudent == null)
            {
                return HttpNotFound();
            }
            return View(registerStudent);
        }

        // POST: RegisterStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterStudent registerStudent = db.RegiterStudent.Find(id);
            db.RegiterStudent.Remove(registerStudent);
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
