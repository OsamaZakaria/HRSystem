using HRSystem.Application.Core.Models.Employee;
using HRSystem.Application.Employee.Command;
using HRSystem.Application.Employee.Query;
using HRSystem.Application.Employee.Query.Manager;
using HRSystem.Application.Models.Employee;
using HRSystem.Domain.Core.Errors;
using HRSystem.Domain.Core.Result;
using HRSystem.Web.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HRSystem.Web.Controllers
{
    public sealed class EmployeeController : ApiController
    {
        public EmployeeController(IMediator mediator)
        : base(mediator)
        {
        }

        [HttpPost("/UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {

            return await Result.Create(updateEmployeeRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateEmployeeCommand() { Employee = updateEmployeeRequest })
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
        }

        [HttpPost("/CreateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest createEmployeeRequest) =>
         await Result.Create(createEmployeeRequest, DomainErrors.General.UnProcessableRequest)
           .Map(request => new CreateEmployeeCommand() { Employee = createEmployeeRequest })
           .Bind(command => Mediator.Send(command))
           .Match(Ok, BadRequest);

        [HttpGet("/GetEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployees(int page, int pageSize, string search ="")
        {
            var employees = await Mediator.Send(new GetEmployeesQuery
            {
                PageIndex = page,
                PageSize = pageSize,
                search = search
            });
            return Ok(employees);
        }

        [HttpGet("/GetManagers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetManagers(Guid? currentEmployeeId)
        {
            var managers = await Mediator.Send(new GetManagersQuery
            {
                CurrentId = currentEmployeeId?? Guid.Empty
            });
            return Ok(managers);
        }
        [HttpGet("/GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await Mediator.Send(new GetEmployeeByIdQuery
            {
                Id = id
            });
            return Ok(employee);
        }
    }


}
