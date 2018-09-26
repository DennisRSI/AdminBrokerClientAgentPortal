using System;
using System.Security.Claims;
using ClientPortal.Models;

namespace ClientPortal.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Role GetRole(this ClaimsPrincipal principal)
        {
            if (principal.IsInRole(Role.SuperAdministrator.GetName()))
            {
                return Role.SuperAdministrator;
            }

            if (principal.IsInRole(Role.Administrator.GetName()))
            {
                return Role.Administrator;
            }

            if (principal.IsInRole(Role.Broker.GetName()))
            {
                return Role.Broker;
            }

            if (principal.IsInRole(Role.Agent.GetName()))
            {
                return Role.Agent;
            }

            if (principal.IsInRole(Role.Client.GetName()))
            {
                return Role.Client;
            }

            throw new InvalidOperationException("Invalid role");
        }
    }
}
