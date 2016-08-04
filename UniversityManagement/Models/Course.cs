using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Enter Course Code")]
        [Index(IsUnique = true)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Code Must be minimum 5 Character long")]
        public string Code { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Enter Course Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Credit")]
        [Range(0.5, 5.0, ErrorMessage = "Cradit Save 0.5 between 5.0")]
        public double Credit { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required(ErrorMessage = "Enter Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Required(ErrorMessage = "Select Department")]
        [DisplayName("Department")]
        public int DepatmentId { get; set; }
        public virtual Department Depatment { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        [Required(ErrorMessage = "Select Semester")]
        public string Semester { get; set; }
        public virtual List<CourseAssignTeacher> CourseAssingnTeachers { get; set; }
    }
}