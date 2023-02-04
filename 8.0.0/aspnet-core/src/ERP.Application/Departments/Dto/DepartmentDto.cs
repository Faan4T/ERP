using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ERP.Departments.Dto
{
    [AutoMap(typeof(Department))]
    public class DepartmentDto: EntityDto<int>
    {
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string DepartmentCode { get; set; }
    }
}
