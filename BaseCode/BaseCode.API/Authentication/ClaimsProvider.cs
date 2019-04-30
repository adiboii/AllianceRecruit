using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BaseCode.API.Authentication
{
    public class ClaimsProvider
    {
        private readonly IUserService _userService;

        /// <summary>
        ///     Constructor for IUserService
        /// </summary>
        /// <param name="userService"></param>
        public ClaimsProvider(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Used to retreive claimsIdentity for access token generation
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GetClaimsIdentityAsync(string username, string password, BaseCodeEntities db)
        {
            ClaimsIdentity claimsIdentity = null;

            var user = await _userService.FindUserAsync(username, password);
            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            claimsIdentity = CreateClaimsIdentity(user, db);
            return await Task.FromResult(claimsIdentity);
        }

        /// <summary>
        ///      Used to retreive claimsIdentity for refresh token generation
        /// </summary>
        /// <param name="username"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GetIdentityAsync(string username, BaseCodeEntities db)
        {
            ClaimsIdentity claimsIdentity = null;

            var user = _userService.FindUser(username);
            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            claimsIdentity = CreateClaimsIdentity(user, db);
            return await Task.FromResult(claimsIdentity);
        }

        /// <summary>
        ///     Adds claims to claimsIdentity
        /// </summary>
        /// <param name="user"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public ClaimsIdentity CreateClaimsIdentity(User user, BaseCodeEntities db)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>();

            var userRoles = db.UserRoles.Where(i => i.UserId == user.UserID);
            foreach (var u in userRoles)
            {
                var role = db.Roles.Single(i => i.Id == u.RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            claims.Add(new Claim(Constants.ClaimTypes.UserName, user.Username));            
            claims.Add(new Claim(Constants.ClaimTypes.ID, user.Id.ToString()));
            claims.Add(new Claim(Constants.ClaimTypes.UserId, user.UserID));
            claims.Add(new Claim(ClaimTypes.Name, user.UserID));

            return new ClaimsIdentity(claims);
        }
    }
}
