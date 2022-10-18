using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Response.Attendence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Attendence.Query.GetLastAttendanceAction
{
    public sealed class GetLastAttendanceActionQuery : IQuery<GetLastAttendanceActionResponse>
    {
        public Guid EmployeeId { get; set; }
    }
}
