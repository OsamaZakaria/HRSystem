using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Response.Attendence;
using HRSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Attendence.Query.GetLastAttendanceAction
{
    public sealed class GetLastAttendanceActionQueryHandler : IQueryHandler<GetLastAttendanceActionQuery, GetLastAttendanceActionResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetLastAttendanceActionQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetLastAttendanceActionResponse> Handle(GetLastAttendanceActionQuery request, CancellationToken cancellationToken)
        {
            var log = await _dbContext.EmployeeAttendance.OrderByDescending(a => a.TimeIn).FirstOrDefaultAsync(a => a.TimeIn.Value.Date == DateTime.Now.Date && a.EmployeeId == request.EmployeeId);
            if (log == null || !log.TimeOut.HasValue)
                return new GetLastAttendanceActionResponse() { Action = "Check-Out" };

            return new GetLastAttendanceActionResponse() { Action = "Check-In" };
        }
    }
}
