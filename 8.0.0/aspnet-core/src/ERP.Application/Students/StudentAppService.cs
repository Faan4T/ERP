using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using ERP.Authorization;
using ERP.Courses;
using ERP.Departments;
using ERP.Students.Dto;
using ERP.StudentsWithCourses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ERP.Students
{
    [AbpAuthorize(PermissionNames.Pages_Students)]
    public class StudentAppService : ApplicationService, IStudentAppService
    {

        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<StudentWithCourse> _studentwithcourses;
        public StudentAppService(
            IRepository<Student> studentRepository,
            IRepository<Department> departmentRepository, 
            IRepository<Course> courseRepository, 
            IRepository<StudentWithCourse> studentwithcourses)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentwithcourses = studentwithcourses;
            _departmentRepository = departmentRepository;
        }
        //add student
        public async Task CreateStudent(StudentDto input)
        {
            var check = _studentRepository.FirstOrDefault(p => p.RollNo == input.RollNo);
            var student = _studentRepository.FirstOrDefault(p => p.DepartmentId == input.DepartmentId && p.Id == input.Id);
            DateTime localDateTime, univDateTime;
            string strDateTime = input.DateOfBirth.ToString();
            univDateTime = DateTime.Parse(strDateTime);
            localDateTime = univDateTime.ToLocalTime();
            if (check == null || check.DepartmentId == input.DepartmentId && check.RollNo == input.RollNo)
            {
                input.DateOfBirth = localDateTime;
                await _studentRepository.InsertAsync(ObjectMapper.Map<Student>(input));
            }
            else
            {
                throw new UserFriendlyException("Student can only be enroll in one Department");
            }
        }
        //update student info
        public async Task UpdateStudent(StudentDto input)
        {
            //date conversion
            DateTime localDateTime, univDateTime;
            string strDateTime = input.DateOfBirth.ToString();
            univDateTime = DateTime.Parse(strDateTime);
            localDateTime = univDateTime.ToLocalTime();

            var student = _studentRepository.FirstOrDefault(p => p.Id == input.Id);

            student.FirstName = input.FirstName;
            student.LastName = input.LastName;
            student.Age = input.Age;
            student.RollNo = input.RollNo;
            student.Email = input.Email;
            student.DateOfBirth = localDateTime;
            student.PhoneNo = input.PhoneNo;
            student.DepartmentId = input.DepartmentId;
            if (student == null || student.DepartmentId == input.DepartmentId && student.RollNo == input.RollNo)
            {
                await _studentRepository.UpdateAsync(student);
            }
            else
            {
                throw new UserFriendlyException("Please Provide Correct Information");
            }
        }
        //getall student
        public List<StudentDto> GetAll(PagedStudentsResultRequestDto input)
        {
            var query = _studentRepository.GetAll().WhereIf(
                !input.Keyword.IsNullOrWhiteSpace(), x => x.FirstName.Contains(input.Keyword)
                || x.LastName.Contains(input.Keyword)
                || x.Email.Contains(input.Keyword)
                || x.RollNo.Contains(input.Keyword)).ToList();

            return new List<StudentDto>(ObjectMapper.Map<List<StudentDto>>(query));
        }

        //get departmentList
        public List<Department> GetAllDepartmentDropDown()
        {
            var query = _departmentRepository.GetAll().ToList();
            return new List<Department>(ObjectMapper.Map<List<Department>>(query));
        }

        //get all courses
        public List<Course> GetAllCourse(GetAllCourses input)
        {
            var query = _courseRepository.GetAll().Where(c => c.Id == input.Id).ToList();

            return new List<Course>(ObjectMapper.Map<List<Course>>(query));
        }

        //get student by id
        public async Task<Student> GetById(int id)
        {
            return _studentRepository.GetAll().Include(s => s.Departments).FirstOrDefault(x => x.Id == id);
        }
        //delete student by id

        public async Task Delete(string input)
        {
            var item = _studentRepository.FirstOrDefault(x => x.RollNo == input);
            await _studentRepository.DeleteAsync(item);
        }

        //get courses for register
        public async Task<List<StudentWithCourse>> GetCourses(int studentId)
        {
            var studentCourse = await _studentwithcourses.GetAllIncluding(x => x.Courses).Where(x => x.StudentId == studentId).Select(e => e.CourseId).ToListAsync();
            List<StudentWithCourse> courses = new List<StudentWithCourse>();
            foreach (int id in studentCourse)
            {
                var course = _studentwithcourses.GetAllIncluding(x => x.Courses).FirstOrDefault(x => x.CourseId == id);
                courses.Add(course);
            }
            return courses;
        }

        //add Courses with student
        public async Task RegisterCourses(List<int?> courseId, int studentId)
        {
            var student = _studentRepository.FirstOrDefault(x => x.Id == studentId);

            var checkcourse = _studentwithcourses.GetAll().Where(x => x.StudentId == student.Id).Select(e => e.CourseId).ToList();

            var existingIds = await _studentwithcourses.GetAll().Where(x => x.StudentId == student.Id).Select(e => e.CourseId).ToListAsync();
            var check = existingIds.Except(courseId).ToList();
            var add = courseId.Except(check).ToList();
            List<string> errormsg = add.ConvertAll<string>(x => x.ToString());
            bool result = false;
            foreach (var ids in checkcourse)
            {
                foreach (var any in add)
                {
                    if (ids == any)
                    {
                        result = true;
                    }
                }

            }
            if (!result)
            {
                foreach (int id in add)
                {
                    var studentCourse = new StudentWithCourse();
                    studentCourse.CourseId = id;
                    studentCourse.StudentId = studentId;
                    await _studentwithcourses.InsertAsync(studentCourse);
                }
            }
            else
            {
                throw new UserFriendlyException("Selected Course Already Enrolled " + String.Join(", ", errormsg));
            }



        }

        //update Course info
        public async Task UpdateCourse(int newcourseId, int id, int courseId)
        {
            //var student = _studentRepository.FirstOrDefault(x => x.Id == studentId);
            var studentExist = await _studentwithcourses.GetAll().Where(x => x.StudentId == id).Select(e => e.CourseId).ToListAsync();

            var student = await _studentwithcourses.FirstOrDefaultAsync(x => x.CourseId == courseId);
            student.CourseId = newcourseId;
            await _studentwithcourses.UpdateAsync(student);
        }

        //selection of courses
        public async Task<List<GetAllCoursesForRegisterDto>> GetRegisteredCourses(int studentId)
        {
            // get all courses from course
            var allCourses = _courseRepository.GetAll();
            // get all courses to check already register courses
            var registeredCourses = await _studentwithcourses.GetAll().Where(x => x.Id == studentId).Select(e => e.CourseId).ToListAsync();

            List<GetAllCoursesForRegisterDto> courses = new List<GetAllCoursesForRegisterDto>();
            foreach (var course in allCourses)
            {
                if (registeredCourses.Contains(course.Id))
                {
                    courses.Add(new GetAllCoursesForRegisterDto { check = true, Id = course.Id, CourseName = course.CourseName, CourseCode = course.CourseCode });
                }
                else
                {
                    courses.Add(new GetAllCoursesForRegisterDto { check = false, Id = course.Id, CourseName = course.CourseName, CourseCode = course.CourseCode });
                }
            }
            return courses;
        }

        //delete all register courses
        public async Task DropAllCourses(int studentId)
        {

            var studentCourse = await _studentwithcourses.GetAll().Where(x => x.StudentId == studentId).ToListAsync();
            foreach (var s in studentCourse)
            {
                await _studentwithcourses.DeleteAsync(s);

            }
        }

        //delete specific course
        public async Task DropCourse(int courseId, int studentId)
        {
            var course = await _studentwithcourses.FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == studentId);
            await _studentwithcourses.DeleteAsync(course);
        }

        public async Task<List<StudentTreeView>> StudentTreeView()
        {
       var getTree = await _studentwithcourses.GetAll().Include(x => x.Students).Include(x => x.Courses).GroupBy(s=>s.StudentId).Select(j => new StudentTreeView()
            {
               Key = j.First().StudentId,
                label = j.First().Students.FirstName,
                children = j.Select(f => new CourseDetails()
                {
                    Key = f.CourseId,
                    label = f.Courses.CourseName
                }).ToList()
            }).ToListAsync();

            return getTree;
        }
    }
}
