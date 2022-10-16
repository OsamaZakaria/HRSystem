using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Data;
using HRSystem.Domain.Core.Result;
using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Attendence.Command
{
    public sealed class LogAttendenceCommandHandler : ICommandHandler<LogAttendenceCommand, Result>
    {
        private readonly ApplicationDbContext _dbContext;
        public LogAttendenceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(LogAttendenceCommand request, CancellationToken cancellationToken)
        {

            var lastSignIn = await _dbContext.EmployeeAttendance.FirstOrDefaultAsync(a => a.TimeOut == null
             && a.EmployeeId == request.LogAttendence.EmployeeId
             && a.TimeIn.Value.Date == System.DateTime.UtcNow.Date);

            //Sign In
            if (lastSignIn == null)
                _dbContext.EmployeeAttendance.Add(EmployeeAttendance.Create(request.LogAttendence.EmployeeId, request.LogAttendence.LogTime));
            else
            {
                lastSignIn.Update(request.LogAttendence.LogTime);
                _dbContext.Entry(lastSignIn).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
            return Result.Success();

        }
    }
}
