using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Students
{
    public class StudentAttendence : Entity<int>
    {
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public int percentage { get; set; }
        public bool Status { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public string Section { get; set; }

    }
}
