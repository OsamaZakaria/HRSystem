using System;

namespace HRSystem.Application.Core.Models.Employee
{
    public class GetEmployeeByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mobile { get; set; }
        public Nullable<Guid> ManagerId { get; set; }
        public Nullable<Guid> DepartmentId { get; set; }
    }
}
