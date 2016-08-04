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
    public class CoursesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Depatment);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.Semister = GetAllSemister();
            ViewBag.DepatmentId = new SelectList(db.department, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            var Code = db.Courses.Where(c => c.Code == course.Code).FirstOrDefault();
            var Name = db.Courses.Where(c => c.Name == course.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (Code != null)
                {

                    ViewBag.Message = "Course Code Already Exists";
                }
                if (Name != null)
                {

                    ViewBag.Message = "Course Name Already Exists";
                }
                if (Name == null && Code == null)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    ViewBag.Message = "Save Course Successfull";
                    //return RedirectToAction("Index");
                }
            }
            ViewBag.Semister = GetAllSemister();
            ViewBag.DepatmentId = new SelectList(db.department, "Id", "Name", course.DepatmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepatmentId = new SelectList(db.department, "Id", "Code", course.DepatmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Credit,Description,DepatmentId,Semester")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepatmentId = new SelectList(db.department, "Id", "Code", course.DepatmentId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private List<SelectListItem> GetAllSemister()
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Select...", Value = ""},
                new SelectListItem {Text = "First Semister", Value = "First Semister"},
                new SelectListItem {Text = "Second Semister", Value ="Second Semister"},
                new SelectListItem {Text = "Third Semister", Value ="Third Semister"},
                new SelectListItem {Text = "Forth Semister", Value ="Forth Semister"},
                new SelectListItem {Text = "Fifth Semister", Value ="Fifth Semister"},
                new SelectListItem {Text = "Sixth Semister", Value ="Sixth Semister"},
                new SelectListItem {Text = "Seventh Semister", Value ="Seventh Semister"},
                new SelectListItem {Text = "Eight Semister", Value ="Eight Semister"},
                new SelectListItem {Text = "Nine Semister", Value ="Nine Semister"},
                new SelectListItem {Text = "Ten Semister", Value ="Ten Semister"},
                new SelectListItem {Text = "Eleven Semister", Value ="Eleven Semister"},
                new SelectListItem {Text = "Twelve Semister", Value ="Twelve Semister"},
            };
            return items;
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
