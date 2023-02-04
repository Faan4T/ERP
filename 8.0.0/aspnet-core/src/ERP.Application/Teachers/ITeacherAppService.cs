using Abp.Application.Services;
using ERP.Courses.Dto;
using ERP.Departments.Dto;
using ERP.Students.Dto;
using ERP.Teachers.Dto;
using ERP.TeacherWithCourses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Teachers
{
    public interface ITeacherAppService : IApplicationService
    {
        Task CreateTeacher(TeacherDto input);
        Task Delete(string input);
        Task DropAllCourses(int teacherId);
        List<TeacherDto> GetAll(PagedStudentsResultRequestDto input);
        List<CourseDto> GetAllCourse(GetAllCourses input);
        List<DepartmentDto> GetAllDepartmentDropDown();
        Task<Teacher> GetById(int id);
        Task<List<TeacherWithCourse>> GetCourses(int teacherId);
        Task<List<GetAllCoursesToAssign>> GetRegisteredCourses(int studentId);
        Task RegisterCourses(List<int?> courseId, int teacherId);
        Task<List<TeacherTreeViews>> TeacherTreeViews();
        Task UpdateCourse(int newcourseId, int id, int courseId);
        Task UpdateTeacherRecord(TeacherDto input);
    }
}