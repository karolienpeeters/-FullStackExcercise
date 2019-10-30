using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FullStack.BLL.Models;

namespace FullStack.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> HandleLogin(LoginDto login);
    }
}
