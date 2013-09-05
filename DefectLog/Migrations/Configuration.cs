using System.Collections.Generic;
using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;

namespace DefectLog.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LogContext context)
        {
            var statuses = new List<Status>
            {
                new Status {Id = 1, Name = "Open", CssClass = ""},
                new Status {Id = 2, Name = "Fixed", CssClass = "info"},
                new Status {Id = 3, Name = "Unable to Reproduce", CssClass = "warning"},
                new Status {Id = 4, Name = "Complete", CssClass = "success"}
            };

            statuses.ForEach(status => context.Statuses.AddOrUpdate(status));

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category {Id = 1, CategoryName = "Example Category"},
                };

                categories.ForEach(product => context.Categories.AddOrUpdate(product));
            }

            var priorityLevels = new List<PriorityLevel>
            {
                new PriorityLevel {Id = 1, PriorityName = "Low"},
                new PriorityLevel {Id = 2, PriorityName = "Medium"},
                new PriorityLevel {Id = 3, PriorityName = "High"}
            };

            priorityLevels.ForEach(level => context.PriorityLevels.AddOrUpdate(level));

            var roles = new List<Role>
            {
                new Role {Id = 1, RoleName = "User"},
                new Role {Id = 2, RoleName = "Admin"}
            };

            roles.ForEach(role => context.Roles.AddOrUpdate(role));

            if (!context.Users.Any())
            {
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var password = BCrypt.Net.BCrypt.HashPassword("password", salt);

                var admin = new User
                {
                    Id = 1,
                    UserName = "Admin",
                    EmailAddress = "admin@email.com",
                    FirstName = "Mister",
                    LastName = "Manager",
                    Password = password,
                    PasswordSalt = salt,
                    RoleId = 2,
                    IsApproved = true,
                    IsDeleted = false
                };

                context.Users.AddOrUpdate(admin);
            }
        }
    }
}
