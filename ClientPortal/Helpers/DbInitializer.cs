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
        
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            context.Database.Migrate();

            await CreateRoles(context);
            await CreateDefaultUsers(context, userManager, roleManager, logger);
        }

        private static async Task CreateRoles(ApplicationDbContext context)
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

        private static async Task CreateDefaultUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            List<(string password, string role, ApplicationUser user)> users = new List<(string password, string role, ApplicationUser user)>()
            {
                ("Evan0707!", "Super Administrator", new ApplicationUser()
                    {
                            CompanyName = "Reservation Services Intl",
                            Address = "1000 Parnell Ct",
                            City = "Deltona",
                            ParentId = "",
                            Country = "USA",
                            CreationDate = DateTime.Now,
                            CreatorIP = "127.0.0.1",
                            DeactivationDate = null,
                            DeactivationReason = "",
                            EIN = "",
                            Email = "dennis@accessrsi.com",
                            Fax = "",
                            FaxExtension = "",
                            FirstName = "Dennis",
                            LastName = "Chipps",
                            MiddleName = "E",
                            MobilePhone = "4074273755",
                            OfficeExtension = "",
                            OfficePhone = "4074273755",
                            PhoneNumber = "+4074273755",
                            NormalizedEmail = "DENNIS@ACCESSRSI.COM",
                            EmailConfirmed = true,
                            UserName = "dennis@accessrsi.com",
                            NormalizedUserName = "DENNIS@ACCESSRSI.COM",
                            PostalCode = "32738",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = "FL"
                    }
                ),
                ("Changeme123!", "Administrator", new ApplicationUser()
                    {
                        CompanyName = "Reservation Services Intl",
                        Address = "501 N Wymore Rd",
                        City = "Winter Park",
                        ParentId = "",
                        Country = "USA",
                        CreationDate = DateTime.Now,
                        CreatorIP = "127.0.0.1",
                        DeactivationDate = null,
                        DeactivationReason = "",
                        EIN = "",
                        Email = "jec361@me.com",
                        Fax = "",
                        FaxExtension = "",
                        FirstName = "Jim",
                        LastName = "Carey",
                        MiddleName = "",
                        MobilePhone = "8636609597",
                        OfficeExtension = "",
                        OfficePhone = "8634504288",
                        PhoneNumber = "+8634504288",
                        NormalizedEmail = "JEC361@ME.COM",
                        EmailConfirmed = true,
                        UserName = "jec361@me.com",
                        NormalizedUserName = "JEC361@ME.COM",
                        PostalCode = "32789",
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        State = "FL"
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
