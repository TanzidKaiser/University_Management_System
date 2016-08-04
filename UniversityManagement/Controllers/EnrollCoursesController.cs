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
    public class EnrollCoursesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: EnrollCourses
        public ActionResult Index()
        {
            var enrollCourse = db.EnrollCourse.Include(e => e.Course).Include(e => e.Student);
            return View(enrollCourse.ToList());
        }

        // GET: EnrollCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourse.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Create
        public ActionResult Create()
        {
            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.StudentId = db.RegiterStudent;
            return View();
        }

        // POST: EnrollCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnrollCourse enrollCourse)
        {
            var course = db.EnrollCourse.Where(c => c.CourseId == enrollCourse.CourseId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (course != null)
                {

                    ViewBag.Message = "Course Already Enrolled";
                }
                if (course == null)
                {
                    db.EnrollCourse.Add(enrollCourse);
                    db.SaveChanges();
                    ViewBag.Message = "Enroll Successfully";
                }
            }

            //  ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", enrollCourse.CourseId);
            ViewBag.StudentId = db.RegiterStudent;
            return View(enrollCourse);
        }

        //Get: Name,Email,Department By StudentId

        public JsonResult GetNameEmailDepartmentByStudentId(int? StudentId)
        {
            ProjectDbContext db = new ProjectDbContext();
            var Select = db.RegiterStudent.Where(a => a.Id == StudentId).FirstOrDefault();

            if (StudentId == null)
            {

                RegisterStudent student1 = new RegisterStudent()
                {
                    Name = "",
                    Email = "",
                    DeptName = ""


                };
                return Json(student1, JsonRequestBehavior.AllowGet);

            }
            RegisterStudent student = new RegisterStudent()
            {
                Name = Select.Name,
                Email = Select.Email,
                DeptName = Select.Department.Name


            };
            return Json(student, JsonRequestBehavior.AllowGet);
        }
        //Get:Course Name By Student
        public JsonResult GetCourseStudentId(int? StuedentId)
        {
            ProjectDbContext db = new ProjectDbContext();

            if (StuedentId == null)
            {
                Course c = new Course()
                {

                    Id = 0,
                    Name = ""
                };


                return Json(c, JsonRequestBehavior.AllowGet);

            }

            var select = db.RegiterStudent.Where(a => a.Id == StuedentId).FirstOrDefault();

            int deptId = select.DepartmentId;

            var courses = db.Courses.Where(a => a.DepatmentId == deptId).ToList();

            return Json(courses.Select(c => new
            {

                Id = c.Id,
                Name = c.Name
            }), JsonRequestBehavior.AllowGet);
        }

        // GET: EnrollCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourse.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", enrollCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.RegiterStudent, "Id", "Name", enrollCourse.StudentId);
            return View(enrollCourse);
        }

        // POST: EnrollCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StudentId,Name,Email,Department,CourseId,Date")] EnrollCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", enrollCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.RegiterStudent, "Id", "Name", enrollCourse.StudentId);
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourse.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // POST: EnrollCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollCourse enrollCourse = db.EnrollCourse.Find(id);
            db.EnrollCourse.Remove(enrollCourse);
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
