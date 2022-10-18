using System;
using System.Collections.Generic;

namespace HRSystem.Application.Core.Models.Response.Attendence
{
    public class AttendenceLogResponse
    {
        bool _hasViolation;
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan? TotalWorkingHours { get; private set; } = new TimeSpan();
        public bool HasViolation { get { return  TotalWorkingHours < TimeSpan.Parse("8:00:00");} set { _hasViolation =  value; } }
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
