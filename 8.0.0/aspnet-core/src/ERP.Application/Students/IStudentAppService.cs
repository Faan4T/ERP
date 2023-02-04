using Abp.Application.Services;
using ERP.Courses;
using ERP.Departments;
using ERP.Students.Dto;
using ERP.StudentsWithCourses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Students
{
    public interface IStudentAppService : IApplicationService
    {
        Task CreateStudent(StudentDto input);
        Task Delete(string input);
        Task DropAllCourses(int sId);
        Task DropCourse(int courseId, int studentId);
        List<StudentDto> GetAll(PagedStudentsResultRequestDto input);
        List<Course> GetAllCourse(GetAllCourses input);
        List<Department> GetAllDepartmentDropDown();
        Task<Student> GetById(int id);
        Task<List<StudentWithCourse>> GetCourses(int studentId);
        Task<List<GetAllCoursesForRegisterDto>> GetRegisteredCourses(int studentId);
        Task RegisterCourses(List<int?> courseId, int studentId);
        Task<List<StudentTreeView>> StudentTreeView();
        Task UpdateCourse(int courseId, int studentId, int newcourseId);
        Task UpdateStudent(StudentDto input);
    }
}
