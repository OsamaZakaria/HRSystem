using HRSystem.Domain.Core;

namespace HRSystem.Application.Errors
{
    internal static class ValidationErrors
    {
        internal static class Login
        {
            internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The email is required.");

            internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
        }
        internal static class CreateEmployee
        {
            internal static Error EmailIsRequired => new Error("Employee.EmailIsRequired", "The email is required.");
            internal static Error NameRequired => new Error("Employee.NameRequired", "The name is required.");
            internal static Error AddressRequired => new Error("Employee.AddressRequired", "The address is required.");
            internal static Error MobileRequired => new Error("Employee.MobileRequired", "The mobile is required.");
            internal static Error DepartmentRequired => new Error("Employee.DepartmentRequired", "The department is required.");
            internal static Error BirthDateRequired => new Error("Employee.BirthDateRequired", "The birthDate is required.");
            internal static Error BirthDateCanNotBeInTheFuture => new Error("Employee.BirthDateInTheFuture", "BirthDate can not be in the future.");
            internal static Error EmployeeAgeNotValid => new Error("Employee.EmployeeAgeNotValid", "Employee should be older than or equal 18 years old.");
        }
    }
}
