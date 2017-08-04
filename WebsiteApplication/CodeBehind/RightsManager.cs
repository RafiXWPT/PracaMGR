using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.DataAccessLayer;

namespace WebsiteApplication.CodeBehind
{
    public class RightsManager : IRightsManager
    {
        public WebsiteDatabaseContext Context { get; }

        public RightsManager(WebsiteDatabaseContext context)
        {
            Context = context;
        }

        public Right AddRight(string rightName, string rightDescription)
        {
            if (Context.Rights.Any(r => r.RightName == rightName))
                return null;

            var newRight = new Right
            {
                Id = Guid.NewGuid(),
                RightName = rightName,
                RightDescription = rightDescription
            };

            Context.Rights.Add(newRight);
            Context.SaveChanges();
            return newRight;
        }

        public Right EditRight(Guid rightId, string rightName, string rightDescription)
        {
            var right = Context.Rights.Find(rightId);
            if (right == null)
                return null;
            right.RightName = rightName;
            right.RightDescription = rightDescription;
            Context.SaveChanges();
            return right;
        }

        public Right RemoveRight(Guid rightId)
        {
            var right = Context.Rights.Find(rightId);
            Context.Entry(right).State = EntityState.Deleted;
            Context.SaveChanges();
            return right;
        }

        public List<Right> Rights()
        {
            return Context.Rights.ToList();
        }

        public List<string> RolesForRight(Guid rightId)
        {
            return Context.RolesToRights.Where(r => r.RightId == rightId).Select(x => x.Role).ToList();
        }

        public void AddRoleToRight(Guid rightId, string role)
        {
            Context.RolesToRights.Add(new RoleToRight
            {
                Id = Guid.NewGuid(),
                RightId = rightId,
                Role = role
            });
            Context.SaveChanges();
        }
    }
}