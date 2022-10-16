using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Models.Employee;
using HRSystem.Domain.Core.Result;
using System;

namespace HRSystem.Application.Employee.Command
{
    public sealed class CreateEmployeeCommand : ICommand<Result>
    {
        public CreateEmployeeRequest Employee { get; set; }
    }
}
