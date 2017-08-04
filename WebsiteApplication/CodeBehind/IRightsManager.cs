using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace WebsiteApplication.CodeBehind
{
    public interface IRightsManager
    {
        Right AddRight(string rightName, string rightDescription);
        Right EditRight(Guid rightId, string rightName, string rightDescription);
        Right RemoveRight(Guid rightId);
        List<Right> Rights();
        List<string> RolesForRight(Guid rightId);
        void AddRoleToRight(Guid rightId, string role);
    }
}