
using HRSystem.Application.Core.Models.Employee;
using HRSystem.Application.Employee.Command;
using HRSystem.Application.Models.Employee;
using HRSystem.Domain.Core.Errors;
using HRSystem.Domain.Core.Result;
using HRSystem.Web.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRSystem.Web.Controllers
{
    public sealed class EmployeeController : ApiController
    {
        public EmployeeController(IMediator mediator)
        : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateEmployeeRequest createEmployeeRequest) =>
    await Result.Create(createEmployeeRequest, DomainErrors.General.UnProcessableRequest)
        .Map(request => new CreateEmployeeCommand() { Employee = createEmployeeRequest })
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateEmployeeRequest updateEmployeeRequest)
        {

            return await Result.Create(updateEmployeeRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateEmployeeCommand() { Employee = updateEmployeeRequest })
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
        }
      
    }
   

}
