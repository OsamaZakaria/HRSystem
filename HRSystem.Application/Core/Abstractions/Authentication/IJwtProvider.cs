using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Core.Abstractions.Authentication
{
    public interface IJwtProvider
    {
        string Create(ApplicationUser user);
    }
}
