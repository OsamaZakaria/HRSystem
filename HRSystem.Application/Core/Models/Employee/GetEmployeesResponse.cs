using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Core.Models.Employee
{
    public class GetEmployeesResponse
    {
        public Guid Id { get; set; }
        public string ManagerName { get; set; }
        public string Email { get; set; }
        public IEnumerable<AllEmployees> Team { get; set; }
    }
    public class AllEmployees
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
    }
}
