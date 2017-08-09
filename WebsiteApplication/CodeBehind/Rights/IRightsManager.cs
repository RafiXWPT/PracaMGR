using System;
using System.Collections.Generic;

namespace WebsiteApplication.CodeBehind.Rights
{
    public interface IRightsManager<TRightViewModel, TRoleViewModel, TUserViewModel>
        where TRightViewModel : class
        where TRoleViewModel : class
        where TUserViewModel : class
    {
        List<TRightViewModel> Rights();
        List<TRoleViewModel> Roles();
        List<TUserViewModel> Users();
        List<string> RolesForRight(TRightViewModel viewModel);
        List<string> RolesForRight(string right);
        List<string> RoleNamesForGuid(List<string> roleGuids);
        void AddRight(TRightViewModel viewModel);
        void AddRole(TRoleViewModel viewModel);
        void EditRight(TRightViewModel viewModel);
        void RemoveRight(TRightViewModel viewModel);
        void RemoveRole(TRoleViewModel viewModel);
        void AddRoleToRight(Guid rightId, string role);
        void RemoveRoleFromRight(Guid rightId, string role);
        void AddRoleToUser(string userId, string role);
        void RemoveRoleFromUser(string userId, string role);
    }
}