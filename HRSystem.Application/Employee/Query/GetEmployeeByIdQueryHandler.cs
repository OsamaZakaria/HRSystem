using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models.Employee;
using HRSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Employee.Query
{
    public sealed class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetEmployeeByIdQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.Select(e => 
                     new GetEmployeeByIdResponse()
                     {
                         Id = e.Id,
                         Address = e.Address,
                         BirthDate = e.BirthDate,
                         DepartmentId = e.DepartmentId,
                         Email = e.Email,
                         ManagerId = e.ManagerId,
                         Mobile = e.Mobile,
                         Name = e.Name,
                     }
                ).FirstOrDefaultAsync(e => e.Id == request.Id);
        }
    }
}
