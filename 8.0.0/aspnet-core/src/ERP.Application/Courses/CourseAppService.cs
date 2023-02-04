using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using ERP.Authorization;
using ERP.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Courses
{
    [AbpAuthorize(PermissionNames.Pages_Courses)]
    public class CourseAppService : ApplicationService, ICourseAppService
    {
        private readonly IRepository<Course> _courseRepository;
        public CourseAppService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task CreateCourse(CourseDto input)
        {
            var student = _courseRepository.FirstOrDefault(p => p.CourseCode == input.CourseCode);
            if (student != null)
            {
                throw new UserFriendlyException("There is already a course with given Course Code");
            }

            await _courseRepository.InsertAsync(ObjectMapper.Map<Course>(input));
        }

        public async Task DeleteCourse(int id)
        {
            var item = _courseRepository.FirstOrDefault(x => x.Id == id);
            await _courseRepository.DeleteAsync(item);
        }

        public List<CourseDto> GetAll(PagedCourseResultRequestDto input)
        {
            var course = _courseRepository.GetAllList().WhereIf(
                !input.Keyword.IsNullOrWhiteSpace(), x => x.CourseName.Contains(input.Keyword)
                || x.CourseCode.Contains(input.Keyword)).ToList();

            return new List<CourseDto>(ObjectMapper.Map<List<CourseDto>>(course));
        }

        public async Task<Course> GetById(int id)
        {
            var course = _courseRepository.FirstOrDefault(p => p.Id == id);
            if (course == null)
            {
                throw new UserFriendlyException("There is No course with given Course Code");
            }
            return _courseRepository.FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateCourse(CourseDto input)
        {
            var course = _courseRepository.FirstOrDefault(p => p.Id == input.Id);
            if (course == null)
            {
                throw new UserFriendlyException("There is No course with given Course Code");
            }
            else
            {
                course.CourseName = input.CourseName;
                course.CourseCode = input.CourseCode;
                await _courseRepository.UpdateAsync(course);
            }
        }
    }
}

