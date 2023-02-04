using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Teachers.Dto
{
    public class GetAllCoursesToAssign
    {
        public bool check { get; set; }
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

    }
}
