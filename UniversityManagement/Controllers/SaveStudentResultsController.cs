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
    public class SaveStudentResultsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: SaveStudentResults
        public ActionResult Index()
        {
            var studentResult = db.StudentResult.Include(s => s.Course).Include(s => s.Student);
            return View(studentResult.ToList());
        }

        // GET: SaveStudentResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveStudentResult saveStudentResult = db.StudentResult.Find(id);
            if (saveStudentResult == null)
            {
                return HttpNotFound();
            }
            return View(saveStudentResult);
        }

        // GET: SaveStudentResults/Create
        public ActionResult Create()
        {
            ViewBag.Grade = GetAllGrade();
            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            ViewBag.StudentId = db.RegiterStudent;
            return View();
        }

        // POST: SaveStudentResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaveStudentResult saveStudentResult)
        {
            int regId = db.EnrollCourse.Where(c => (c.id == saveStudentResult.StudentId)&&(c.CourseId==saveStudentResult.CourseId)).Select(c => c.id).FirstOrDefault();
            if (regId == 0) {

                ViewBag.Message = "Student Doesn't Enroll This Course.";
               
            }
            else if (ModelState.IsValid)
            {
                db.StudentResult.Add(saveStudentResult);
                db.SaveChanges();
                ViewBag.Message = "Result Save Successfully.";
                //return RedirectToAction("Index");
            }
            ViewBag.StudentId = db.RegiterStudent;
            ViewBag.Grade = GetAllGrade();
            return View(saveStudentResult);

           // ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", saveStudentResult.CourseId);
           
        }

        //Get: Name,Email,Department By StudentId

        //public JsonResult GetNameEmailDepartmentByStudentId(int? StudentId)
        //{
        //    ProjectDbContext db = new ProjectDbContext();
        //    var Select = db.RegiterStudent.Where(a => a.Id == StudentId).FirstOrDefault();

        //    if (StudentId == null)
        //    {

        //        RegisterStudent student1 = new RegisterStudent()
        //        {
        //            Name = "",
        //            Email = "",
        //            DeptName = ""


        //        };
        //        return Json(student1, JsonRequestBehavior.AllowGet);

        //    }
        //    RegisterStudent student = new RegisterStudent()
        //    {
        //        Name = Select.Name,
        //        Email = Select.Email,
        //        DeptName = Select.Department.Name


        //    };
        //    return Json(student, JsonRequestBehavior.AllowGet);
        //}
        //Get:Course Name By Student
        //public JsonResult GetCourseStudentId(int? StuedentId)
        //{
        //    ProjectDbContext db = new ProjectDbContext();

        //    if (StuedentId == null)
        //    {
        //        Course c = new Course()
        //        {

        //            Id = 0,
        //            Name = ""
        //        };


        //        return Json(c, JsonRequestBehavior.AllowGet);

        //    }

        //    var select = db.RegiterStudent.Where(a => a.Id == StuedentId).FirstOrDefault();

        //    int deptId = select.DepartmentId;

        //    var courses = db.Courses.Where(a => a.DepatmentId == deptId).ToList();

        //    return Json(courses.Select(c => new
        //    {

        //        Id = c.Id,
        //        Name = c.Name
        //    }), JsonRequestBehavior.AllowGet);
        //}
        //View Result
        public ActionResult ViewReslt() {

            ViewBag.StudentId = db.RegiterStudent;
            return View();
        }

        //Get Code,Name,Grade 
        public JsonResult LoadCourseCodeNameGradeByStudentId(int? StudentId)
        {

            var result = db.StudentResult.Where(c => c.StudentId == StudentId).ToList();
           
            List<SaveStudentResult> viewResult = new List<SaveStudentResult>();
           
            foreach (var item in result) {

                SaveStudentResult a = new SaveStudentResult();

                string code = db.Courses.Where(c => c.Id == item.CourseId).Select(c => c.Code).FirstOrDefault();
                string name = db.Courses.Where(c => c.Id == item.CourseId).Select(c => c.Name).FirstOrDefault();
                string grade = item.Grade;
                if (grade == null)
                {

                    a.Msg = "Not Graded Yet";
                }
                else {

                    a.Msg = grade;
                }
                a.CourseCode = code;
                a.CourseName = name;

                viewResult.Add(a);
            }
            return Json(viewResult.Select(c => new
            {
                Code = c.CourseCode,
                Name = c.CourseName,
                Msg = c.Msg
                
            }), JsonRequestBehavior.AllowGet);
           
        }

        // GET: SaveStudentResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveStudentResult saveStudentResult = db.StudentResult.Find(id);
            if (saveStudentResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", saveStudentResult.CourseId);
            ViewBag.StudentId = new SelectList(db.RegiterStudent, "Id", "Name", saveStudentResult.StudentId);
            return View(saveStudentResult);
        }

        // POST: SaveStudentResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StudentId,Name,Email,Department,CourseId,Grade")] SaveStudentResult saveStudentResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saveStudentResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", saveStudentResult.CourseId);
            ViewBag.StudentId = new SelectList(db.RegiterStudent, "Id", "Name", saveStudentResult.StudentId);
            return View(saveStudentResult);
        }

        // GET: SaveStudentResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveStudentResult saveStudentResult = db.StudentResult.Find(id);
            if (saveStudentResult == null)
            {
                return HttpNotFound();
            }
            return View(saveStudentResult);
        }

        // POST: SaveStudentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaveStudentResult saveStudentResult = db.StudentResult.Find(id);
            db.StudentResult.Remove(saveStudentResult);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private List<SelectListItem> GetAllGrade()
        {
            List<SelectListItem> grade = new List<SelectListItem>
            {
                new SelectListItem{Text = "Select Grade", Value = ""},
                new SelectListItem {Text = "A+", Value = "A+"},
                new SelectListItem {Text = "A", Value ="A"},
                new SelectListItem {Text = "A-", Value ="A-"},
                new SelectListItem {Text = "B+", Value ="B+"},
                new SelectListItem {Text = "B", Value ="B"},
                new SelectListItem {Text = "B-", Value ="B-"},
                new SelectListItem {Text = "C+", Value ="C+"},
                new SelectListItem {Text = "C", Value ="C"},
                new SelectListItem {Text = "C-", Value ="C-"},
                new SelectListItem {Text = "D+", Value ="D+"},
                new SelectListItem {Text = "D", Value ="D"},
                new SelectListItem {Text = "D-", Value ="D-"},
                new SelectListItem {Text = "F", Value ="F"},
            };
            return grade;
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
