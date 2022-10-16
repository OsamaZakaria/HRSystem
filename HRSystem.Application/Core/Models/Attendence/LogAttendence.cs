using System;

namespace HRSystem.Application.Core.Models.Attendence
{
    public class LogAttendence
    {
        public Guid EmployeeId { get; set; }
        public DateTime LogTime { get; set; }
    }
}
