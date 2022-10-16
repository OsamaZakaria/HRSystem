using HRSystem.Domain.Entities;
using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<EmployeeAttendance> EmployeeAttendance { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Employee>()
           .HasOne(x => x.Manager).WithMany(x => x.Employees).HasForeignKey(x => x.ManagerId)
           .Metadata.DeleteBehavior = DeleteBehavior.Restrict;


            builder.Entity<Department>().HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            builder.Entity<Employee>().HasMany(d => d.Attendances)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeId);

            builder.Entity<ApplicationUser>()
             .HasOne(x => x.Employee).WithOne(e => e.User).HasForeignKey<Employee>(e => e.UserId);

            builder.Entity<IdentityRole>(e => e.ToTable(name: "Role"));


            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            builder.Entity<Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
            this.SeedRoles(builder);
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
            this.SeedDepartments(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5"),
                UserName = "Admin",
                Email = "admin@hr.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                EmailConfirmed = true
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            passwordHasher.HashPassword(user, "Admin@123");

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role() { Id = Guid.Parse("fab4fac1-c546-41de-aebc-a14da6895711"), Name = "SystemAdmin", ConcurrencyStamp = "1", NormalizedName = "System Admin" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>() { RoleId = Guid.Parse("fab4fac1-c546-41de-aebc-a14da6895711"), UserId = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5") }
                );
        }
        private void SeedDepartments(ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
                Department.Create("Software", Guid.NewGuid()),
                Department.Create("HR", Guid.NewGuid()));
        }
    }
}
