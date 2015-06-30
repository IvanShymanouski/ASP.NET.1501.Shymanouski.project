using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TaskManager.Providers;
using BLL.Interfaces;

namespace TaskManager.Areas.Admin
{
    public static class HelperFunctions
    {
        public static List<string> GetUsersAjax(string userLogin, IHasIdService<UserEntity> userService, string roleName="")
        {
            int namesCount = 10;
            Regex regex = new Regex(@"(\w*)" + userLogin + @"(\w*)", RegexOptions.IgnoreCase);

            IEnumerable<string> users;
            if (roleName==String.Empty) users = userService.GetAll().Where(u => regex.Matches(u.Login).Count > 0).Select(u => u.Login);
            else users = CustomRoleProvider.GetUsersInRole(roleName).Where(x => regex.Matches(x).Count > 0);

            List<string> userMatches = new List<string>(0);
            int i = 0;
            foreach (var user in users)
            {
                userMatches.Add(user);
                if (i >= namesCount) break;
            }

            return userMatches;
        }
    }
}