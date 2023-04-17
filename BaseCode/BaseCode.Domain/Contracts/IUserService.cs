using BaseCode.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseCode.Domain.Contracts
{
    public interface IUserService
    {
        User FindByUsername(string username);
        User FindById(string id);
        User FindUser(string userName);
        IEnumerable<User> FindAll();
        bool Create(User user);
        bool Update(User user);
        void Delete(User user);
        Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityUser> FindUser(string username, string password);
        Task<User> FindUserAsync(string userName, string password);
    }
}
