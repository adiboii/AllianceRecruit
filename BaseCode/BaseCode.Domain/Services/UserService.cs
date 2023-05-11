
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using BaseCode.Data.Models;

namespace BaseCode.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User FindByUsername(string username)
        {
            return _userRepository.FindById(username);
        }

        public UserViewModel FindById(string username)
        {
            UserViewModel userViewModel = null;
            var user = _userRepository.FindById(username);

            if (user != null)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        public User FindUser(string userName)
        {
            return _userRepository.FindUser(userName);
        }

        public IQueryable<User> FindAll()
        {
            return _userRepository.FindAll();
        }

        public ListViewModel FindUsers(UserSearchViewModel searchModel)
        {
            return _userRepository.FindUsers(searchModel);
        }

        public bool Create(User user)
        {
            return _userRepository.Create(user);
        }

        public bool Update(User user)
        {
            return _userRepository.Update(user);
        }

        public void SoftDelete(User user)
        {
            _userRepository.SoftDelete(user);
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public void DeleteById(string id)
        {
            _userRepository.DeleteById(id);
        }

        public Task<User> FindUserAsync(string userName, string password)
        {
            return _userRepository.FindUserAsync(userName, password);
        }
        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string phoneNumber, string role, bool isActive)
        {
            return await _userRepository.RegisterUser(username, password, firstName, lastName, email, phoneNumber, role, isActive);
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await _userRepository.CreateRole(roleName);
        }

        public async Task<IdentityUser> FindUser(string username, string password)
        {
            return await _userRepository.FindUser(username, password);
        }
    }
}