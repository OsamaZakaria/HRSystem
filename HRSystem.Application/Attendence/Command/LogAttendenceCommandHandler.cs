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

            var lastSign = await _dbContext.EmployeeAttendance.FirstOrDefaultAsync(a => a.TimeOut == null
             && a.EmployeeId == request.LogAttendence.EmployeeId
             && a.TimeIn.Value.Date == System.DateTime.UtcNow.Date);

            //Sign In
            if (lastSign == null)
                _dbContext.EmployeeAttendance.Add(EmployeeAttendance.Create(request.LogAttendence.EmployeeId, DateTime.UtcNow));
            else
            {
                lastSign.Update(DateTime.UtcNow);
                _dbContext.Entry(lastSign).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
            return Result.Success();

        }
    }
}
