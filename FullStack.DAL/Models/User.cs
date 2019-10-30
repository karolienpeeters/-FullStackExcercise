using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FullStack.DAL.Models
{
    public class User
    {
        public User(IdentityUser identyUser, List<string> userRoles)
        {
            UserId = identyUser.Id;
            UserName = identyUser.UserName;
            RolesList = userRoles;
            Email = identyUser.Email;
            EmailConfirmed = identyUser.EmailConfirmed;
            LockoutEnabled = identyUser.LockoutEnabled;
            AccessFailedCount = identyUser.AccessFailedCount;
        }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> RolesList { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
