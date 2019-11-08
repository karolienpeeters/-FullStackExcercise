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
            RolesList = user.RolesList;
            Email = user.Email;
        }


        public string UserId { get; set; }
        public List<string> RolesList { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
      
    }
}
