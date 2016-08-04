using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class CourseAssignTeacher
    {

        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int TeacherId { get; set; }

        [DisplayName("Credit to be taken")]
        [Required]
        public double CraditTaken { get; set; }
        [DisplayName("Remaining credit")]
        [Required]
        public double RemainCredit { get; set; }

        public int CourseId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        [Required]
        [DisplayName("Name")]
        public string CoursName { get; set; }
        [Required]
        public double Credit { get; set; }

        public virtual SaveTeacher Teacher { get; set; }
        public virtual Course Course { get; set; }
       
    }
}