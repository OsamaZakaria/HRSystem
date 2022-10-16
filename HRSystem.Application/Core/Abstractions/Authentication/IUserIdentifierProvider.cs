using System;

namespace HRSystem.Application.Core.Abstractions.Authentication
{
    public interface IUserIdentifierProvider
    {
        Guid UserId { get; }
    }
}
