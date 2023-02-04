using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using ERP.Authorization;
using ERP.Courses;
using ERP.Courses.Dto;
using ERP.Departments;
using ERP.Departments.Dto;
using ERP.Students;
using ERP.Students.Dto;
using ERP.Teachers.Dto;
using ERP.TeacherWithCourses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Teachers
{
    [AbpAuthorize(PermissionNames.Pages_Teachers)]
    public class TeacherAppService : ApplicationService, ITeacherAppService
    {
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<TeacherWithCourse> _teacherWithCourseRepository;

        public TeacherAppService(
             IRepository<Teacher> teacherRepository,
             IRepository<Course> courseRepository,
             IRepository<Department> departmentRepository,
             IRepository<TeacherWithCourse> teacherWithCourseRepository
            )
        {
            _teacherRepository=teacherRepository;
            _courseRepository = courseRepository;
            _departmentRepository=departmentRepository;
            _teacherWithCourseRepository=teacherWithCourseRepository;
        }

        //Add Teacher Record
        public async Task CreateTeacher(TeacherDto input)
        {
            var check = _teacherRepository.FirstOrDefault(s => s.Id == input.Id);
            DateTime localDateTime, univDateTime;
            string strDateTime = input.DateOfBirth.ToString();
            univDateTime = DateTime.Parse(strDateTime);
            localDateTime = univDateTime.ToLocalTime();
            if (check == null || check.DepartmentId == input.DepartmentId && check.TeacherId == input.TeacherId)
            {
                input.DateOfBirth = localDateTime;
                await _teacherRepository.InsertAsync(ObjectMapper.Map<Teacher>(input));
            }
            else
            {
                throw new UserFriendlyException("Teacher can enroll in one Department");
            }
        }

        //update Teacher Record
        public async Task UpdateTeacherRecord(TeacherDto input)
        {
            DateTime localDateTime, univDateTime;
            string strDateTime = input.DateOfBirth.ToString();
            univDateTime = DateTime.Parse(strDateTime);
            localDateTime = univDateTime.ToLocalTime();

            var teacher = _teacherRepository.FirstOrDefault(p => p.TeacherId == input.TeacherId);


            teacher.FirstName = input.FirstName;
            teacher.LastName = input.LastName;
            teacher.Age = input.Age;
            teacher.TeacherId = input.TeacherId;
            teacher.Email = input.Email;
            teacher.Designation = input.Designation;
            teacher.DateOfBirth = localDateTime;
            teacher.PhoneNo = input.PhoneNo;
            teacher.DepartmentId = input.DepartmentId;

            if (teacher.DepartmentId == input.DepartmentId && teacher.TeacherId == input.TeacherId)
            {
                await _teacherRepository.UpdateAsync(teacher);
            }
            else
            {
                throw new UserFriendlyException("Please Provide Correct Information");
            }
        }

        //get all Teacher Record
        public List<TeacherDto> GetAll(PagedStudentsResultRequestDto input)
        {
            var query = _teacherRepository.GetAll().WhereIf(
                !input.Keyword.IsNullOrWhiteSpace(), x => x.FirstName.Contains(input.Keyword)
                || x.LastName.Contains(input.Keyword)
                || x.Email.Contains(input.Keyword)
                || x.TeacherId.Contains(input.Keyword)).ToList();

            return new List<TeacherDto>(ObjectMapper.Map<List<TeacherDto>>(query));
        }

        //get departmentList
        public List<DepartmentDto> GetAllDepartmentDropDown()
        {
            var query = _departmentRepository.GetAll().ToList();
            return new List<DepartmentDto>(ObjectMapper.Map<List<DepartmentDto>>(query));
        }

        //get all courses
        public List<CourseDto> GetAllCourse(GetAllCourses input)
        {
            var query = _courseRepository.GetAll().Where(c => c.Id == input.Id).ToList();

            return new List<CourseDto>(ObjectMapper.Map<List<CourseDto>>(query));
        }

        //get teacher by id
        public async Task<Teacher> GetById(int id)
        {
            return _teacherRepository.GetAll().Include(s => s.Departments).FirstOrDefault(x => x.Id == id);
        }

        //delete 
        public async Task Delete(string input)
        {
            var item = _teacherRepository.FirstOrDefault(x => x.TeacherId == input);
            await _teacherRepository.DeleteAsync(item);
        }

        //get courses for Allocation
        public async Task<List<TeacherWithCourse>> GetCourses(int teacherId)
        {
            var teacherCourse = await _teacherWithCourseRepository.GetAllIncluding(x => x.Courses).Where(x => x.TeacherId == teacherId).Select(e => e.CourseId).ToListAsync();
            List<TeacherWithCourse> Teacherandcourses = new List<TeacherWithCourse>();
            foreach (int id in teacherCourse)
            {
                var course = _teacherWithCourseRepository.GetAllIncluding(x => x.Courses).FirstOrDefault(x => x.CourseId == id);
                Teacherandcourses.Add(course);
            }
            return Teacherandcourses;
        }

        //alloate Courses to teachers
        public async Task RegisterCourses(List<int?> courseId, int teacherId)
        {
            var teacher = _teacherRepository.FirstOrDefault(x => x.Id == teacherId);

            var checkcourse = _teacherWithCourseRepository.GetAll().Where(x => x.TeacherId == teacher.Id).Select(e => e.CourseId).ToList();

            var existingIds = await _teacherWithCourseRepository.GetAll().Where(x => x.TeacherId == teacher.Id).Select(e => e.CourseId).ToListAsync();
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
                    var teacherCourse = new TeacherWithCourse();
                    teacherCourse.CourseId = id;
                    teacherCourse.TeacherId = teacherId;
                    await _teacherWithCourseRepository.InsertAsync(teacherCourse);
                }
            }
            else
            {
                throw new UserFriendlyException("Teacher can not Alloate more then 4 courses");
            }

        }

        //update Course info
        public async Task UpdateCourse(int newcourseId, int id, int courseId)
        {
            //var student = _studentRepository.FirstOrDefault(x => x.Id == studentId);
            var studentExist = await _teacherWithCourseRepository.GetAll().Where(x => x.TeacherId == id).Select(e => e.CourseId).ToListAsync();

            var student = await _teacherWithCourseRepository.FirstOrDefaultAsync(x => x.CourseId == courseId);
            student.CourseId = newcourseId;
            await _teacherWithCourseRepository.UpdateAsync(student);
        }

        //selection of courses
        public async Task<List<GetAllCoursesToAssign>> GetRegisteredCourses(int studentId)
        {
            // get all courses from course
            var allCourses = _courseRepository.GetAll();
            // get all courses to check already Allocated courses
            var registeredCourses = await _teacherWithCourseRepository.GetAll().Where(x => x.Id == studentId).Select(e => e.CourseId).ToListAsync();

            List<GetAllCoursesToAssign> courses = new List<GetAllCoursesToAssign>();
            foreach (var course in allCourses)
            {
                if (registeredCourses.Contains(course.Id))
                {
                    courses.Add(new GetAllCoursesToAssign { check = true, Id = course.Id, CourseName = course.CourseName, CourseCode = course.CourseCode });
                }
                else
                {
                    courses.Add(new GetAllCoursesToAssign { check = false, Id = course.Id, CourseName = course.CourseName, CourseCode = course.CourseCode });
                }
            }
            return courses;
        }

        //delete all Allocted courses
        public async Task DropAllCourses(int teacherId)
        {

            var teacherCourse = await _teacherWithCourseRepository.GetAll().Where(x => x.TeacherId == teacherId).ToListAsync();
            foreach (var s in teacherCourse)
            {
                await _teacherWithCourseRepository.DeleteAsync(s);

            }
        }

        //delete specific course
        public async Task DropCourse(int courseId, int teacherId)
        {
            var course = await _teacherWithCourseRepository.FirstOrDefaultAsync(x => x.CourseId == courseId && x.TeacherId == teacherId);
            await _teacherWithCourseRepository.DeleteAsync(course);
        }

        public async Task<List<TeacherTreeViews>> TeacherTreeViews()
        {
            var getTree = await _teacherWithCourseRepository.GetAll().Include(x => x.Teachers).Include(x => x.Courses).GroupBy(s => s.TeacherId).Select(j => new TeacherTreeViews()
            {
                Key = j.First().TeacherId,
                label = j.First().Teachers.FirstName,
                children = j.Select(f => new TeacherCourseDetails()
                {
                    Key = f.CourseId,
                    label = f.Courses.CourseName
                }).ToList()
            }).ToListAsync();

            return getTree;
        }
    }
}
