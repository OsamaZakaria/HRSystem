using HRSystem.Domain.Core;
using HRSystem.Web.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public ApiController(IMediator mediator) => Mediator = mediator;

        protected IMediator Mediator { get; }

        protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));

        protected new IActionResult Ok(object value) => base.Ok(value);

        protected new IActionResult NotFound() => base.NotFound();
    }
}
