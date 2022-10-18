using System;

namespace HRSystem.Application.Core.Models.Employee
{
   public  class UpdateEmployeeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mobile { get; set; }
        public Nullable<Guid> ManagerId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
