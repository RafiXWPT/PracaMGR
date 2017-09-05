using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Models;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.CodeBehind.Rights
{
    public class RightsManager : IRightsManager<RightViewModel, RoleViewModel, UserViewModel>
    {
        public RightsManager(WebsiteDatabaseContext context, ApplicationUserManager userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        public WebsiteDatabaseContext Context { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public List<RightViewModel> Rights()
        {
            return Context.Rights.Select(r => new RightViewModel
                {
                    Id = r.RightId,
                    RightDescription = r.RightDescription,
                    RightName = r.RightName
                })
                .ToList();
        }

        public List<RoleViewModel> Roles()
        {
            return Context.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();
        }

        public List<UserViewModel> Users()
        {
            var applicationUsers = Context.Users.ToList();
            return applicationUsers.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Name = u.UserName,
                    Roles = u.Roles.Select(r => new RoleViewModel
                        {
                            Id = r.RoleId,
                            Name = Roles().FirstOrDefault(x => x.Id == r.RoleId)?.Name ?? "-"
                        })
                        .ToList()
                })
                .ToList();
        }

        public List<string> RolesForRight(string right)
        {
            return Context.RolesToRights.Where(r => r.Right.RightName == right).Select(x => x.Role).ToList();
        }

        public List<string> RightsForRole(string role)
        {
            return Context.RolesToRights.Where(r => r.Role == role).Select(r => r.Right.RightName).ToList();
        }

        public List<string> RolesForRight(RightViewModel viewModel)
        {
            return Context.RolesToRights.Where(r => r.RightId == viewModel.Id).Select(x => x.Role).ToList();
        }

        public List<string> RoleNamesForGuid(List<string> roleGuids)
        {
            var roles = Roles();
            return roleGuids.SelectMany(r => roles.Where(ro => ro.Id == r)).Select(r => r.Name).ToList();
        }

        public void AddRight(RightViewModel viewModel)
        {
            if (Context.Rights.Any(r => r.RightName == viewModel.RightName))
                return;

            var newRight = new Right
            {
                RightId = Guid.NewGuid(),
                RightName = viewModel.RightName,
                RightDescription = viewModel.RightDescription
            };

            viewModel.Id = newRight.RightId;
            Context.Rights.Add(newRight);
            Context.SaveChanges();
        }

        public void EditRight(RightViewModel viewModel)
        {
            var right = Context.Rights.Find(viewModel.Id);
            if (right == null)
                return;

            right.RightName = viewModel.RightName;
            right.RightDescription = viewModel.RightDescription;
            Context.SaveChanges();
        }

        public void RemoveRight(RightViewModel viewModel)
        {
            var right = Context.Rights.Find(viewModel.Id);
            foreach (var rightToRole in Context.RolesToRights.Where(r => r.RightId == viewModel.Id))
                Context.Entry(rightToRole).State = EntityState.Deleted;

            Context.SaveChanges();
            Context.Entry(right).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void AddRole(RoleViewModel viewModel)
        {
            var newRole = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = viewModel.Name
            };
            viewModel.Id = newRole.Id;
            Context.Roles.Add(newRole);
            Context.SaveChanges();
        }

        public void RemoveRole(RoleViewModel viewModel)
        {
            var role = Context.Roles.Find(viewModel.Id);
            foreach (var user in role.Users)
                Context.Entry(user).State = EntityState.Deleted;

            Context.SaveChanges();
            foreach (var rightToRole in Context.RolesToRights.Where(r => r.Role == role.Name))
                Context.Entry(rightToRole).State = EntityState.Deleted;

            Context.SaveChanges();
            Context.Entry(role).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void AddRoleToRight(Guid rightId, string role)
        {
            Context.RolesToRights.Add(new RoleToRight
            {
                RoleToRightId = Guid.NewGuid(),
                RightId = rightId,
                Role = role
            });
            Context.SaveChanges();
        }

        public void RemoveRoleFromRight(Guid rightId, string role)
        {
            var roleToRight = Context.RolesToRights.FirstOrDefault(r => r.RightId == rightId);
            Context.Entry(roleToRight).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void AddRoleToUser(string userId, string role)
        {
            UserManager.AddToRole(userId, role);
        }

        public void RemoveRoleFromUser(string userId, string role)
        {
            UserManager.RemoveFromRole(userId, role);
        }
    }
}