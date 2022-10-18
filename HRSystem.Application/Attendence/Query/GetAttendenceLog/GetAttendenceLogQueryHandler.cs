using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models;
using HRSystem.Application.Core.Models.Response.Attendence;
using HRSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Application.Attendence.Query.GetAttendenceLog
{
    public sealed class GetAttendenceLogQueryHandler : IQueryHandler<GetAttendenceLogQuery, PagedList<AttendenceLogResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetAttendenceLogQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedList<AttendenceLogResponse>> Handle(GetAttendenceLogQuery request, CancellationToken cancellationToken)
        {

            var logs = await Task.Run(() => (from attendence in _dbContext.EmployeeAttendance.AsEnumerable()
                        join employee in _dbContext.Employees on attendence.EmployeeId equals employee.Id
                        orderby attendence.TimeIn
                        group attendence by new
                        {
                            attendence.EmployeeId,
                            employee.Name,
                            attendence.TimeIn.Value.Date
                        } into _log
                        select new AttendenceLogResponse()
                        {
                            EmployeeId = _log.Key.EmployeeId,
                            LogDate = _log.Key.Date.Date,
                            Name = _log.Key.Name,
                            LogDetails = _log.Select(s => new LogAttendenceDetails()
                            {
                                TimeIn = (s.TimeIn)?.TimeOfDay,
                                TimeTimeOut = (s.TimeOut)?.TimeOfDay
                            }
                            ).AsEnumerable(),
                        }
                        ));

            int totalCount = logs.Count();

            IEnumerable<AttendenceLogResponse> logPage = logs
              .Skip((request.PageIndex) * request.PageSize)
              .Take(request.PageSize)
              .ToArray();

            foreach (var log in logPage)
            {
                log.CalcTotalHours();
            }
            return new PagedList<AttendenceLogResponse>(logPage, request.PageIndex, request.PageSize, totalCount);
        }
    }
}
