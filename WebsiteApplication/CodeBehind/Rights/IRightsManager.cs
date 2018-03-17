using System;
using System.Collections.Generic;

namespace WebsiteApplication.CodeBehind.Rights
{
    public interface IRightsManager<TRightViewModel, TRoleViewModel, TUserViewModel>
        where TRightViewModel : class
        where TRoleViewModel : class
        where TUserViewModel : class
    {
        IEnumerable<TRightViewModel> Rights();
        IEnumerable<TRoleViewModel> Roles();
        IEnumerable<TUserViewModel> Users();
        IEnumerable<string> RolesForRight(TRightViewModel viewModel);
        IEnumerable<string> RolesForRight(string right);
        IEnumerable<string> RightsForRole(string role);
        IEnumerable<string> RoleNamesForGuid(List<string> roleGuids);
        void AddRight(TRightViewModel viewModel);
        void EditRight(TRightViewModel viewModel);
        void RemoveRight(TRightViewModel viewModel);
        void AddRole(TRoleViewModel viewModel);
        void RemoveRole(TRoleViewModel viewModel);
        void AddRoleToRight(Guid rightId, string role);
        void RemoveRoleFromRight(Guid rightId, string role);
        void AddRoleToUser(string userId, string role);
        void RemoveRoleFromUser(string userId, string role);
    }
}