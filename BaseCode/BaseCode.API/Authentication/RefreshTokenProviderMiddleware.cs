using BaseCode.API.Utilities;
using BaseCode.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BaseCode.API.Authentication
{
    public class RefreshTokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private ClaimsProvider _claimsProvider;
        private readonly TokenProviderOptions _options;

        /// <summary>
        ///     Constructor for RequestDelegate and IOptions
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public RefreshTokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        ///     Catches context path for requesting refresh token
        /// </summary>
        /// <param name="context"></param>
        /// <param name="claimsProvider"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context, ClaimsProvider claimsProvider)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.RefreshTokenPath, StringComparison.Ordinal))
            {
                return _next(context);
            }

            _claimsProvider = claimsProvider;

            return GenerateToken(context);
        }

        /// <summary>
        ///     Handles Refresh token generation
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GenerateToken(HttpContext context)
        {
            var stream = context.Request.Body;
            string json = new StreamReader(stream).ReadToEnd();
            var user = JObject.Parse(json);

            var username = user[Constants.Token.Username].ToString();
            var refreshToken = user[Constants.Token.RefreshToken].ToString();

            var db = context.RequestServices.GetService<BaseCodeEntities>();
            var userManager = context.RequestServices.GetService<UserManager<IdentityUser>>();

            var refreshTokenModel = db.RefreshToken
                .SingleOrDefault(i => i.Token == refreshToken);

            if (refreshTokenModel == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var userDb = await userManager.FindByNameAsync(username);

            var identity = await _claimsProvider.GetIdentityAsync(username, db);
            if (identity == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(Constants.User.InvalidUserNamePassword);
                return;
            }

            var id = identity.Claims.Where(c => c.Type == Constants.Token.UserID)
                                    .Select(c => c.Value).SingleOrDefault();
            var userRole = db.UserRoles
                           .SingleOrDefault(i => i.UserId == id);

            if (userRole == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("No user role");
                return;
            }

            context.Response.Clear();
            var response = GetAuthResponse.Execute(identity, db, userDb, refreshTokenModel);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = Constants.Common.JSONContentType;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
