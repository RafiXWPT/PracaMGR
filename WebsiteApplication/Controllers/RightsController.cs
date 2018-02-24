using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RightsController : KendoController
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
            return Json(Manager.Roles(), JsonRequestBehavior.AllowGet);
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
            return JsonDataSourceResult(request, Manager.Rights());
        }

        public ActionResult ReadRoles([DataSourceRequest] DataSourceRequest request)
        {
            return JsonDataSourceResult(request, Manager.Roles().Where(x => x.Name != "ADMIN"));
        }

        public ActionResult CreateRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            Manager.AddRight(viewModel);
            return JsonDataSourceResult(request, ModelState, viewModel);
        }

        public ActionResult CreateRole([DataSourceRequest] DataSourceRequest request, RoleViewModel viewModel)
        {
            Manager.AddRole(viewModel);
            return JsonDataSourceResult(request, ModelState, viewModel);
        }

        public ActionResult UpdateRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            Manager.EditRight(viewModel);
            return JsonDataSourceResult(request, ModelState, viewModel);
        }

        public ActionResult UpdateRole([DataSourceRequest] DataSourceRequest request, RoleViewModel viewModel)
        {
            return Json("");
        }

        public ActionResult DestroyRight([DataSourceRequest] DataSourceRequest request, RightViewModel viewModel)
        {
            Manager.RemoveRight(viewModel);
            return JsonDataSourceResult(request, ModelState, viewModel);
        }

        public ActionResult DestroyRole([DataSourceRequest] DataSourceRequest request, RoleViewModel viewModel)
        {
            Manager.RemoveRole(viewModel);
            return JsonDataSourceResult(request, ModelState, viewModel);
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

            return JsonDataSourceResult(request, rolesToRights);
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

            return JsonDataSourceResult(request, viewModel);
        }

        public ActionResult ReadRolesToUsers([DataSourceRequest] DataSourceRequest request)
        {
            return JsonDataSourceResult(request, Manager.Users());
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

            return JsonDataSourceResult(request, viewModel);
        }
    }
}