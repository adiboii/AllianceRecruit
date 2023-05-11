using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCode.Data.Contracts
{
    public interface IUserRepository
    {
        User FindByUsername(string username);
        User FindById(string id);
        User FindUser(string UserName);
        IQueryable<User> FindAll();
        ListViewModel FindUsers(UserSearchViewModel searchModel);
        bool Create(User user);
        bool Update(User user);
        void SoftDelete(User user);
        void Delete(User user);
        void DeleteById(string id);
        Task<IdentityResult> UpdateUser(User user);
        Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string phoneNumber, string role, bool isActive);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<User> FindUserAsync(string userName, string password);
        string GetSortKey(string sortBy);
    }
}