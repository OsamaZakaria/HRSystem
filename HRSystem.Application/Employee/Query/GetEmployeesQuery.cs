using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models;
using HRSystem.Application.Core.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Employee.Query
{
    public sealed class GetEmployeesQuery : IQuery<PagedList<GetEmployeesResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string search { get; set; } = "";
    }
}
