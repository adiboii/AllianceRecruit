using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using BaseCode.API.Authentication;
using BaseCode.Data;
using BaseCode.Data.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseCode.Data.ViewModels.Common;

namespace BaseCode.API.Utilities
{
    public class GetAuthResponse
    {
        /// <summary>
        ///     Retrieves token options from appconfig.json 
        /// </summary>
        /// <returns></returns>
        public static TokenProviderOptions GetOptions()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Config.GetSection("BaseCode:AuthSecretKey").Value));

            return new TokenProviderOptions
            {
                Path = Configuration.Config.GetSection("BaseCode:TokenPath").Value,
                Audience = Configuration.Config.GetSection("BaseCode:Audience").Value,
                Issuer = Configuration.Config.GetSection("BaseCode:Issuer").Value,
                Expiration = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.Config.GetSection("BaseCode:ExpirationMinutes").Value)),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };

        }

        /// <summary>
        ///     Handles token generation for access and refresh tokens
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="db"></param>
        /// <param name="userName"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static LoginViewModel Execute(ClaimsIdentity identity, BaseCodeEntities db, IdentityUser userName, RefreshToken refreshToken = null)
        {
            var options = GetOptions();
            var now = DateTime.UtcNow;


            if (refreshToken == null)
            {
                refreshToken = new RefreshToken()
                {
                    Username = userName.UserName,
                    Token = Guid.NewGuid().ToString("N"),
                };
                db.InsertNew(refreshToken);
            }

            refreshToken.IssuedUtc = now;
            refreshToken.ExpiresUtc = now.Add(options.Expiration);
            db.SaveChanges();

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: identity.Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(options.Expiration),
                signingCredentials: options.SigningCredentials
            );

            IdentityModelEventSource.ShowPII = true;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new LoginViewModel
            {
                access_token = encodedJwt,
                refresh_token = refreshToken.Token,
                expires_in = (int)options.Expiration.TotalSeconds,
            };
            return response;
        }
    }
}