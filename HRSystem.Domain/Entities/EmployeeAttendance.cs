using HRSystem.Domain.Core.Primitives;
using System;

namespace HRSystem.Domain.Entities
{
    public class EmployeeAttendance : Entity<Guid>
    {
        private EmployeeAttendance() { }
        private EmployeeAttendance(Guid employeeId, DateTime? timeIn, DateTime? timeOut)
        {
            EmployeeId = employeeId;
            TimeIn = timeIn;
            TimeOut = timeOut;
        }
        public Guid EmployeeId { get; private set; }
        public DateTime? TimeIn { get; private set; }
        public DateTime? TimeOut { get; private set; }
        public virtual Employee Employee { get; private set; }

        public static EmployeeAttendance Create(Guid employeeId, DateTime? timeIn, DateTime? timeOut = null)
        {
            return new EmployeeAttendance(employeeId, timeIn, timeOut);
        }
        public void Update(DateTime timeOut)
        {
            TimeOut = timeOut;
        }
    }
}
