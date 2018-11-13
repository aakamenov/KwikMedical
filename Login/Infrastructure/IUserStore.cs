using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;

namespace Login.Infrastructure
{
    public interface IUserStore
    {
        Task<bool> AddUser(string username, string password);
        Task<AuthenticatedUser> GetUser(string username, string password);
        Task<bool> AuthenticateUser(string token);
        Task<bool> UpdateUser(User user);
    }
}
