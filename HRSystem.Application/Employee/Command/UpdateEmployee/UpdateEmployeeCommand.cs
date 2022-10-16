using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Employee;
using HRSystem.Application.Models.Employee;
using HRSystem.Domain.Core.Result;
using System;

namespace HRSystem.Application.Employee.Command
{
    public sealed class UpdateEmployeeCommand : ICommand<Result>
    {
        public UpdateEmployeeRequest Employee { get; set; }
    }
}
