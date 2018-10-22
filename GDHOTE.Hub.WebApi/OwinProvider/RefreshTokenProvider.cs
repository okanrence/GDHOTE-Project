using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.Models;
using Microsoft.Owin.Security.Infrastructure;

namespace GDHOTE.Hub.WebApi.OwinProvider
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            //var service = new RefreshTokenService();
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime") == null
                ? "10"
                : context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshToken
            {

                Token = PasswordManager.ReturnHashPassword(refreshTokenId),
                ClientId = Convert.ToInt64(clientid),
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await RefreshTokenService.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }

        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            // var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string hashedTokenId = context.Token;

            var refreshToken = await RefreshTokenService.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await RefreshTokenService.RemoveRefreshToken(hashedTokenId);
            }

        }
    }
}