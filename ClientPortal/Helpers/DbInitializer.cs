using ClientPortal.Data;
using ClientPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Helpers
{
    public class DbInitializer
    {
        
        public static async Task Initialize(Application1DbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            context.Database.Migrate();

            await CreateRoles(context);
            await CreateDefaultUsers(context, userManager, roleManager, logger);
        }

        private static async Task CreateRoles(Application1DbContext context)
        {
            string[] roles = new string[] { "Super Administrator", "Administrator", "Broker", "Agent", "Client" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    var identityRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role
                    };

                    await roleStore.CreateAsync(identityRole);
                }
            }
        }

        private static async Task CreateDefaultUsers(Application1DbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            List<(string password, string role, ApplicationUser user)> users = new List<(string password, string role, ApplicationUser user)>()
            {
                ("asdfasdf1", "Super Administrator", new ApplicationUser()
                    {
                        CompanyName = "SuperAdmin Company",
                        Address = "1 Main St",
                        City = "Seattle",
                        ParentId = "",
                        Country = "USA",
                        CreationDate = DateTime.Now,
                        CreatorIP = "127.0.0.1",
                        DeactivationDate = null,
                        DeactivationReason = "",
                        EIN = "",
                        Email = "superadministrator@example.com",
                        Fax = "",
                        FaxExtension = "",
                        FirstName = "DbInitializer",
                        LastName = "LastName",
                        MiddleName = "",
                        MobilePhone = "1234567890",
                        OfficeExtension = "",
                        OfficePhone = "1234567890",
                        PhoneNumber = "+1234567890",
                        NormalizedEmail = "SUPERADMINISTRATOR@EXAMPLE.COM",
                        EmailConfirmed = true,
                        UserName = "superadministrator@example.com",
                        NormalizedUserName = "SUPERADMINISTRATOR@EXAMPLE.COM",
                        PostalCode = "98052",
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        State = "WA"
                    }
                )
            };

            foreach ((string password, string role, ApplicationUser user) row in users)
            {
                if (!context.Users.Any(u => u.UserName == row.user.Email))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(row.user, row.password);
                    row.user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
                    var result = userStore.CreateAsync(row.user);

                }
                
                await AssignRoles(userManager, row.user.Email, new string[] { row.role });
            }

            await context.SaveChangesAsync();

        }

        public static async Task<IdentityResult> AssignRoles(UserManager<ApplicationUser> userManager, string email, string[] roles)
        {
            
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }
        
    }
}
