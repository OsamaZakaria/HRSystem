using HRSystem.Domain.Core.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        private Employee() { }

        private Employee(string name, string address, string email, DateTime birthDate, string mobile, Nullable<Guid> managerId, ApplicationUser user, Guid departmentId)
        {
            Name = name;
            Address = address;
            Email = email;
            BirthDate = birthDate;
            Mobile = mobile;
            ManagerId = managerId;
            User = user;
            DepartmentId = departmentId;
        }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Mobile { get; private set; }
        public Nullable<Guid> ManagerId { get; private set; }
        public Nullable<Guid> DepartmentId { get; private set; }
        [ForeignKey("ManagerId")] 
        public virtual Employee Manager { get; private set; }
        public virtual List<Employee> Employees { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; private set; }
        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; private set; }

        public virtual List<EmployeeAttendance> Attendances { get; set; }

        public static Employee Create(string name, string address, string email, DateTime birthDate, string mobile, Nullable<Guid> managerId, ApplicationUser user, Guid departmentId)
        {
            return new Employee(name, address, email, birthDate, mobile, managerId, user,departmentId);
        }
        public void Update(string name, string address, DateTime birthDate, string mobile, Nullable<Guid> managerId, Guid departmentId)
        {
            Name = name;
            Address = address;
            BirthDate = birthDate;
            Mobile = mobile;
            ManagerId = managerId;
            DepartmentId = departmentId;
        }
    }
}
