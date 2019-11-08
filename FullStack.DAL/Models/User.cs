﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FullStack.DAL.Models
{
    public class User
    {


        public User(IdentityUser identityUser, List<string> userRoles)
        {
            UserId = identityUser.Id;
            RolesList = userRoles;
            Email = identityUser.Email;

        }


        public string UserId { get; set; }
        
        public List<string> RolesList { get; set; }
        public string Email { get; set; }

    }
}
