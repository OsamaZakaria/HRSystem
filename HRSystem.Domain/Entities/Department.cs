using HRSystem.Domain.Core.Primitives;
using System;
using System.Collections.Generic;

namespace HRSystem.Domain.Entities
{
    public class Department : Entity<Guid>
    {
        private Department(string name, Guid? id = null)
        {
            if (id.HasValue)
                Id = id.Value;
            Name = name;
        }
        private Department()
        { 
        }
        public string Name { get; private set; }
        public virtual List<Employee> Employees { get; set; }

        public static Department Create(string name, Guid? id = null)
        {
            return new Department(name, id);
        }
    }
}
