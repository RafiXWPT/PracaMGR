using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using WebsiteApplication.Models;

namespace WebsiteApplication.CodeBehind.Classess
{
    public class ApplicationPrincipal : ClaimsPrincipal
    {
        private const string Guest = "Gość";
        private readonly Dictionary<string, string> _claimCache = new Dictionary<string, string>();

        public ApplicationPrincipal(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
        }

        public string Name => GetValueOrDefault(ClaimTypes.Name, Guest);

        public string[] Roles
        {
            get { return FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray(); }
        }

        public string[] Rights
        {
            get { return FindAll(ApplicationClaims.Rights).Select(c => c.Value).ToArray(); }
        }


        private string GetValueOrDefault(string claimType, string defaultValue = null)
        {
            string claimValue;
            if (_claimCache.TryGetValue(claimType, out claimValue))
            {
                return claimValue;
            }

            var claim = FindFirst(claimType);
            claimValue = claim != null ? claim.Value : defaultValue ?? string.Empty;
            _claimCache.Add(claimType, claimValue);
            return claimValue;
        }
    }
}