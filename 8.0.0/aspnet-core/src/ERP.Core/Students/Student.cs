using Abp.Domain.Entities;
using ERP.Courses;
using ERP.Departments;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Students
{
    public class Student : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(10)]
        public string RollNo { get; set; }
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
