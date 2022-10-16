using HRSystem.Application.Core.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Authentication
{
    internal sealed class UserIdentifierProvider : IUserIdentifierProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentifierProvider"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
        {
            string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId")
                ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

            UserId = new Guid(userIdClaim);
        }

        /// <inheritdoc />
        public Guid UserId { get; }
    }
}
