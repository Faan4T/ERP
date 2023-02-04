using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ERP.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Teachers.Dto
{
    [AutoMap(typeof(Teacher))]
    public class TeacherDto: EntityDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TeacherId { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public int? DepartmentId { get; set; }
        public int Age { get; set; }
        public virtual Department Departments { get; set; }

    }
}
