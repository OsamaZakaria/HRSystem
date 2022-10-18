using HRSystem.Application.Abstractions.Messaging;
using HRSystem.Application.Core.Models;
using HRSystem.Data;
using HRSystem.Domain.Core.Errors;
using HRSystem.Domain.Core.Result;
using HRSystem.Domain.Core.ValueObjects;
using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSystem.Application.Authentication.Command.Login
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, Result<TokenResponse>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Result<Email> emailResult = Email.Create(request.Email);

                if (emailResult.IsFailure)
                {
                    return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == emailResult.Value);

                if (user == null)
                {
                    return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
                }


                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

                var passwordValid = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

                if (passwordValid == PasswordVerificationResult.Failed)
                {
                    return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
                }

                string token = user.CreateToken(_configuration);
                var isEmpoyee = _dbContext.Employees.Any(e => e.UserId == user.Id);
                return Result.Success(new TokenResponse(token, user.UserName, isEmpoyee));
            }
            catch (Exception ex)
            {
                return Result.Failure<TokenResponse>(new Domain.Core.Error("",ex.Message));
            }
        }
    }
}
