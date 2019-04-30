using BaseCode.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseCode.Data.Contracts
{
    public interface IUserRepository
    {
        User FindByUsername(string username);
        User FindById(string id);
        User FindUser(string UserName);
        IEnumerable<User> FindAll();
        bool Create(User user);
        bool Update(User user);
        void Delete(User user);
        Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<User> FindUserAsync(string userName, string password);
    }
}
