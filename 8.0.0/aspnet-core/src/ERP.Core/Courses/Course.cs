using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Courses
{
    public class Course : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }
        [Required]
        [MaxLength(8)]
        public string CourseCode { get; set; }

    }
}
