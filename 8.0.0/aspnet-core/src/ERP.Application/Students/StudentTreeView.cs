using Abp.Domain.Entities;
using ERP.StudentsWithCourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Students
{
    public class StudentTreeView 
    {
        public int Key { get; set; }
        public string label { get; set; }
        public virtual List<CourseDetails> children { get; set; }
    }
    public class CourseDetails
    {
        public int? Key { get; set; }
        public string label { get; set; }

    }
}
