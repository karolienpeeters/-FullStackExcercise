using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FullStack.DAL.Models
{
    public class User
    {


        public User(IdentityUser identityUser, string [] userRoles)
        {
            UserId = identityUser.Id;
            UserName = identityUser.UserName;
            RolesList = userRoles;
            Email = identityUser.Email;

        }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public string [] RolesList { get; set; }
        public string Email { get; set; }

    }
}
