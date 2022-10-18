using HRSystem.Application.Authentication.Command.Login;
using HRSystem.Application.Core.Models;
using HRSystem.Application.Core.Models.Authentication;
using HRSystem.Domain.Core.Errors;
using HRSystem.Domain.Core.Result;
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

    public class AuthController : HRSystem.Web.Controllers.ApiController
    {
        public AuthController(IMediator mediator)
: base(mediator)
        {
        }

        [HttpPost("/Login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequest loginRequest) =>
       await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
       .Map(request => new LoginCommand(request.Email, request.Password))
       .Bind(command => Mediator.Send(command))
       .Match(Ok, BadRequest);
    }
}
