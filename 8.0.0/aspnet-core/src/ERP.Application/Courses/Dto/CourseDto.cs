using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Courses.Dto
{
    [AutoMap(typeof(Course))]
    public class CourseDto : EntityDto<int>
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
    }
}
