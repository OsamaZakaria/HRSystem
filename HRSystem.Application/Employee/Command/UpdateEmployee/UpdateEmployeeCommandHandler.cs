using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Data;
using HRSystem.Domain.Core.Result;
using HRSystem.Domain.Core.ValueObjects;
using HRSystem.Domain.Entities;
//using HRSystem.Domain.Repository;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Employee.Command
{
    public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, Result>
    {
        private readonly ApplicationDbContext _dbContext;
        public UpdateEmployeeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
           
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == request.Employee.Id);

            if (employee == null)
                return Result.Failure(new Domain.Core.Error("Employee.NotExists", "Employee Not Exists!"));

            employee.Update(name: request.Employee.Name,
                                                                    address: request.Employee.Address,
                                                                    birthDate: request.Employee.BirthDate,
                                                                    mobile: request.Employee.Mobile,
                                                                    managerId: request.Employee.ManagerId,
                                                                    departmentId: request.Employee.DepartmentId
                                                                    );
            _dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
