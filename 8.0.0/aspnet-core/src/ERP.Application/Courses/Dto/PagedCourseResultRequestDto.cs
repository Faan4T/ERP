using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Courses.Dto
{
    public class PagedCourseResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

    }
}
