using Packless.Dtos;
using Packless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packless.Database
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<List<String>> GetUsersAsync();
        Task<bool> DeleteUser(string username);
        Task<bool> ChangePassword(UserForRegisterDto user, string password);
    }
}
