using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Departments
{
    public class Department : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }
        [Required]
        [MaxLength(8)]
        public string DepartmentCode { get; set; }

    }
}
