using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Response.Department;
using HRSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Department.Query
{
    public class GetDepartmentQueryHandler : IQueryHandler<GetDepartmentQuery, List<GetDepartmentResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetDepartmentQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<GetDepartmentResponse>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await (from department in _dbContext.Departments
                          select new GetDepartmentResponse()
                          { 
                              Id = department.Id,
                              Name = department.Name
                          }).ToListAsync();
        }
    }
}
