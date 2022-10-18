using HRSystem.Application.Attendence.Command;
using HRSystem.Application.Attendence.Query.GetAttendenceLog;
using HRSystem.Application.Core.Models.Attendence;
using HRSystem.Domain.Core.Errors;
using HRSystem.Domain.Core.Result;
using HRSystem.Web.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRSystem.Web.Controllers
{
    [Authorize]
    public class AttendenceLogController : HRSystem.Web.Controllers.ApiController
    {
        public AttendenceLogController(IMediator mediator)
: base(mediator)
        {
        }

        [HttpPost("/Log")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Log(LogAttendence log) =>
         await Result.Create(log, DomainErrors.General.UnProcessableRequest)
         .Map(request => new LogAttendenceCommand() { LogAttendence = log })
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);

        [HttpGet("/GetLog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLog(int page, int pageSize)
        {
            var test = User;
            var projects = await Mediator.Send(new GetAttendenceLogQuery
            {
                PageIndex = page,
                PageSize = pageSize,
            });
            return Ok(projects);
        }
    }
}
