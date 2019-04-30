using BaseCode.Data;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
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

            var result = await _userService.RegisterUser(userModel.UserName, userModel.Password, userModel.FirstName, userModel.LastName, userModel.EmailAddress);
            var errorResult = GetErrorResult(result);

            return errorResult ? Helper.ComposeResponse(HttpStatusCode.BadRequest, ModelState) : Helper.ComposeResponse(HttpStatusCode.OK, "Successfully added user");
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