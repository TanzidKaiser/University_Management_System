using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class SaveStudentResult
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Select Reg NO")]
        public int StudentId { get; set; }
        public virtual RegisterStudent Student { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30)]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30)]
        [Required]
        public string Department { get; set; }
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        
        [Required]
        [DisplayName("Select Grade Latter")]
        public string Grade { get; set; }
        [NotMapped]
        public string Msg { get; set; }
        [NotMapped]
        public string CourseCode { get; set; }
        [NotMapped]
        public string CourseName { get; set; }
        
    }
}