using System;
using System.Collections.Generic;
using System.Text;
using FullStack.DAL.Models;

namespace FullStack.BLL.Models
{
    public class UserDto
    {
        public UserDto()
        {
            
        }

        public UserDto(User user)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            RolesList = user.RolesList;
            Email = user.Email;
            ShowForm = false;
        }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public string [] RolesList { get; set; }
        public string Email { get; set; }
        public bool ShowForm { get; set; }
   
    }
}
