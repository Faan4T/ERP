using Abp.Zero.EntityFrameworkCore;
using ERP.Authorization.Roles;
using ERP.Authorization.Users;
using ERP.Courses;
using ERP.Departments;
using ERP.MultiTenancy;
using ERP.Students;
using ERP.StudentsWithCourses;
using ERP.Teachers;
using ERP.TeacherWithCourses;
using Microsoft.EntityFrameworkCore;

namespace ERP.EntityFrameworkCore
{
    public class ERPDbContext : AbpZeroDbContext<Tenant, Role, User, ERPDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentWithCourse> CoursesEnrolled { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherWithCourse> TeacherWithCourses { get; set; }
        public ERPDbContext(DbContextOptions<ERPDbContext> options)
            : base(options)
        {
        }
    }
}
