using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientPortal.Data;
using ClientPortal.Models;
using Codes.Service.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ClientPortal.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class LinkIdentityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CodesDbContext _codesContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LinkIdentityController(ApplicationDbContext context, CodesDbContext codesContext, UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager, ILogger<LinkIdentityController> logger)
        {
            _context = context;
            _codesContext = codesContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            throw new NotImplementedException("See source code on using this method");

            // Uncomment code below and run on dev system to use this method:

            //try
            //{
            //    string brokerMessage = await _BrokersAddAsync();
            //    string clientMessage = await _ClientAddAsync();
            //    string agentMessage = await _AgentAddAsync();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

            //return Ok();
        }

        private async Task<string> _AgentAddAsync()
        {
            string[] agentRole = new string[] { "Agent" };
            string message = "Agent Message: -> ";

            try
            {
                var model = _codesContext.Agents.Where(x => x.IsActive && x.ApplicationReference == null && x.Email != null
                    && x.AgentFirstName != null && x.AgentLastName != null && x.AgentLastName.Length > 0 && x.AgentFirstName.Length > 0);

                foreach (var row in model)
                {
                    if (!_userManager.Users.Any(u => u.UserName == row.Email))
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            CompanyName = row.CompanyName,
                            Address = row.Address,
                            City = row.City,
                            ParentId = "",
                            Country = row.Country,
                            CreationDate = DateTime.Now,
                            CreatorIP = "127.0.0.1",
                            DeactivationDate = null,
                            DeactivationReason = "",
                            EIN = "",
                            Email = row.Email,
                            Fax = "",
                            FaxExtension = "",
                            FirstName = row.AgentFirstName,
                            LastName = row.AgentLastName,
                            MiddleName = row.AgentMiddleName,
                            MobilePhone = row.MobilePhone,
                            OfficeExtension = "",
                            OfficePhone = row.OfficePhone,
                            PhoneNumber = "+" + row.MobilePhone ?? (row.OfficePhone ?? ""),
                            NormalizedEmail = row.Email.ToUpper(),
                            EmailConfirmed = true,
                            UserName = row.Email,
                            NormalizedUserName = row.Email.ToUpper(),
                            PostalCode = row.PostalCode,
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = row.State,
                            AgentId = row.AgentId
                        };

                        var password = new PasswordHasher<ApplicationUser>();
                        var hashed = password.HashPassword(user, GetRandomString(16));
                        user.PasswordHash = hashed;

                        var userStore = new UserStore<ApplicationUser>(_context);
                        var result = await userStore.CreateAsync(user);

                        row.ApplicationReference = user.Id;
                    }
                    else
                    {
                        var usr = await _userManager.FindByEmailAsync(row.Email);
                        row.ApplicationReference = usr.Id;
                    }

                    await _context.SaveChangesAsync();
                    await AssignRoles(_userManager, row.Email, agentRole);
                }

                await _codesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                message += $"Error: {ex.Message}";

                return message;
            }

            message += "Success";

            return message;
        }

        private async Task<string> _ClientAddAsync()
        {
            string[] clientRole = new string[] { "Client" };
            string message = "Client Message: -> ";

            try
            {
                var model = _codesContext.Clients.Where(x => x.IsActive && x.ApplicationReference == null && x.Email != null
                    && x.ContactFirstName != null && x.ContactLastName != null && x.ContactLastName.Length > 0 && x.ContactFirstName.Length > 0);

                foreach (var row in model)
                {
                    if (!_userManager.Users.Any(u => u.UserName == row.Email))
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            CompanyName = row.CompanyName,
                            Address = row.Address,
                            City = row.City,
                            ParentId = "",
                            Country = row.Country,
                            CreationDate = DateTime.Now,
                            CreatorIP = "127.0.0.1",
                            DeactivationDate = null,
                            DeactivationReason = "",
                            EIN = "",
                            Email = row.Email,
                            Fax = "",
                            FaxExtension = "",
                            FirstName = row.ContactFirstName,
                            LastName = row.ContactLastName,
                            MiddleName = row.ContactMiddleName,
                            MobilePhone = row.MobilePhone,
                            OfficeExtension = "",
                            OfficePhone = row.OfficePhone,
                            PhoneNumber = "+" + row.MobilePhone ?? (row.OfficePhone ?? ""),
                            NormalizedEmail = row.Email.ToUpper(),
                            EmailConfirmed = true,
                            UserName = row.Email,
                            NormalizedUserName = row.Email.ToUpper(),
                            PostalCode = row.PostalCode,
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = row.State,
                            ClientId = row.ClientId
                        };

                        var password = new PasswordHasher<ApplicationUser>();
                        var hashed = password.HashPassword(user, GetRandomString(16));
                        user.PasswordHash = hashed;

                        var userStore = new UserStore<ApplicationUser>(_context);
                        var result = await userStore.CreateAsync(user);

                        row.ApplicationReference = user.Id;
                    }
                    else
                    {
                        var usr = await _userManager.FindByEmailAsync(row.Email);
                        row.ApplicationReference = usr.Id;
                    }

                    await AssignRoles(_userManager, row.Email, clientRole);
                }

                await _context.SaveChangesAsync();
                await _codesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                message += $"Error: {ex.Message}";

                return message;
            }

            message += "Success";

            return message;
        }

        public static async Task<IdentityResult> AssignRoles(UserManager<ApplicationUser> userManager, string email, string[] roles)
        {

            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }

        private async Task<string> _BrokersAddAsync()
        {
            string[] brokerRole = new string[] { "Broker" };
            string message = "Broker Message: -> ";

            try
            {
                var model = _codesContext.Brokers.Where(x => x.IsActive && x.ApplicationReference == null && x.Email != null
                    && x.BrokerFirstName != null && x.BrokerLastName != null && x.BrokerLastName.Length > 0 && x.BrokerFirstName.Length > 0);

                foreach (var row in model)
                {
                    if (!_userManager.Users.Any(u => u.UserName == row.Email))
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            CompanyName = row.CompanyName,
                            Address = row.Address,
                            City = row.City,
                            ParentId = "",
                            Country = row.Country,
                            CreationDate = DateTime.Now,
                            CreatorIP = "127.0.0.1",
                            DeactivationDate = null,
                            DeactivationReason = "",
                            EIN = "",
                            Email = row.Email,
                            Fax = "",
                            FaxExtension = "",
                            FirstName = row.BrokerFirstName,
                            LastName = row.BrokerLastName,
                            MiddleName = row.BrokerMiddleName,
                            MobilePhone = row.MobilePhone,
                            OfficeExtension = "",
                            OfficePhone = row.OfficePhone,
                            PhoneNumber = "+" + row.MobilePhone ?? (row.OfficePhone != null ? row.OfficePhone : ""),
                            NormalizedEmail = row.Email.ToUpper(),
                            EmailConfirmed = true,
                            UserName = row.Email,
                            NormalizedUserName = row.Email.ToUpper(),
                            PostalCode = row.PostalCode,
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = row.State,
                            BrokerId = row.BrokerId
                        };

                        var password = new PasswordHasher<ApplicationUser>();
                        var hashed = password.HashPassword(user, GetRandomString(16));
                        user.PasswordHash = hashed;

                        var userStore = new UserStore<ApplicationUser>(_context);
                        var result = await userStore.CreateAsync(user);

                        row.ApplicationReference = user.Id;
                    }
                    else
                    {
                        var usr = await _userManager.FindByEmailAsync(row.Email);
                        row.ApplicationReference = usr.Id;
                    }

                    await AssignRoles(_userManager, row.Email, brokerRole);
                }

                await _context.SaveChangesAsync();
                await _codesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                message += $"Error: {ex.Message}";

                return message;
            }

            message += "Success";

            return message;
        }

        private Random rand = new Random();
        private const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private string GetRandomString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }
    }
}
