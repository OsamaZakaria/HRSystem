using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models;
using HRSystem.Application.Core.Models.Employee;
using HRSystem.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace HRSystem.Application.Employee.Query
{
    public sealed class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery, PagedList<GetEmployeesResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetEmployeesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedList<GetEmployeesResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await Task.Run(() => (from man in _dbContext.Employees.AsEnumerable()
                                  join emp in _dbContext.Employees
                                  on man.Id equals emp.ManagerId 
                                  into EmployeeGroups
                                  where
                                  man.ManagerId == null &&(
                                  man.Name.Contains(request.search ?? "") || man.Email.Contains(request.search ?? ""))
                                  orderby man.Name
                                  select new GetEmployeesResponse()
                                   {
                                       Email = man.Email,
                                       ManagerName = man.Name,
                                       Id = man.Id,
                                       Team = EmployeeGroups.Select(e =>
                                                           new AllEmployees()
                                                           {
                                                               Id = e.Id,
                                                               Email = e.Email,
                                                               EmployeeName = e.Name
                                                           }).AsEnumerable()
                                   }));



                int totalCount = employees.Count();

                IEnumerable<GetEmployeesResponse> employeesPage = employees
                  .Skip((request.PageIndex) * request.PageSize)
                  .Take(request.PageSize)
                  .ToArray();

                return new PagedList<GetEmployeesResponse>(employeesPage, request.PageIndex, request.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                return new PagedList<GetEmployeesResponse>(new List<GetEmployeesResponse>(), request.PageIndex, request.PageSize, 0);

            }
        }
    }
}
