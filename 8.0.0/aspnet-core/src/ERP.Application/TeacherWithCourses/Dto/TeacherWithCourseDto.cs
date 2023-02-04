using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ERP.StudentsWithCourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TeacherWithCourses.Dto
{
    [AutoMap(typeof(TeacherWithCourse))]
    public class TeacherWithCourseDto : EntityDto<int>
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
    }
}
