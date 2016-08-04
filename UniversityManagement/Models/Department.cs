using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Enter Department Code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Provide 2 to 7 character long")]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        [Required(ErrorMessage = "Enter Department Name")]
        [Index(IsUnique=true)]
        public string Name { get; set; }

        public virtual List<Course> Courses { get; set; }
        public virtual List<SaveTeacher> SaveTeachers { get; set; }
        public virtual List<CourseAssignTeacher> CourseAssignTeachers { get; set; }
        public virtual List<RegisterStudent> RegisterStudents { get; set; }

    }
}