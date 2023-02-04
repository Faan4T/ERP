using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Teachers.Dto
{
    public class PagedTeacherResultDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
