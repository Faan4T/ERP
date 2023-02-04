using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ERP.StudentsWithCourses;

namespace ERP.StudentCourses
{
    [AutoMap(typeof(StudentWithCourse))]
    public class StudentWithCoursesDto : EntityDto<int>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

    }
}
