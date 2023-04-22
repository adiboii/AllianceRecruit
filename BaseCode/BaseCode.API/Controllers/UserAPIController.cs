using BaseCode.Data;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAPIController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("register")]
        public async Task<HttpResponseMessage> PostRegister(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            var result = await _userService.RegisterUser(userModel.UserName, userModel.Password, userModel.FirstName, userModel.LastName, userModel.EmailAddress, userModel.RoleName);
            var errorResult = GetErrorResult(result);

            return errorResult ? Helper.ComposeResponse(HttpStatusCode.BadRequest, ModelState) : Helper.ComposeResponse(HttpStatusCode.OK, "Successfully added user");
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("roles")]
        public async Task<HttpResponseMessage> PostCreateRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Constants.Common.InvalidRole);
            }

            var result = await _userService.CreateRole(role);
            var errorResult = GetErrorResult(result);

            return errorResult ? Helper.ComposeResponse(HttpStatusCode.BadRequest, Constants.Common.InvalidRole) : Helper.ComposeResponse(HttpStatusCode.OK, "Successfully added role");
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("login")]
        public async Task<HttpResponseMessage> PostLogin(string username, string password)
        {

            var result = await _userService.FindUserAsync(username, password);

            if (result == null)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Constants.User.InvalidUserNamePassword);
            }

            return Helper.ComposeResponse(HttpStatusCode.OK, Constants.User.Success);

        }

        private bool GetErrorResult(IdentityResult result)
        {
            if (result.Succeeded || result.Errors == null) return false;

            var flag = false;
            foreach (var error in result.Errors)
            {
                flag = true;
                ModelState.AddModelError("ModelStateErrors", error.Description);
            }

            return flag;
        }
    }
}