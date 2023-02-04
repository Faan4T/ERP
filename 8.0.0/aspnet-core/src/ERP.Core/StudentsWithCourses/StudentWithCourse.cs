using Abp.Domain.Entities;
using ERP.Courses;
using ERP.Students;

namespace ERP.StudentsWithCourses
{
    public class StudentWithCourse : Entity<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int StudentId { get; set; }
        public int? CourseId { get; set; }
        public virtual Course Courses { get; set; }
        public virtual Student Students { get; set; }

    }
}
