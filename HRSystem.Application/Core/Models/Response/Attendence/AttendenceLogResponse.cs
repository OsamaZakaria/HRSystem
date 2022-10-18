using System;
using System.Collections.Generic;

namespace HRSystem.Application.Core.Models.Response.Attendence
{
    public class AttendenceLogResponse
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan? TotalWorkingHours { get; private set; } = new TimeSpan();
        public IEnumerable<LogAttendenceDetails> LogDetails { get; set; }
        public void CalcTotalHours()
        {
            foreach (var log in LogDetails)
            {
                TotalWorkingHours += log.TimeTimeOut.HasValue && log.TimeIn.HasValue ?(log.TimeTimeOut - log.TimeIn): new TimeSpan();
            }
        }
    }
    public class LogAttendenceDetails
    {
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeTimeOut { get; set; }
    }
}
