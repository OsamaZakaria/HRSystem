using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models;
using HRSystem.Application.Core.Models.Response.Attendence;
using System.Collections.Generic;

namespace HRSystem.Application.Attendence.Query.GetAttendenceLog
{
    public sealed class GetAttendenceLogQuery : IQuery<PagedList<AttendenceLogResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
