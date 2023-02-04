using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Departments.Dto
{
    public interface IDepartmentAppService : IApplicationService
    {
      
        Task CreateDepartment(DepartmentDto input);
        Task DeleteDepartment(int id);
        Task UpdateDepartment(DepartmentDto input);
        List<DepartmentDto> GetAll(PagedDepartmentsResultRequestDto input);
        Task<Department> GetById(int id);
    }
}
