using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Filters;
using WebsiteApplication.Models;
using WebsiteApplication.Models.ViewModels.Admin;

namespace WebsiteApplication.Controllers
{
    [RoleAuthorize(Roles = "ADMIN,ADMIN_TECH")]
    public class AdminController : BaseController
    {
        private readonly List<IdentityRole> _applicationRoles;

        public AdminController(WebsiteDatabaseContext context, ApplicationUserManager manager) : base(context, manager)
        {
            _applicationRoles = Context.Roles.ToList();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplicationUsers()
        {
            ViewData["rights"] = _applicationRoles.Select(x => new RightViewModel {RightId = x.Id, RightDescription = x.Name }).ToList();
            return View();
        }

        public ActionResult ReadUsers([DataSourceRequest] DataSourceRequest request)
        {
            var users = Context.Users.ToList();
            var userViewModels = users.Select(x => new UserViewModel
                                            {
                                                UserId = new Guid(x.Id),
                                                Username = x.Email,
                                                UserRoles = x.Roles.Select(r => new RightViewModel {RightId = r.RoleId, RightDescription = _applicationRoles.First(q => q.Id == r.RoleId).Name})
                                                    .ToList()
                                            });
            return Json(userViewModels.ToDataSourceResult(request));
        }

        public ActionResult UpdateUser([DataSourceRequest] DataSourceRequest request, UserViewModel userViewModel)
        {
            var userToUpdate = Context.Users.FirstOrDefault(x => x.Id == userViewModel.UserId.ToString());
            if (userToUpdate != null)
            {
                var rolesToAdd = new List<string>();
                var rolesToRemove = new List<string>();

                foreach (var newRole in userViewModel.UserRoles)
                {
                    var currentRole = userToUpdate.Roles.FirstOrDefault(x => x.RoleId == newRole.RightId);
                    if (currentRole == null)
                        rolesToAdd.Add(newRole.RightDescription);
                }
                foreach (var userRole in userToUpdate.Roles)
                {
                    var vmRole = userViewModel.UserRoles.FirstOrDefault(x => x.RightId == userRole.RoleId);
                    if (vmRole == null)
                        rolesToRemove.Add(_applicationRoles.Find(x => x.Id == userRole.RoleId).Name);
                }

                UserManager.RemoveFromRoles(userToUpdate.Id, rolesToRemove.ToArray());
                UserManager.AddToRoles(userToUpdate.Id, rolesToAdd.ToArray());
            }

            Context.SaveChanges();

            return Json(new[] { userViewModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser([DataSourceRequest] DataSourceRequest request, UserViewModel userViewModel)
        {
            var userToRemove = Context.Users.FirstOrDefault(x => x.Id == userViewModel.UserId.ToString());
            if (userToRemove != null)
            {
                Context.Users.Remove(userToRemove);
            }

            Context.SaveChanges();

            return Json(new[] {userViewModel}.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}