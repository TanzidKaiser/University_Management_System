using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagement.Models
{
    public class Designation
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Designations { get; set; }
    }
}