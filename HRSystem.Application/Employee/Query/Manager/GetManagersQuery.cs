using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Employee;
using System;
using System.Collections.Generic;

namespace HRSystem.Application.Employee.Query.Manager
{
    public sealed class GetManagersQuery : IQuery<List<GetManagersResponse>>
    {
        public Guid CurrentId { get; set; } = Guid.Empty;
    }
}
