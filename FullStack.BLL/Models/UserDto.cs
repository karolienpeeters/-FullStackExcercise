using FullStack.DAL.Models;
using System.Collections.Generic;

namespace FullStack.BLL.Models
{
    public class UserDto
    {
        public UserDto()
        {
            
        }


        public UserDto(User user)
        {
            Id = user.UserId;
            RolesList = user.RolesList;
            Email = user.Email;
        }


        public string Id { get; set; }
        public List<string> RolesList { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
      
    }
}
