using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseCode.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User FindByUsername(string username)
        {
            return _userRepository.FindByUsername(username);
        }

        public User FindById(string id)
        {
            return _userRepository.FindById(id);
        }

        public User FindUser(string userName)
        {
            return _userRepository.FindUser(userName);
        }

        public IEnumerable<User> FindAll()
        {
            return _userRepository.FindAll();
        }

        public bool Create(User user)
        {
            return _userRepository.Create(user);
        }

        public bool Update(User user)
        {
            return _userRepository.Update(user);
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public Task<User> FindUserAsync(string userName, string password)
        {
            return _userRepository.FindUserAsync(userName, password);
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email)
        {
            return await _userRepository.RegisterUser(username, password, firstName, lastName, email);
        }

        public async Task<IdentityUser> FindUser(string username, string password)
        {
            return await _userRepository.FindUser(username, password);
        }
    }
}
