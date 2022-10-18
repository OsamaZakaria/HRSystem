using HRSystem.Application.Department.Query;
using HRSystem.Web.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Web.Controllers
{

    public class DepartmentController : ApiController
    {
        public DepartmentController(IMediator mediator)
        : base(mediator)
        {
        }

        [HttpGet("/GetDepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await Mediator.Send(new GetDepartmentQuery
            {
            });
            return Ok(departments);
        }
    }
}
