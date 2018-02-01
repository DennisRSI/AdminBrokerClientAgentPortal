using ClientPortal.Data;
using ClientPortal.Models;
using ClientPortal.Services._Interfaces;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Services
{
    public class UserService : IUserService
    {
        private readonly ICodeService _codeService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICodeService codeService)
        {
            _codeService = codeService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<AdminViewModel> SuperAdminAdd(AdminViewModel model, string password)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || user.Id == null || user.Id.Length < 1)
                {
                    user = new ApplicationUser()
                    {
                        CompanyName = model.CompanyName,
                        Address = model.Address,
                        City = model.City,
                        ParentId = "",
                        Country = model.Country,
                        CreationDate = DateTime.Now,
                        CreatorIP = model.CreatorIP,
                        DeactivationDate = model.DeactivationDate,
                        DeactivationReason = model.DeactivationReason,
                        EIN = model.EIN,
                        Email = model.Email,
                        Fax = model.Fax,
                        FaxExtension = model.FaxExtension,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        MobilePhone = model.MobilePhone,
                        OfficeExtension = model.OfficeExtension,
                        OfficePhone = model.OfficePhone,
                        NormalizedEmail = model.Email.ToUpper(),
                        EmailConfirmed = true,
                        UserName = model.Email,
                        NormalizedUserName = model.Email.ToUpper(),
                        PostalCode = model.PostalCode,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        State = model.State
                    };

                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var result1 = await _userManager.AddToRolesAsync(user, new string[] { "Super Administrator" });

                        model.Message = "Success";
                    }
                    else
                    {
                        model.Message = "";
                        foreach (var error in result.Errors)
                        {
                            if (model.Message.Length > 0)
                                model.Message += " | ";
                            else
                                model.Message = "Error: ";

                            model.Message += error.Description;
                        }

                    }
                }
                else
                {
                    model.Message = "Error: Admin user already created";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AdminViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<AdminViewModel> AdminAdd(AdminViewModel model, string password)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || user.Id == null || user.Id.Length < 1)
                {
                    user = new ApplicationUser()
                    {
                        CompanyName = model.CompanyName,
                        Address = model.Address,
                        City = model.City,
                        ParentId = "",
                        Country = model.Country,
                        CreationDate = DateTime.Now,
                        CreatorIP = model.CreatorIP,
                        DeactivationDate = model.DeactivationDate,
                        DeactivationReason = model.DeactivationReason,
                        EIN = model.EIN,
                        Email = model.Email,
                        Fax = model.Fax,
                        FaxExtension = model.FaxExtension,
                        FirstName = model.FirstName,                       
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        MobilePhone = model.MobilePhone,
                        OfficeExtension = model.OfficeExtension,
                        OfficePhone = model.OfficePhone,
                        NormalizedEmail = model.Email.ToUpper(),
                        EmailConfirmed = true,
                        UserName = model.Email,
                        NormalizedUserName = model.Email.ToUpper(),
                        PostalCode = model.PostalCode,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        State = model.State
                    };

                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var result1 = await _userManager.AddToRolesAsync(user, new string[] { "Administrator" });
                        model.Message = "Success";
                    }
                    else
                    {
                        model.Message = "";
                        foreach(var error in result.Errors)
                        {
                            if (model.Message.Length > 0)
                                model.Message += " | ";
                            else
                                model.Message = "Error: ";

                            model.Message += error.Description;
                        }
                        
                    }
                }
                else
                {
                    model.Message = "Error: Admin user already created";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AdminViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<AdminViewModel> AdminUpdate(AdminViewModel model)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AdminViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<AgentViewModel> AgentAdd(AgentViewModel model, string password)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || user.Id == null || user.Id.Length < 1)
                {
                    model = await _codeService.AgentAdd(model);
                    if (model.Message == "Success")
                    {
                        user = new ApplicationUser()
                        {
                            CompanyName = model.CompanyName,
                            Address = model.Address,
                            City = model.City,
                            ParentId = "",
                            Country = model.Country,
                            CreationDate = DateTime.Now,
                            CreatorIP = model.CreatorIP,
                            DeactivationDate = model.DeactivationDate,
                            DeactivationReason = model.DeactivationReason,
                            EIN = model.EIN,
                            Email = model.Email,
                            Fax = model.Fax,
                            FaxExtension = model.FaxExtension,
                            FirstName = model.AgentFirstName,
                            LastName = model.AgentLastName,
                            MiddleName = model.AgentMiddleName,
                            MobilePhone = model.MobilePhone,
                            OfficeExtension = model.OfficeExtension,
                            OfficePhone = model.OfficePhone,
                            NormalizedEmail = model.Email.ToUpper(),
                            EmailConfirmed = true,
                            UserName = model.Email,
                            NormalizedUserName = model.Email.ToUpper(),
                            PostalCode = model.PostalCode,
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = model.State,
                            AgentId = model.AgentId,
                            BrokerId = model.Broker.BrokerId,
                            ClientId = 0
                        };

                        var result = await _userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            var result1 = await _userManager.AddToRolesAsync(user, new string[] { "Agent" });
                            model.Message = "Success";
                        }
                        else
                        {
                            model.Message = "";
                            foreach (var error in result.Errors)
                            {
                                if (model.Message.Length > 0)
                                    model.Message += " | ";
                                else
                                    model.Message = "Error: ";

                                model.Message += error.Description;
                            }

                        }
                        
                        await _codeService.AgentAddAppReference(model.AgentId, user.Id);
                    }
                }
                else
                {
                    model.Message = "Error: Agent user already created";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AgentViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<AgentViewModel> AgentUpdate(AgentViewModel model)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && user.Id != null && user.Id.Length > 0)
                {
                    user.Address = model.Address;
                    user.City = model.City;
                    user.CompanyName = model.CompanyName;
                    user.Country = model.Country;
                    user.DeactivationDate = model.DeactivationDate;
                    user.DeactivationReason = model.DeactivationReason;
                    user.EIN = model.EIN;
                    user.Email = model.Email;
                    user.EmailConfirmed = true;
                    user.Fax = model.Fax;
                    user.FaxExtension = model.FaxExtension;
                    user.FirstName = model.AgentFirstName;
                    user.MiddleName = model.AgentMiddleName;
                    user.LastName = model.AgentLastName;
                    user.MobilePhone = model.MobilePhone;
                    user.NormalizedEmail = model.Email.ToUpper();
                    user.NormalizedUserName = model.Email.ToUpper();
                    user.OfficeExtension = model.OfficeExtension;
                    user.OfficePhone = model.OfficePhone;
                    user.PhoneNumberConfirmed = true;
                    user.PostalCode = model.PostalCode;
                    user.State = model.State;
                    user.UserName = model.Email;

                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();

                    await _codeService.AgentUpdate(model);
                }
                else
                {
                    model.Message = "Error: Agent not found";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AgentViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<BrokerViewModel> BrokerAdd(BrokerViewModel model, string password)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || user.Id == null || user.Id.Length < 1)
                {
                    model = await _codeService.BrokerAdd(model);
                    if (model.Message == "Success")
                    {
                        user = new ApplicationUser()
                        {
                            CompanyName = model.CompanyName,
                            Address = model.Address,
                            City = model.City,
                            ParentId = "",
                            Country = model.Country,
                            CreationDate = DateTime.Now,
                            CreatorIP = model.CreatorIP,
                            DeactivationDate = model.DeactivationDate,
                            DeactivationReason = model.DeactivationReason,
                            EIN = model.EIN,
                            Email = model.Email,
                            Fax = model.Fax,
                            FaxExtension = model.FaxExtension,
                            FirstName = model.BrokerFirstName,
                            LastName = model.BrokerLastName,
                            MiddleName = model.BrokerMiddleName,
                            MobilePhone = model.MobilePhone,
                            OfficeExtension = model.OfficeExtension,
                            OfficePhone = model.OfficePhone,
                            NormalizedEmail = model.Email.ToUpper(),
                            EmailConfirmed = true,
                            UserName = model.Email,
                            NormalizedUserName = model.Email.ToUpper(),
                            PostalCode = model.PostalCode,
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = model.State,
                            BrokerId = model.BrokerId,
                            AgentId = 0,
                            ClientId = 0
                        };

                        var result = await _userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            var result1 = await _userManager.AddToRolesAsync(user, new string[] { "Broker" });
                            model.Message = "Success";
                        }
                        else
                        {
                            model.Message = "";
                            foreach (var error in result.Errors)
                            {
                                if (model.Message.Length > 0)
                                    model.Message += " | ";
                                else
                                    model.Message = "Error: ";

                                model.Message += error.Description;
                            }

                        }
                        
                        await _codeService.BrokerAddAppReference(model.BrokerId, user.Id);
                    }
                }
                else
                {
                    model.Message = "Error: Broker user already created";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new BrokerViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<BrokerViewModel> BrokerUpdate(BrokerViewModel model)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && user.Id != null && user.Id.Length > 0)
                {
                    user.Address = model.Address;
                    user.City = model.City;
                    user.CompanyName = model.CompanyName;
                    user.Country = model.Country;
                    user.DeactivationDate = model.DeactivationDate;
                    user.DeactivationReason = model.DeactivationReason;
                    user.EIN = model.EIN;
                    user.Email = model.Email;
                    user.EmailConfirmed = true;
                    user.Fax = model.Fax;
                    user.FaxExtension = model.FaxExtension;
                    user.FirstName = model.BrokerFirstName;
                    user.MiddleName = model.BrokerMiddleName;
                    user.LastName = model.BrokerLastName;
                    user.MobilePhone = model.MobilePhone;
                    user.NormalizedEmail = model.Email.ToUpper();
                    user.NormalizedUserName = model.Email.ToUpper();
                    user.OfficeExtension = model.OfficeExtension;
                    user.OfficePhone = model.OfficePhone;
                    user.PhoneNumberConfirmed = true;
                    user.PostalCode = model.PostalCode;
                    user.State = model.State;
                    user.UserName = model.Email;

                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();

                    await _codeService.BrokerUpdate(model);
                }
                else
                {
                    model.Message = "Error: Broker not found";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new BrokerViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<ClientViewModel> ClientAdd(ClientViewModel model, string password)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                
                if (user == null || user.Id == null || user.Id.Length < 1)
                {
                    model = await _codeService.ClientAdd(model);
                    if (model.Message == "Success")
                    {
                        user = new ApplicationUser()
                        {
                            CompanyName = model.CompanyName,
                            Address = model.Address,
                            City = model.City,
                            ParentId = "",
                            Country = model.Country,
                            CreationDate = DateTime.Now,
                            CreatorIP = model.CreatorIP,
                            DeactivationDate = model.DeactivationDate,
                            DeactivationReason = model.DeactivationReason,
                            EIN = model.EIN,
                            Email = model.Email,
                            Fax = model.Fax,
                            FaxExtension = model.FaxExtension,
                            FirstName = model.ContactFirstName,
                            LastName = model.ContactLastName,
                            MiddleName = model.ContactMiddleName,
                            MobilePhone = model.MobilePhone,
                            OfficeExtension = model.OfficeExtension,
                            OfficePhone = model.OfficePhone,
                            NormalizedEmail = model.Email.ToUpper(),
                            EmailConfirmed = true,
                            UserName = model.Email,
                            NormalizedUserName = model.Email.ToUpper(),
                            PostalCode = model.PostalCode,
                            PhoneNumberConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                            State = model.State,
                            BrokerId = model.Broker.BrokerId,
                            ClientId = model.ClientId,
                            AgentId = 0
                        };

                        var result = await _userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            var result1 = await _userManager.AddToRolesAsync(user, new string[] { "Client" });
                            model.Message = "Success";
                        }
                        else
                        {
                            model.Message = "";
                            foreach (var error in result.Errors)
                            {
                                if (model.Message.Length > 0)
                                    model.Message += " | ";
                                else
                                    model.Message = "Error: ";

                                model.Message += error.Description;
                            }

                        }
                        
                        await _codeService.ClientAddAppReference(model.ClientId, user.Id);

                    }
                }
                else
                {
                    model.Message = "Error: Client user already created";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ClientViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<ClientViewModel> ClientUpdate(ClientViewModel model)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && user.Id != null && user.Id.Length > 0)
                {
                    user.Address = model.Address;
                    user.City = model.City;
                    user.CompanyName = model.CompanyName;
                    user.Country = model.Country;
                    user.DeactivationDate = model.DeactivationDate;
                    user.DeactivationReason = model.DeactivationReason;
                    user.EIN = model.EIN;
                    user.Email = model.Email;
                    user.EmailConfirmed = true;
                    user.Fax = model.Fax;
                    user.FaxExtension = model.FaxExtension;
                    user.FirstName = model.ContactFirstName;
                    user.MiddleName = model.ContactMiddleName;
                    user.LastName = model.ContactLastName;
                    user.MobilePhone = model.MobilePhone;
                    user.NormalizedEmail = model.Email.ToUpper();
                    user.NormalizedUserName = model.Email.ToUpper();
                    user.OfficeExtension = model.OfficeExtension;
                    user.OfficePhone = model.OfficePhone;
                    user.PhoneNumberConfirmed = true;
                    user.PostalCode = model.PostalCode;
                    user.State = model.State;
                    user.UserName = model.Email;

                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();

                    await _codeService.ClientUpdate(model);
                }
                else
                {
                    model.Message = "Error: Client not found";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ClientViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
    }
}
