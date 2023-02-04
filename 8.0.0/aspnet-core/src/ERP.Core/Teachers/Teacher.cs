using Abp.Domain.Entities;
using ERP.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Teachers
{
    public class Teacher : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(14)]
        public string TeacherId { get; set; }

        [Required]
        [MaxLength(15)]
        public string Designation { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(2)]
        public int Age { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public int? DepartmentId { get; set; }
        
        public virtual Department Departments { get; set; }
    }
}
