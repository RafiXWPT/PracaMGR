﻿using System.Linq;
using System.Security.Principal;

namespace WebsiteApplication.CodeBehind.Extensions
{
    public static class UserRoleExtensions
    {
        public static bool IsInAnyRole(this IPrincipal user, string roles)
        {
            return roles.Split(',').Any(user.IsInRole);
        }
    }
}