using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private UserManager<IdentityUser> _userManager;

        public UserRepository(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) 
            : base(unitOfWork)
        {
            _userManager = userManager;
        }

        public IEnumerable<User> FindAll()
        {
            return GetDbSet<User>();
        }

        public User FindByUsername(string username)
        {
            return GetDbSet<User>().Where(x => x.Username.ToLower().Equals(username.ToLower())).AsNoTracking().FirstOrDefault();
        }

        public User FindById(string id)
        {
            return GetDbSet<User>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public User FindUser(string userName)
        {
            var userDB = GetDbSet<User>().Where(x => x.Username.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            return userDB;
        }

        public bool Create(User user)
        {
            try
            {
                GetDbSet<User>().Add(user);
                UnitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(User user)
        {
            try
            {
                SetEntityState(user, EntityState.Modified);
                UnitOfWork.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Delete(User user)
        {
            GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return result;

            var userId = user.Id;

            // Insert user details
            var userEntity = new User
            {
                Id = userId,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
            };

            Create(userEntity);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<User> FindUserAsync(string userName, string password)
        {
            var userDB = GetDbSet<User>().Where(x => x.Username.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            var user = await _userManager.FindByNameAsync(userName);
            var isPasswordOK = await _userManager.CheckPasswordAsync(user, password);
            if ((user == null) || (isPasswordOK == false))
            {
                userDB = null;
            }
            return userDB;
        }
    }
}
