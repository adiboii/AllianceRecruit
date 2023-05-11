using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
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
        private RoleManager<IdentityRole> _roleManager;

        public UserRepository(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<User> FindAll()
        {
            return GetDbSet<User>();
        }

        public ListViewModel FindUsers(UserSearchViewModel searchModel)
        {
            //var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var user = FindAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.UserId) || x.UserID.Contains(searchModel.UserId)) &&
                            (string.IsNullOrEmpty(searchModel.UserFirstName) || x.FirstName.Contains(searchModel.UserFirstName)) &&
                            (string.IsNullOrEmpty(searchModel.UserLastName) || x.LastName.Contains(searchModel.UserLastName)) &&
                            (string.IsNullOrEmpty(searchModel.UserEmail) || x.Email.Contains(searchModel.UserEmail)) &&
                            (string.IsNullOrEmpty(searchModel.UserPhoneNumber) || x.PhoneNumber.Contains(searchModel.UserPhoneNumber)) &&
                            (string.IsNullOrEmpty(searchModel.UserUsername) || x.Username.Contains(searchModel.UserUsername)));
                //.OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = user.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = user.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(stat => new {
                    id = stat.UserID,
                    firstname = stat.FirstName,
                    lastname = stat.LastName,
                    email = stat.Email,
                    phone = stat.PhoneNumber,
                    username = stat.Username,
                    password = stat.Password,
                    isActive = stat.IsActive

                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public User FindByUsername(string username)
        {
            return GetDbSet<User>().Where(x => x.Username.ToLower().Equals(username.ToLower())).AsNoTracking().FirstOrDefault();
        }

        public User FindById(string id)
        {
            return GetDbSet<User>().FirstOrDefault(x => x.Username.Equals(id));
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
                var userUpdate = FindById(user.Username);
                userUpdate.FirstName = user.FirstName;
                userUpdate.LastName = user.LastName;
                userUpdate.Email = user.Email;
                userUpdate.PhoneNumber = user.PhoneNumber;
                userUpdate.Username = user.Username;
                userUpdate.Password = user.Password;
                userUpdate.IsActive = user.IsActive;
                //this.SetEntityState(user, System.Data.Entity.EntityState.Modified);

                UnitOfWork.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            // GetDbSet<User>().Where(x => x.Username.ToLower().Equals(username.ToLower())).AsNoTracking().FirstOrDefault();
            var userUpdate = FindById(user.Username);
            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Email = user.Email;
            userUpdate.PhoneNumber = user.PhoneNumber;
            userUpdate.Username = user.Username;
            userUpdate.Password = user.Password;
            userUpdate.IsActive = user.IsActive;

            var identityUpdate = await _userManager.FindByNameAsync(user.Username);

            identityUpdate.UserName = userUpdate.Username;
            identityUpdate.Email = userUpdate.Email;
            var newPassword = _userManager.PasswordHasher.HashPassword(identityUpdate, userUpdate.Password);
            identityUpdate.PasswordHash = newPassword;

            var result = await _userManager.UpdateAsync(identityUpdate);
            UnitOfWork.SaveChanges();

            return result;
        }

        public void SoftDelete(User user)
        {
            var statusUpdate = FindById(user.Username);
            statusUpdate.IsActive = false;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void Delete(User user)
        {
            GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }
        public void DeleteById(string id)
        {
            var user = FindById(id);
            GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string phoneNumber, string role, bool isActive)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return result;

            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(role);
            if (checkIfRoleExists)
            {
                var result1 = await _userManager.AddToRoleAsync(user, role);

                if (!result1.Succeeded) return result1;
            }

            var userId = user.Id;

            // Insert user details
            var userEntity = new User
            {
                UserID = userId,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = password,
                //Role = role,
                IsActive = true
            };

            Create(userEntity);

            return result;
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!checkIfRoleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                var result = await _roleManager.CreateAsync(role);
                return result;
            }

            return null;
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

        public string GetSortKey(string sortBy)
        {
            string sortKey = "head";

            //switch (sortBy)
            //{
            //    case (Constants.User.UserHeaderId):
            //        sortKey = "UserID";
            //        break;

            //    case (Constants.User.UserHeaderFirstName):
            //        sortKey = "FirstName";
            //        break;

            //    case (Constants.User.UserHeaderLastName):
            //        sortKey = "LastName";
            //        break;

            //    case (Constants.User.UserHeaderUsername):
            //        sortKey = "Username";
            //        break;

            //    default:
            //        sortKey = "UserID";
            //        break;
            //}

            return sortKey;
        }
    }
}
