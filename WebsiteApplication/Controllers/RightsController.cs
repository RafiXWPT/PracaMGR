﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RightsController : BaseController
    {
        public RightsController(IRightsManager<RightViewModel, RoleViewModel, UserViewModel> manager)
        {
            Manager = manager;
        }

        public IRightsManager<RightViewModel, RoleViewModel, UserViewModel> Manager { get; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadMultiSelectRoles()
        {
            return Json(Mapper.Map<List<RoleViewModel>>(Manager.Roles()), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RolesTab()
        {
            return PartialView();
        }

        public PartialViewResult RightsTab()
        {
            return PartialView();
        }

        public PartialViewResult RolesToRightsTab()
        {
            return PartialView();
        }

        public PartialViewResult UserRolesTab()
        {
            return PartialView();
        }

        public ActionResult ReadRights([DataSourceRequest] DataSourceRequest request)
        {
            var rights = Manager.Rights();

            return Json(Mapper.Map<List<RightViewModel>>(rights).ToDataSourceResult(request),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRoles([DataSourceRequest] DataSourceRequest request)
        {
            var roles = Manager.Roles().Where(x => x.Name != "ADMIN");

            return Json(Mapper.Map<List<RoleViewModel>>(roles).ToDataSourceResult(request),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            Manager.AddRight(viewModel);
            return Json(new[] {viewModel}.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateRole([DataSourceRequest] DataSourceRequest request, RoleViewModel viewModel)
        {
            Manager.AddRole(viewModel);
            return Json(new[] {viewModel}.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            Manager.EditRight(viewModel);
            return Json(new[] {viewModel}.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRole([DataSourceRequest] DataSourceRequest request, RoleViewModel viewModel)
        {
            return Json("");
        }

        public ActionResult DestroyRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            Manager.RemoveRight(viewModel);
            return Json(new[] {viewModel}.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyRole([DataSourceRequest] DataSourceRequest request, RoleViewModel viewModel)
        {
            Manager.RemoveRole(viewModel);
            return Json(new[] {viewModel}.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRolesToRight([DataSourceRequest] DataSourceRequest request)
        {
            var rolesToRights = new List<RightViewModel>();
            var rights = Manager.Rights();
            var roles = Manager.Roles();
            foreach (var right in rights)
            {
                var rightViewModel = new RightViewModel
                {
                    Id = right.Id,
                    RightName = right.RightName,
                    RightDescription = right.RightDescription,
                    Roles = new List<RoleViewModel>()
                };
                var rightRoles = Manager.RolesForRight(right);
                foreach (var rightRole in rightRoles)
                {
                    var role = roles.First(r => r.Name == rightRole);
                    rightViewModel.Roles.Add(new RoleViewModel
                    {
                        Id = role.Id,
                        Name = role.Name
                    });
                }
                rolesToRights.Add(rightViewModel);
            }

            return Json(rolesToRights.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRolesToRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            var currentRolesForRight = Manager.RolesForRight(viewModel);
            var toAddRoles = new List<string>();
            var toRemoveRoles = new List<string>();
            foreach (var role in viewModel.Roles)
                if (!currentRolesForRight.Contains(role.Name))
                    toAddRoles.Add(role.Name);

            foreach (var role in currentRolesForRight)
                if (viewModel.Roles.All(r => r.Name != role))
                    toRemoveRoles.Add(role);

            foreach (var role in toRemoveRoles)
                Manager.RemoveRoleFromRight(viewModel.Id, role);
            foreach (var role in toAddRoles)
                Manager.AddRoleToRight(viewModel.Id, role);

            return Json(new[] {viewModel}.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRolesToUsers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Manager.Users().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRolesToUser([DataSourceRequest] DataSourceRequest request, UserViewModel viewModel)
        {
            var currentRolesForUser = Manager.Users()
                .First(u => u.Id == viewModel.Id)
                .Roles.Select(r => r.Name)
                .ToList();
            var toAddRoles = new List<string>();
            var toRemoveRoles = new List<string>();

            foreach (var role in viewModel.Roles)
                if (!currentRolesForUser.Contains(role.Name))
                    toAddRoles.Add(role.Name);
            foreach (var role in currentRolesForUser)
                if (viewModel.Roles.All(r => r.Name != role))
                    toRemoveRoles.Add(role);

            foreach (var role in toRemoveRoles)
                Manager.RemoveRoleFromUser(viewModel.Id, role);
            foreach (var role in toAddRoles)
                Manager.AddRoleToUser(viewModel.Id, role);

            return Json(new[] {viewModel}.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}