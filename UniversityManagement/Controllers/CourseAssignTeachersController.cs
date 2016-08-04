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
    public class CourseAssignTeachersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: CourseAssignTeachers
        public ActionResult Index()
        {
            var courseAssignTeacher = db.CourseAssignTeacher.Include(c => c.Course).Include(c => c.Department).Include(c => c.Teacher);
            return View(courseAssignTeacher.ToList());
        }

        // GET: CourseAssignTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignTeacher courseAssignTeacher = db.CourseAssignTeacher.Find(id);
            if (courseAssignTeacher == null)
            {
                return HttpNotFound();
            }
            return View(courseAssignTeacher);
        }

        // GET: CourseAssignTeachers/Create
        public ActionResult Create()
        {


            ViewBag.DepartmentId = db.department;
            return View();
        }


        // POST: CourseAssignTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseAssignTeacher courseAssignTeacher)
        {
            var CourseAssign = db.CourseAssignTeacher.ToList();

            var courseName = CourseAssign.Find(n => n.CourseId == courseAssignTeacher.CourseId && n.TeacherId == courseAssignTeacher.TeacherId);
            courseAssignTeacher.RemainCredit = courseAssignTeacher.RemainCredit - courseAssignTeacher.Credit;

            if (ModelState.IsValid)
            {
                if (courseName != null)
                {

                    ViewBag.message = "Course Already Assign";
                }
                else if (courseName == null)
                {

                    db.CourseAssignTeacher.Add(courseAssignTeacher);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    ViewBag.message = "Save Successfully";
                }

            }

            ViewBag.DepartmentId = db.department;
            return View(courseAssignTeacher);
        }
        public ActionResult CreateViewStatics()
        {


            ViewBag.DepartmentId = db.department;
            return View();
        }

        //Get Teacher By Department Id
        public JsonResult GetTeachersByDepartmentId(int? departmentId)
        {
            ProjectDbContext db = new ProjectDbContext();
            var teachers = db.SaveTeacher.Where(a => a.DepartmentId == departmentId).ToList();

            return Json(teachers.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
            }), JsonRequestBehavior.AllowGet);
        }

        //Get Couse Code By Department Id

        public JsonResult GetCourseCodeByDepartmentId(int? departmentId)
        {
            ProjectDbContext db = new ProjectDbContext();
            var CourseCodes = db.Courses.Where(a => a.DepatmentId == departmentId).ToList();

            return Json(CourseCodes.Select(c => new
            {

                Id = c.Id,
                Code = c.Code
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadCourseStaticsByDepartmentId(int departmentId)
        {
            //var courses = GetCourseStatics(departmentId);

            var list = from c in db.Courses
                       join ca in db.CourseAssignTeacher
                           on c.Id equals ca.CourseId
                       join te in db.SaveTeacher on ca.TeacherId
                           equals te.Id into courses
                       from co in courses.DefaultIfEmpty()
                       where c.DepatmentId == departmentId
                       select new
                       {
                           CourseCode = c.Code,
                           Name = c.Name,
                           Semester = c.Semester,
                           Assignto = (co == null ? "Not Assign yet" : co.Name)
                       };

            return Json(list.Select(c => new
            {
                Code = c.CourseCode,
                Name = c.Name,
                Semester = c.Semester,
                Assignto = c.Assignto
            }), JsonRequestBehavior.AllowGet);
            
            //return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Get Credit to be Taken By Teacher Id

        public JsonResult GetNameAndCreditByCourseId(int? CourseId)
        {
            ProjectDbContext db = new ProjectDbContext();
            var Select = db.Courses.Where(a => a.Id == CourseId).FirstOrDefault();

            Course s = new Course()
            {
                Name = Select.Name,
                Credit = Select.Credit

            };
            //Name = Select.Name;
            //cradit = Select.Credit;

            return Json(s, JsonRequestBehavior.AllowGet);
        }
        //Get Credit Taken and Remain Cradit

        public JsonResult GetCraditTakenAndRemainCreditByTeacherId(int? TeacherId)
        {
            ProjectDbContext db = new ProjectDbContext();

            var Select = db.SaveTeacher.Where(a => a.Id == TeacherId).FirstOrDefault();

            //var assignCredit = db.CourseAssignTeacher.ToList();
            List<double> assignCredit = db.CourseAssignTeacher.Where(t => t.TeacherId == TeacherId).Select(r => r.RemainCredit).ToList();

            CourseAssignTeacher assign = new CourseAssignTeacher();

            double remainCredit;

            if (assignCredit.Count() != 0)
            {

                remainCredit = assignCredit.LastOrDefault();

            }
            else
            {

                remainCredit = Select.CraditTaken;
            }


            SaveTeacher s = new SaveTeacher()
            {
                CraditTaken = Select.CraditTaken,
                Credit = remainCredit

            };


            return Json(s, JsonRequestBehavior.AllowGet);
        }





        // GET: CourseAssignTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignTeacher courseAssignTeacher = db.CourseAssignTeacher.Find(id);
            if (courseAssignTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseAssignTeacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Code", courseAssignTeacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.SaveTeacher, "Id", "Name", courseAssignTeacher.TeacherId);
            return View(courseAssignTeacher);
        }

        // POST: CourseAssignTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,TeacherId,CraditTaken,RemainCredit,CourseId,CoursName,Credit")] CourseAssignTeacher courseAssignTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseAssignTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseAssignTeacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.department, "Id", "Code", courseAssignTeacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.SaveTeacher, "Id", "Name", courseAssignTeacher.TeacherId);
            return View(courseAssignTeacher);
        }

        // GET: CourseAssignTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssignTeacher courseAssignTeacher = db.CourseAssignTeacher.Find(id);
            if (courseAssignTeacher == null)
            {
                return HttpNotFound();
            }
            return View(courseAssignTeacher);
        }

        // POST: CourseAssignTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssignTeacher courseAssignTeacher = db.CourseAssignTeacher.Find(id);
            db.CourseAssignTeacher.Remove(courseAssignTeacher);
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
