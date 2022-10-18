using FluentValidation;
using HRSystem.Application.Errors;
using HRSystem.Application.Models.Employee;

namespace HRSystem.Application.Employee.Command
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee.Name).NotEmpty().WithError(ValidationErrors.CreateEmployee.NameRequired);
            RuleFor(x => x.Employee.DepartmentId).NotEqual(System.Guid.Empty).WithError(ValidationErrors.CreateEmployee.DepartmentRequired);
            RuleFor(x => x.Employee.BirthDate).NotEqual(System.DateTime.MinValue).WithError(ValidationErrors.CreateEmployee.BirthDateRequired);
            RuleFor(x => x.Employee.BirthDate).LessThan(System.DateTime.UtcNow).WithError(ValidationErrors.CreateEmployee.BirthDateCanNotBeInTheFuture);
            RuleFor(x => x.Employee.Address).NotEmpty().WithError(ValidationErrors.CreateEmployee.AddressRequired);
            RuleFor(x => x.Employee.Mobile).NotEmpty().WithError(ValidationErrors.CreateEmployee.MobileRequired);
        }
    }
}
