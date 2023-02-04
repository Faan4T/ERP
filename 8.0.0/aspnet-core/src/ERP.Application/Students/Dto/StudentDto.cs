using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ERP.Departments;
using System;

namespace ERP.Students.Dto
{
    [AutoMap(typeof(Student))]
    public class StudentDto: EntityDto<int>

    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string RollNo { get; set; }
        
        public string PhoneNo { get; set; }
        
        public string Email { get; set; }
        
        public int Age { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        
        public int DepartmentId { get; set; }

        public virtual Department Departments { get; set; }
    }
}
