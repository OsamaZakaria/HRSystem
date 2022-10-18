using System;

namespace HRSystem.Application.Models.Employee
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; } = DateTime.MinValue.ToString();
        public string Mobile { get; set; }
        public Nullable<Guid> ManagerId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
