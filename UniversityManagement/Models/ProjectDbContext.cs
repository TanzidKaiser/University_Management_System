using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class ProjectDbContext:DbContext
    {
        public DbSet<Department> department { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<SaveTeacher> SaveTeacher { get; set; }
        public DbSet<CourseAssignTeacher> CourseAssignTeacher { get; set; }
        public DbSet<RegisterStudent> RegiterStudent { get; set; }
        public DbSet<EnrollCourse> EnrollCourse { get; set; }
        public DbSet<SaveStudentResult> StudentResult { get; set; }

       
    }
}