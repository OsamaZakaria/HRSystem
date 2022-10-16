using Microsoft.AspNetCore.Identity;
using System;

namespace HRSystem.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Name { get; set; }
    }
}
