using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Employee;
using HRSystem.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Application.Employee.Query.Manager
{
    public class GetManagersQueryHandler : IQueryHandler<GetManagersQuery, List<GetManagersResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetManagersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<GetManagersResponse>> Handle(GetManagersQuery request, CancellationToken cancellationToken)
        {
            return await (from employee in _dbContext.Employees
                          where employee.Id != request.CurrentId
                          select new GetManagersResponse()
                          {
                              Id = employee.Id,
                              Name = employee.Name
                          }).ToListAsync();
        }
    }
}
