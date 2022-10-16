using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Data;
using HRSystem.Domain.Core.Result;
using HRSystem.Domain.Core.ValueObjects;
using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Employee.Command
{
    public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Result>
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateEmployeeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Result<Email> email = Email.Create(request.Employee.Email);
            if(email.IsFailure)
                return Result.Failure(email.Error);

            var employeeExists = _dbContext.Employees.FirstOrDefault(e => e.Email.ToLower() == request.Employee.Email.ToLower());

            if (employeeExists != null)
                return Result.Failure(new Domain.Core.Error("Employee.Exists", "Employee Email Already Exists!"));
            var user = new ApplicationUser()
            {
                Id = System.Guid.NewGuid(),
                UserName = email.Value,
                Email = email.Value,
                LockoutEnabled = false,
                PhoneNumber = request.Employee.Mobile,
                EmailConfirmed = true
            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            //Generate Random password after implement EmailService
            passwordHasher.HashPassword(user, "new@123");

            var employee = HRSystem.Domain.Entities.Employee.Create(name: request.Employee.Name,
                                                                    address: request.Employee.Address,
                                                                    email: request.Employee.Address,
                                                                    birthDate: request.Employee.BirthDate,
                                                                    mobile: request.Employee.Mobile,
                                                                    managerId: request.Employee.ManagerId,
                                                                    user: user,
                                                                    departmentId: request.Employee.DepartmentId
                                                                    );
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            //Send Employee Email with the new user name and password

            return Result.Success();
        }
    }
}
