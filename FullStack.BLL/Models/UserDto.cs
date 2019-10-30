using System;
using System.Collections.Generic;
using System.Text;
using FullStack.DAL.Models;

namespace FullStack.BLL.Models
{
    public class UserDto
    {
        public UserDto(User user)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            RolesList = user.RolesList;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            LockoutEnabled = user.LockoutEnabled;
            AccessFailedCount = user.AccessFailedCount;
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
