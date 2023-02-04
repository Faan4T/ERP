using Abp.Domain.Entities;
using ERP.Courses;
using ERP.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TeacherWithCourses
{
    public class TeacherWithCourse : Entity<int>
    {
        public int TeacherId { get; set; }
        public int? CourseId { get; set; }
        public virtual Course Courses { get; set; }
        public virtual Teacher Teachers { get; set; }

    }
}
