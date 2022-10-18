using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Employee;
using System;

namespace HRSystem.Application.Employee.Query
{
    public class GetEmployeeByIdQuery : IQuery<GetEmployeeByIdResponse>
    {
        public  Guid Id { get; set; }
    }
}
