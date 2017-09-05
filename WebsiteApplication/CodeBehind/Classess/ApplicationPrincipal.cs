using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using WebsiteApplication.Models;

namespace WebsiteApplication.CodeBehind.Classess
{
    public class ApplicationPrincipal : ClaimsPrincipal
    {
        private const string Guest = "Gość";
        private readonly Dictionary<string, string> _claimCache = new Dictionary<string, string>();

        public ApplicationPrincipal() { }

        public ApplicationPrincipal(IPrincipal claimsPrincipal) : base(claimsPrincipal) { }

        public string Name => GetValueOrDefault(ClaimTypes.Name, Guest);

        public string[] Roles => FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

        public string[] Rights => FindAll(ApplicationClaims.Rights).Select(c => c.Value).ToArray();

        private string GetValueOrDefault(string claimType, string defaultValue = null)
        {
            if (_claimCache.TryGetValue(claimType, out string claimValue))
                return claimValue;

            var claim = FindFirst(claimType);
            claimValue = claim != null ? claim.Value : defaultValue ?? string.Empty;
            _claimCache.Add(claimType, claimValue);
            return claimValue;
        }
    }
}