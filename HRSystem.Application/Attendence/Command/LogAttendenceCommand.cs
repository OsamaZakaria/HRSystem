using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Attendence;
using HRSystem.Domain.Core.Result;

namespace HRSystem.Application.Attendence.Command
{
    public sealed class LogAttendenceCommand : ICommand<Result>
    {
        public LogAttendence LogAttendence { get; set; }
    }
}
