using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Teachers
{
    public class TeacherTreeViews
    {
        public int Key { get; set; }
        public string label { get; set; }
        public virtual List<TeacherCourseDetails> children { get; set; }
    }
    public class TeacherCourseDetails
    {
        public int? Key { get; set; }
        public string label { get; set; }

    }
}
