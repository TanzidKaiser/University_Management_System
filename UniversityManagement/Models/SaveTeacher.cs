using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    
    public class SaveTeacher
    {
        
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Enter teacher Name")]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        [Required(ErrorMessage = "Provide teacher address ")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Column(TypeName = "varchar")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        [Required(ErrorMessage = "Enter Email address")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        [Required(ErrorMessage = "Enter Contact Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Select Designation")]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }

        [Required(ErrorMessage = "Select Department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [DisplayName("Credit to be Taken")]
        [Required(ErrorMessage = "Enter Cradit taken")]
        [Range(0, double.MaxValue, ErrorMessage = "Credit Can Not be Less than Zero")]
        public double CraditTaken { get; set; }

        [NotMapped]
        public double Credit{get;set;}
        public virtual List<CourseAssignTeacher> CourseAssignTeachers { get; set; }
        
    }
}