using Abp.Application.Services;
using ERP.Courses.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task CreateCourse(CourseDto input);
        Task UpdateCourse(CourseDto input);
        List<CourseDto> GetAll(PagedCourseResultRequestDto input);
        Task<Course> GetById(int id);
        Task DeleteCourse(int id);
    }
}
