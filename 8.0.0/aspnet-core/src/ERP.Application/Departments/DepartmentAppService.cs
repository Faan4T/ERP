using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using ERP.Authorization;
using ERP.Departments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Departments
{
    [AbpAuthorize(PermissionNames.Pages_Departments)]
    public class DepartmentAppService : ApplicationService, IDepartmentAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        public DepartmentAppService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task CreateDepartment(DepartmentDto input)
        {
            var student = _departmentRepository.FirstOrDefault(p => p.DepartmentCode == input.DepartmentCode);
            if (student != null)
            {
                throw new UserFriendlyException("There is already a Department with given Department Code");
            }

            await _departmentRepository.InsertAsync(ObjectMapper.Map<Department>(input));
        }

        public async Task DeleteDepartment(int id)
        {
            var item = _departmentRepository.FirstOrDefault(x => x.Id == id);
            await _departmentRepository.DeleteAsync(item);
        }

        public List<DepartmentDto> GetAll(PagedDepartmentsResultRequestDto input)
        {
            var departments = _departmentRepository.GetAllList().WhereIf(
                !input.Keyword.IsNullOrWhiteSpace(), x => x.DepartmentName.Contains(input.Keyword)
                || x.DepartmentCode.Contains(input.Keyword)).ToList();

            return new List<DepartmentDto>(ObjectMapper.Map<List<DepartmentDto>>(departments));
        }

        public async Task<Department> GetById(int id)
        {
            var query = _departmentRepository.FirstOrDefault(p => p.Id == id);
            if (query == null)
            {
                throw new UserFriendlyException("There is No Department with given Department Code");
            }
            return _departmentRepository.FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateDepartment(DepartmentDto input)
        {
            var query = _departmentRepository.FirstOrDefault(p => p.Id == input.Id);
            if (query == null)
            {
                throw new UserFriendlyException("Record Not Found");
            }
            query.DepartmentName = input.DepartmentName;
            query.DepartmentCode = input.DepartmentCode;
            await _departmentRepository.UpdateAsync(query);
        }
    }
}

