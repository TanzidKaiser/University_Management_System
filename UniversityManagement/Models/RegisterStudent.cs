using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class RegisterStudent
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Enter student Name")]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        [Required(ErrorMessage = "Enter Email address")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Enter contact No")]
        public string ContactNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime date { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [DisplayName("Reg No")]
        public string RegNo { get; set; }
        [NotMapped]
        public string DeptName { get; set; }
        [NotMapped]
        public int CourseId { get; set; }
        [NotMapped]
        public string CourseName { get; set; }
    }
}